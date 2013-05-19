/********************************************************************
 FileName:		main.c
 Dependencies:	See INCLUDES section
 Processor:		PIC18, PIC24, and PIC32 USB Microcontrollers
 Hardware:		This demo is natively intended to be used on Microchip USB demo
 				boards supported by the MCHPFSUSB stack.  See release notes for
 				support matrix.  This demo can be modified for use on other hardware
 				platforms.
 Complier:  	Microchip C18 (for PIC18), C30 (for PIC24), C32 (for PIC32)
 Company:		Microchip Technology, Inc.
********************************************************************
 File Description:

 Change History:
  Rev   Description
  ----  -----------------------------------------
  2.6a  Added support for the PIC24FJ256GB210
  2.7   No change
  2.7b  Improvements to USBCBSendResume(), to make it easier to use.
********************************************************************/

/** INCLUDES *******************************************************/
#include "USB/usb.h"
#include "HardwareProfile.h"
#include "USB/usb_function_generic.h"
#include "delays.h"
#include "crx14.h"

#define VERSION_MAJOR	2
#define VERSION_MINOR	1
#define CODE_PROTECT	ON

#pragma config PCLKEN = ON 
#pragma config PLLSEL = PLL3X 
#pragma config CFGPLLEN = ON 
#pragma config CPUDIV = NOCLKDIV 
#pragma config LS48MHZ = SYS48X8 
#pragma config FOSC = INTOSCIO 
#pragma config WDTEN = OFF 
#pragma config PBADEN = OFF 
#pragma config MCLRE = ON 
#pragma config LVP = OFF 
#pragma config FCMEN = OFF 
#pragma config IESO = OFF 
#pragma config XINST = OFF 
#pragma config CP0      = CODE_PROTECT
#pragma config CP1      = CODE_PROTECT
#pragma config CP2      = CODE_PROTECT
#pragma config CP3      = CODE_PROTECT
#pragma config CPB      = CODE_PROTECT
#pragma config CPD      = CODE_PROTECT
#pragma config WRT0     = CODE_PROTECT
#pragma config WRT1     = CODE_PROTECT
#pragma config WRT2     = CODE_PROTECT
#pragma config WRT3     = CODE_PROTECT
#pragma config WRTB     = CODE_PROTECT
#pragma config WRTC     = CODE_PROTECT

/** VARIABLES ******************************************************/
#pragma udata USB_VARIABLES=0x500
unsigned char OUTPacket[64];	//User application buffer for receiving and holding OUT packets sent from the host
unsigned char INPacket[64];		//User application buffer for sending IN packets to the host
#pragma udata
USB_HANDLE USBGenericOutHandle;
USB_HANDLE USBGenericInHandle;
#pragma udata

/** PRIVATE PROTOTYPES *********************************************/
static void InitializeSystem(void);
void USBDeviceTasks(void);
void YourHighPriorityISRCode(void);
void YourLowPriorityISRCode(void);
void USBCBSendResume(void);
void UserInit(void);
void ProcessIO(void);
void BlinkLED(void);

