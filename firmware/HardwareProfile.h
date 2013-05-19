/********************************************************************
 FileName:      HardwareProfile.h
 Dependencies:  See INCLUDES section
 Processor:     PIC18, PIC24, or PIC32 USB Microcontrollers
 Hardware:      The code is natively intended to be used on the 
                  following hardware platforms: 
                    PICDEM™ FS USB Demo Board
                    PIC18F46J50 FS USB Plug-In Module
                    PIC18F87J50 FS USB Plug-In Module
                    Explorer 16 + PIC24 or PIC32 USB PIMs
                    PIC24F Starter Kit
                    Low Pin Count USB Development Kit
                  The firmware may be modified for use on other USB 
                    platforms by editing this file (HardwareProfile.h)
 Compiler:  	Microchip C18 (for PIC18), C30 (for PIC24), 
                  or C32 (for PIC32)
 Company:       Microchip Technology, Inc.

 Software License Agreement:

 The software supplied herewith by Microchip Technology Incorporated
 (the “Company”) for its PIC® Microcontroller is intended and
 supplied to you, the Company’s customer, for use solely and
 exclusively on Microchip PIC Microcontroller products. The
 software is owned by the Company and/or its supplier, and is
 protected under applicable copyright laws. All rights are reserved.
 Any use in violation of the foregoing restrictions may subject the
 user to criminal sanctions under applicable laws, as well as to
 civil liability for the breach of the terms and conditions of this
 license.

 THIS SOFTWARE IS PROVIDED IN AN “AS IS” CONDITION. NO WARRANTIES,
 WHETHER EXPRESS, IMPLIED OR STATUTORY, INCLUDING, BUT NOT LIMITED
 TO, IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
 PARTICULAR PURPOSE APPLY TO THIS SOFTWARE. THE COMPANY SHALL NOT,
 IN ANY CIRCUMSTANCES, BE LIABLE FOR SPECIAL, INCIDENTAL OR
 CONSEQUENTIAL DAMAGES, FOR ANY REASON WHATSOEVER.

********************************************************************
 File Description:

 Change History:
  Rev   Date         Description
  1.0   11/19/2004   Initial release
  2.1   02/26/2007   Updated for simplicity and to use common
                     coding style
  2.3   09/15/2008   Broke out each hardware platform into its own
                     "HardwareProfile - xxx.h" file
********************************************************************/

#ifndef HARDWARE_PROFILE_H
#define HARDWARE_PROFILE_H

    //#define USE_SELF_POWER_SENSE_IO	
    #define tris_self_power     TRISAbits.TRISA2    // Input
    #if defined(USE_SELF_POWER_SENSE_IO)
    #define self_power          PORTAbits.RA2
    #else
    #define self_power          1
    #endif

    //#define USE_USB_BUS_SENSE_IO
    #define tris_usb_bus_sense  TRISAbits.TRISA1    // Input
    #if defined(USE_USB_BUS_SENSE_IO)
    #define USB_BUS_SENSE       PORTAbits.RA1
    #else
    #define USB_BUS_SENSE       1
    #endif

    #define CLOCK_FREQ 48000000
    
	#define LED			LATAbits.LATA0
	#define LED_TRIS 	TRISAbits.TRISA0

    /** LED ************************************************************/
    #define mInitAllLEDs()      LATC &= 0xF0; TRISC &= 0xF0;
    
    #define mLED_1              LATCbits.LATC0
    #define mLED_2              LATCbits.LATC1
    #define mLED_3              LATCbits.LATC2
    #define mLED_4              LATCbits.LATC3
    
    #define mGetLED_1()         mLED_1
    #define mGetLED_2()         mLED_2
    #define mGetLED_3()         mLED_3
    #define mGetLED_4()         mLED_4

    #define mLED_1_On()         mLED_1 = 1;
    #define mLED_2_On()         mLED_2 = 1;
    #define mLED_3_On()         mLED_3 = 1;
    #define mLED_4_On()         mLED_4 = 1;
    
    #define mLED_1_Off()        mLED_1 = 0;
    #define mLED_2_Off()        mLED_2 = 0;
    #define mLED_3_Off()        mLED_3 = 0;
    #define mLED_4_Off()        mLED_4 = 0;
    
    #define mLED_1_Toggle()     mLED_1 = !mLED_1;
    #define mLED_2_Toggle()     mLED_2 = !mLED_2;
    #define mLED_3_Toggle()     mLED_3 = !mLED_3;
    #define mLED_4_Toggle()     mLED_4 = !mLED_4;
    
    /** SWITCH *********************************************************/
    #define mInitSwitch2()      //TRISAbits.TRISA3=1
        //only one switch available so double duty
    #define mInitSwitch3()      //TRISAbits.TRISA3=1
    #define sw2                 PORTAbits.RA3
    #define sw3                 PORTAbits.RA3
    #define mInitAllSwitches()  mInitSwitch2();

    /** I/O pin definitions ********************************************/
    #define INPUT_PIN 1
    #define OUTPUT_PIN 0