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
	bool success;
	 
	//loop forever
	while(1)
	{
		//first, we have to get the information from the IR sensor.
		cout << "retrieving temperature information." << endl;
		readIR(temps);
		
		//send the data over RF.
		cout << "sending information to reciever." << endl;
		while(!success)
		{
			success = send(temps, &radio);
			if(success) cout << "Success!" << endl;
			else cout << "Failed to send. Trying again" << endl;
		}
		
		//after the info has been sent, wait until the next time to send.
		waitForRequest(&radio);
	}
}

void readIR(double *temps)
{
	char ambBuf[6], objBuf[6];
	char measurement = AMBIENT;
	
	//start with the ambient temp.
	bcm2835_i2c_begin();
	bcm2835_i2c_write(&measurement, 1);
	bcm2835_i2c_read_register_rs(&measurement, &ambBuf[0], 3);
	
	//convert ambient to celsius
	temps[ARRAY_AMBIENT] = getCelcius(ambBuf);
	
	//next, get object temp
	measurement = OBJECT;
	
	bcm2835_i2c_begin();
	bcm2835_i2c_write(&measurement, 1);
	bcm2835_i2c_read_register_rs(&measurement, &objBuf[0], 3);
	
	//convert object to celsius
	temps[ARRAY_OBJECT] = getCelcius(objBuf);
	
}

double getCelcius(char buffer[])
{
	int temp = (double)(((buffer[1]) << 8) + buffer[0]);
	temp = (temp * 0.02) - 0.01;
	temp = temp - 273.15;
	
	return temp;
}

bool send(double *temps, RF24 *radio)
{
	bool success;
	success = radio->write(&temps, sizeof(temps));
	cout << "data sent. Awaiting response..." << endl;
	return success;
}

void waitForRequest(RF24 *radio)
{
	bool request = false;
	
	cout << "waiting for next request" << endl;
	
	radio->startListening();
	
	while(!request)
	{
		if(radio->available())
		{
			cout << "request recieved" << endl;
			radio->read(&request, sizeof(request));
		}
	}
	
	radio->stopListening();
}