/** VECTOR REMAPPING ***********************************************/
	#if defined(PROGRAMMABLE_WITH_USB_HID_BOOTLOADER)
		#define REMAPPED_RESET_VECTOR_ADDRESS			0x1000
		#define REMAPPED_HIGH_INTERRUPT_VECTOR_ADDRESS	0x1008
		#define REMAPPED_LOW_INTERRUPT_VECTOR_ADDRESS	0x1018
	#elif defined(PROGRAMMABLE_WITH_USB_MCHPUSB_BOOTLOADER)	
		#define REMAPPED_RESET_VECTOR_ADDRESS			0x800
		#define REMAPPED_HIGH_INTERRUPT_VECTOR_ADDRESS	0x808
		#define REMAPPED_LOW_INTERRUPT_VECTOR_ADDRESS	0x818
	#else	
		#define REMAPPED_RESET_VECTOR_ADDRESS			0x00
		#define REMAPPED_HIGH_INTERRUPT_VECTOR_ADDRESS	0x08
		#define REMAPPED_LOW_INTERRUPT_VECTOR_ADDRESS	0x18
	#endif
	
	#if defined(PROGRAMMABLE_WITH_USB_HID_BOOTLOADER)||defined(PROGRAMMABLE_WITH_USB_MCHPUSB_BOOTLOADER)
	extern void _startup (void);        // See c018i.c in your C18 compiler dir
	#pragma code REMAPPED_RESET_VECTOR = REMAPPED_RESET_VECTOR_ADDRESS
	void _reset (void)
	{
	    _asm goto _startup _endasm
	}
	#endif
	#pragma code REMAPPED_HIGH_INTERRUPT_VECTOR = REMAPPED_HIGH_INTERRUPT_VECTOR_ADDRESS
	void Remapped_High_ISR (void)
	{
	     _asm goto YourHighPriorityISRCode _endasm
	}
	#pragma code REMAPPED_LOW_INTERRUPT_VECTOR = REMAPPED_LOW_INTERRUPT_VECTOR_ADDRESS
	void Remapped_Low_ISR (void)
	{
	     _asm goto YourLowPriorityISRCode _endasm
	}
	
	#if defined(PROGRAMMABLE_WITH_USB_HID_BOOTLOADER)||defined(PROGRAMMABLE_WITH_USB_MCHPUSB_BOOTLOADER)
	#pragma code HIGH_INTERRUPT_VECTOR = 0x08
	void High_ISR (void)
	{
	     _asm goto REMAPPED_HIGH_INTERRUPT_VECTOR_ADDRESS _endasm
	}
	#pragma code LOW_INTERRUPT_VECTOR = 0x18
	void Low_ISR (void)
	{
	     _asm goto REMAPPED_LOW_INTERRUPT_VECTOR_ADDRESS _endasm
	}
	#endif	//end of "#if defined(PROGRAMMABLE_WITH_USB_HID_BOOTLOADER)||defined(PROGRAMMABLE_WITH_USB_LEGACY_CUSTOM_CLASS_BOOTLOADER)"

	#pragma code
	
	//These are your actual interrupt handling routines.
	#pragma interrupt YourHighPriorityISRCode
	void YourHighPriorityISRCode()
	{
		USBDeviceTasks();
	}	//This return will be a "retfie fast", since this is in a #pragma interrupt section 
	#pragma interruptlow YourLowPriorityISRCode
	void YourLowPriorityISRCode()
	{
	
	}	//This return will be a "retfie", since this is in a #pragma interruptlow section 

/** DECLARATIONS ***************************************************/
#pragma code

unsigned char ChipID;

unsigned char TAGisPresent(void)
{
	if(SelectTAG(ChipID))
		return 1;
	
	if(InitiateTAG(&ChipID)==1)
		if(SelectTAG(ChipID))
			return 1;	

	ResetToInventory();
	return 0;
}

void main(void)
{   
    InitializeSystem();
	BlinkLED();
	USBDeviceAttach();
	
    while(1)
        ProcessIO();
}

static void InitializeSystem(void)
{
	OSCCON = 0x70;	//Internal osc, 16MHz
	
	while(OSCCON2bits.PLLRDY==0);
	
    ADCON1 |= 0x0F;			// Default all pins to digital
    
	LED_TRIS=0;				//abilita output LED
	LED=1;

	TRISB=0x03;				//tutti out tranne SCL SDA
		
	InitCRX();
	Delay10KTCYx(24);		//20ms
	ResetToInventory();
	
	USBGenericOutHandle = 0;	
	USBGenericInHandle = 0;		
    USBDeviceInit();

}//end InitializeSystem

unsigned short blinkLed = 0;
void serviceLED(void)
{
	if(blinkLed!=0)
		blinkLed--;
	if(blinkLed==1)
		LED=~LED;
}


void BlinkLED(void)
{
	if(blinkLed!=0)
		return;
	LED=~LED;
	blinkLed=5000;
}

