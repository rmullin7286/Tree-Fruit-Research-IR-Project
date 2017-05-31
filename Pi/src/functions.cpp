#include "../include/pi.hpp"

void initialize(RF24 *radio)
{
	//first, we'll initialize the i2c connection to the MLX90614
	bcm2835_init();
	bcm2835_i2c_begin();
	bcm2835_i2c_set_baudrate(25000);
	bcm2835_i2c_setSlaveAddress(ADDRESS);
	
	//next, initialize the RF24 radio
	radio->begin();
	radio->setRetries(15,15);
	radio->printDetails();
	radio->openWritingPipe(pipes[0]);
	radio->openReadingPipe(1, pipes[1]);
}

void loop(RF24 radio)
{
	//this is the array we'll be storing the temps in. Use enum values for index.
	double temps[2] = {0.0, 0.0};
	 
	//loop forever
	while(1)
	{
		//first, we have to get the information from the IR sensor.
		readIR(temps);
	}
}

void readIR(double *temps)
{
	char mlxbuf[6];
	//begin i2c connection
	bcm2835_i2c_begin();
}


