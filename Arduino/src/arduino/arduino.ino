#include <SPI.h>
#include "RF24.h"

#define TEST 0x00

bool test(RF24 *radio);
bool radioNumber = 0;
byte pipes[][6] = {"1Node", "2Node"};
RF24 radio(9, 10);

byte command;
float temps[2];

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

  radio.begin();
  radio.setPALevel(RF24_PA_MIN);
  radio.openWritingPipe(pipes[1]);
  radio.openReadingPipe(1, pipes[0]);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial.available() > 0)
  {
    command = Serial.read();

    switch(command)
    {
      case TEST:
      if(test(&radio))
        Serial.println("S"); // S for success
      else
        Serial.println("F"); // F for fail  
      break;
    }
  }
}

bool test(RF24 *radio)
{
  bool request = 0;

  radio->write(&request, sizeof(bool));
  
  radio->startListening();
  unsigned long started_waiting_at = micros();

  while(!radio->available())
  {
    if(micros() - started_waiting_at > 200000)
    {
      return false;
    }
  }

  radio->read(&request, sizeof(bool));
  radio->stopListening();
  return true;
}

