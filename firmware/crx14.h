#define SDA_TRIS	TRISBbits.TRISB0
#define SDA_LAT		LATBbits.LATB0
#define SDA_PORT	PORTBbits.RB0
#define SCL_TRIS	TRISBbits.TRISB1
#define SCL_LAT		LATBbits.LATB1
#define SCL_PORT	PORTBbits.RB1
#define SDA		SDA_TRIS
#define SCL		SCL_TRIS

#define I2C_DELAY	10

#define DEV_SEL 0xA0

#define PARAMETER_REG 	0x00
#define BUFFER_REG		0x01
#define AUTHENICATE_REG	0x02
#define SLOT_MARKER_REG	0x03

#define PARAMETER_REG_DEFAULT	0x70
#define READ_WATCHDOG	0x10
#define AUTH_WATCHDOG	0x50
#define WRITE_WATCHDOG	0x30
#define MCU_WATCHDOG	0x70

unsigned char WriteTAGBlock(unsigned char address, unsigned char* data);
unsigned char GetTAGBlock(unsigned char address, unsigned char* block);

// RETURN = 0x01 -> GET UID OF SELECTED TAG
// RETURN = 0x00 -> ERROR
unsigned char GetTAGUID(unsigned char* UID);

// RETURN = 0x01 -> SELECT OK
// RETURN = 0x00 -> ERROR
void ResetToInventory(void);
unsigned char SelectTAG(unsigned char ChipID);

// RETURN = 0xFF -> CRC ERROR
// RETURN = 0x00 -> NO ANSWER
unsigned char InitiateTAG(unsigned char* ChipID);



void InitCRX(void);
void SetWatchdogCRX(unsigned char watchdog);
void WaitCRX(void);

void writeCRX(unsigned char* buffer, unsigned char address, unsigned char len);
void readCRX(unsigned char* buffer, unsigned char len);
void readCRXAddr(unsigned char* buffer, unsigned char address, unsigned char len);


void StartI2C(void);
void StopI2C(void);
unsigned char WriteI2C(unsigned char data);
unsigned char ReadI2C(unsigned char ack);