void ProcessIO(void)
{   
	unsigned char i;
	unsigned char INlenght=0;
	
	serviceLED();

    if((USBDeviceState < CONFIGURED_STATE)||(USBSuspendControl==1)) return;
   
    if(!USBHandleBusy(USBGenericOutHandle))		//Check if the endpoint has received any data from the host.
    {   
	    INPacket[0]=0;
		INlenght=1;
	    
		//COMANDO 0x01 = PRESENZA TAG
		if(OUTPacket[0]==0x01){
			INPacket[0]=TAGisPresent();
			LED=~INPacket[0];
		}
		
		//COMANDO 0x02 = GET UID
		if(OUTPacket[0]==0x02){
			BlinkLED();
			if(GetTAGUID(INPacket))
				INlenght=8;
		}
		
		//COMANDO 0x03 = READ BLOCK | BLOCK ADDR
		if(OUTPacket[0]==0x03){
			BlinkLED();
			if(GetTAGBlock(OUTPacket[1],INPacket))
				INlenght=4;
		}
		
		//COMANDO 0x04 = WRITE BLOCK | BLOCK ADDR | BYTE 0 | BYTE 1 | BYTE 2 | BYTE 3
		if(OUTPacket[0]==0x04){
			BlinkLED();
			if(WriteTAGBlock(OUTPacket[1],OUTPacket+2))
				INPacket[0]=0x01;
		}
		
		//COMANDO 0x05 = GET VERSION
		if(OUTPacket[0]==0x05){
			BlinkLED();
			INPacket[0]=VERSION_MAJOR;
			INPacket[1]=VERSION_MINOR;
			INlenght=2;
		}
		
		//COMANDO 0x06 = WRITE RAW
		//COMANDO 0x07 = AUTHENTICATE
		
		if(!USBHandleBusy(USBGenericInHandle))		
			USBGenericInHandle = USBGenWrite(USBGEN_EP_NUM,(BYTE*)&INPacket,INlenght);
        USBGenericOutHandle = USBGenRead(USBGEN_EP_NUM,(BYTE*)&OUTPacket,USBGEN_EP_SIZE);
    }
}//end ProcessIO

// ******************************************************************************************************
// ************** USB Callback Functions ****************************************************************
// ******************************************************************************************************
void USBCBSuspend(void)
{
}

void USBCBWakeFromSuspend(void)
{
}

void USBCB_SOF_Handler(void)
{
}

void USBCBErrorHandler(void)
{
}

void USBCBCheckOtherReq(void)
{
}

void USBCBStdSetDscHandler(void)
{
}

void USBCBInitEP(void)
{
    USBEnableEndpoint(USBGEN_EP_NUM,USB_OUT_ENABLED|USB_IN_ENABLED|USB_HANDSHAKE_ENABLED|USB_DISALLOW_SETUP);
    USBGenericOutHandle = USBGenRead(USBGEN_EP_NUM,(BYTE*)&OUTPacket,USBGEN_EP_SIZE);
}

void USBCBSendResume(void)
{
    static WORD delay_count;
    
    if(USBGetRemoteWakeupStatus() == TRUE) 
    {
        if(USBIsBusSuspended() == TRUE)
        {
            USBMaskInterrupts();
            
            //Clock switch to settings consistent with normal USB operation.
            USBCBWakeFromSuspend();
            USBSuspendControl = 0; 
            USBBusIsSuspended = FALSE;
            delay_count = 3600U;        
            do
            {
                delay_count--;
            }while(delay_count);
            
            //Now drive the resume K-state signalling onto the USB bus.
            USBResumeControl = 1;       // Start RESUME signaling
            delay_count = 1800U;        // Set RESUME line for 1-13 ms
            do
            {
                delay_count--;
            }while(delay_count);
            USBResumeControl = 0;       //Finished driving resume signalling

            USBUnmaskInterrupts();
        }
    }
}

BOOL USER_USB_CALLBACK_EVENT_HANDLER(USB_EVENT event, void *pdata, WORD size)
{
    switch(event)
    {
        case EVENT_TRANSFER:
            //Add application specific callback task or callback function here if desired.
            break;
        case EVENT_SOF:
            USBCB_SOF_Handler();
            break;
        case EVENT_SUSPEND:
            USBCBSuspend();
            break;
        case EVENT_RESUME:
            USBCBWakeFromSuspend();
            break;
        case EVENT_CONFIGURED: 
            USBCBInitEP();
            break;
        case EVENT_SET_DESCRIPTOR:
            USBCBStdSetDscHandler();
            break;
        case EVENT_EP0_REQUEST:
            USBCBCheckOtherReq();
            break;
        case EVENT_BUS_ERROR:
            USBCBErrorHandler();
            break;
        case EVENT_TRANSFER_TERMINATED:
            //Add application specific callback task or callback function here if desired.
            //The EVENT_TRANSFER_TERMINATED event occurs when the host performs a CLEAR
            //FEATURE (endpoint halt) request on an application endpoint which was 
            //previously armed (UOWN was = 1).  Here would be a good place to:
            //1.  Determine which endpoint the transaction that just got terminated was 
            //      on, by checking the handle value in the *pdata.
            //2.  Re-arm the endpoint if desired (typically would be the case for OUT 
            //      endpoints).
            break;
        default:
            break;
    }      
    return TRUE; 
}
/** EOF main.c ***************************************************************/
