/***************************************************************************
PROGRAM: Pi program for the Tree Fruit Research Center IR sensor.
AUTHOR: Ryan Mullin
Desc: This is the pi portion of the IR sensor program I'm developing for the
WSU Tree Fruit Research Center. This waits for a request, recieves data from
the MLX90614 IR Sensor and sends it over an RF signal from a nRF24l01+ 
transmitter. This is meant to be used with the other program developed for
Arduino nano and Windows 10 pc.
****************************************************************************/

#include "../include/pi.hpp"

int main(void)
{
		

	
	//Initialize radio in the scope of main()
	//the SPI is set to 25, it's different depending on the user
	RF24 radio(25,0); 
	
	cout << "pi rf reader by Ryan Mullin" << endl;
	

	initialize(&radio);
	
	cout << endl << "Make sure that the Arduino reciever is configured to recieve before continuing. Press Enter when ready." << endl;
	system("pause");
	
	loop(radio);
	
	return 0;
}
