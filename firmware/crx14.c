#include "GenericTypeDefs.h"
#include "Compiler.h"
#include "HardwareProfile.h"
#include "delays.h"
#include "crx14.h"

unsigned char WriteTAGBlock(unsigned char address, unsigned char* data){
	unsigned char buffer[7] = {0x06, 0x09, address, data[0], data[1], data[2], data[3]};
	SetWatchdogCRX(WRITE_WATCHDOG);
	writeCRX(buffer,BUFFER_REG,7);
	//WaitCRX();
	StopI2C();
}


unsigned char GetTAGBlock(unsigned char address, unsigned char* block)
{
	unsigned char buffer[4] = {0x02, 0x08, address};
	unsigned char i;
	SetWatchdogCRX(READ_WATCHDOG);
	writeCRX(buffer,BUFFER_REG,3);
	WaitCRX();
	if(ReadI2C(0)==4)		//RX LEN == 4 ??
	{
		for(i=0;i<3;i++)
			block[i]=ReadI2C(0);
		block[3]=ReadI2C(1);
		StopI2C();
		return 1;
	}
	else
	{
		StopI2C();
		return 0;
	}
}

unsigned char GetTAGUID(unsigned char* UID)
{
	unsigned char buffer[9] = {0x01, 0x0B};
	unsigned char i;
	SetWatchdogCRX(READ_WATCHDOG);
	writeCRX(buffer,BUFFER_REG,2);
	WaitCRX();
	if(ReadI2C(0)==8)		//RX LEN == 8 ??
	{
		for(i=0;i<7;i++)
			UID[i]=ReadI2C(0);
		UID[7]=ReadI2C(1);
		StopI2C();
		return 1;
	}
	else
	{
		StopI2C();
		return 0;
	}
}

void ResetToInventory(void)
{
	unsigned char buffer[2] = {0x01, 0x0C};
	SetWatchdogCRX(READ_WATCHDOG);
	writeCRX(buffer,BUFFER_REG,2);
	WaitCRX();
	StopI2C();
}

//Seleziona il tag che ha il chip id assegnato.
//Return = 1 select OK
//Return = 0 errore
unsigned char SelectTAG(unsigned char ChipID)
{
	unsigned char buffer[3] = {0x02, 0x0E, ChipID};
	SetWatchdogCRX(READ_WATCHDOG);
	writeCRX(buffer,BUFFER_REG,3);
	WaitCRX();
	buffer[0]=ReadI2C(0);
	buffer[1]=ReadI2C(1);
	StopI2C();
	if((buffer[0]==1)&&(buffer[1]==ChipID))
		return 1;
	return 0;
}


unsigned char InitiateTAG(unsigned char* ChipID)
{
	unsigned char buffer[3] = {0x02, 0x06, 0x00};
	SetWatchdogCRX(READ_WATCHDOG);
	//SetWatchdogCRX(WRITE_WATCHDOG);
	writeCRX(buffer,BUFFER_REG,3);
	WaitCRX();
	buffer[0]=ReadI2C(0);
	buffer[1]=ReadI2C(1);
	StopI2C();
	*ChipID=buffer[1];	//Salva ChipID Ricevuto
	return buffer[0];	//Restituisce numero byte letti
}

void InitCRX()
{
	unsigned char buffer = PARAMETER_REG_DEFAULT;
	
	SDA_TRIS=1;
	SCL_TRIS=1;
	SDA_LAT=0;
	SCL_LAT=0;
	
	writeCRX(&buffer,PARAMETER_REG,1);	//RF = ON
}

void SetWatchdogCRX(unsigned char watchdog)
{
	writeCRX(&watchdog,PARAMETER_REG,1);	// SETTA WATCHDOG
}

void WaitCRX()
{
	StartI2C();
	while(WriteI2C(DEV_SEL|0x01)){
		StopI2C();
		StartI2C();		
	}
}

void writeCRX(unsigned char* buffer, unsigned char address, unsigned char len)
{
	unsigned char i;
	StartI2C();     	// Start I2C communication		
	WriteI2C(DEV_SEL);	// Send DEV_SEL (R/W#)=0	
	WriteI2C(address);	// Send Address
	for(i=0;i<len;i++)
	{
		WriteI2C(buffer[i]);	// Send data
	}
	StopI2C();      			// Stop comunication
}

void readCRX(unsigned char* buffer, unsigned char len)
{
	unsigned char i;
	StartI2C();     		// Start I2C communication
	WriteI2C(DEV_SEL|0x01);	// Send DEV_SEL (R/W#)=1
	for(i=0;i<len-1;i++)
		buffer[i]=ReadI2C(0);	// read data w/ ACK
	buffer[len-1]=ReadI2C(1);	// read data w/ NACK
	StopI2C();      			// Stop comunication
}

void readCRXAddr(unsigned char* buffer, unsigned char address, unsigned char len)
{
	unsigned char i;
	StartI2C();     		// Start I2C communication
	WriteI2C(DEV_SEL);		// Send DEV_SEL (R/W#)=0
	WriteI2C(address);		// Send address
	StartI2C();     		// BUS RESTART
	WriteI2C(DEV_SEL|0x01);	// Send DEV_SEL (R/W#)=1
	for(i=0;i<len-1;i++)
		buffer[i]=ReadI2C(0);	// read data w/ ACK
	buffer[len-1]=ReadI2C(1);	// read data w/ NACK
	StopI2C();      			// Stop comunication
}

void StartI2C(void)
{
	SDA=0;
	Delay10TCYx(I2C_DELAY);
	SCL=0;
	Delay10TCYx(I2C_DELAY);
}

void StopI2C(void)
{
	SCL=1;
	Delay10TCYx(I2C_DELAY);
	SDA=1;
}

//return ACK from slave
unsigned char WriteI2C(unsigned char data)
{
	char i;
	unsigned char ack;

	for(i=0x80;i!=0;i=i>>1){
		if((data&i)!=0)
			SDA=1;
		else
			SDA=0;
		Delay10TCYx(1);	//anti glitch
		SCL=1;
		Delay10TCYx(I2C_DELAY);
		SCL=0;
		Delay10TCYx(I2C_DELAY);
	}
	
	SDA=1;		//SDA hi-z to let slave control it
	SCL=1;
	Delay10TCYx(I2C_DELAY);
	if(SDA_PORT)
		ack=1;
	else
		ack=0;
	SDA=0;		//SDA = 0 to be consistent with next xfer
	SCL=0;
	Delay10TCYx(I2C_DELAY);
	return ack;	
}

//
unsigned char ReadI2C(unsigned char ack)
{
	char i;
	char res=0;
	
	SDA=1;
	for(i=0x80;i!=0;i=i>>1){
		SCL=1;
		Delay10TCYx(I2C_DELAY);
		if(SDA_PORT)
			res=res|i;
		SCL=0;
		Delay10TCYx(I2C_DELAY);
	}
	
	SDA=ack;
	SCL=1;
	Delay10TCYx(I2C_DELAY);
	SCL=0;
	
	return res;
}
