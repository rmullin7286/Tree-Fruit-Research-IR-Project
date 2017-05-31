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
	
	return 0;
}
