#include <SPI.h>
#include "RF24.h"

#define TEST 0x00
#define RUN 0x01

enum {ARRAY_AMBIENT, ARRAY_OBJECT};

bool test(RF24 *radio);
bool run(RF24 *radio, int16_t *data, int sizeBytes);
void toString(int *data, char *string);

bool radioNumber = 0;
byte pipes[][6] = {"1Node", "2Node"};
RF24 radio(9, 10);

byte command;
int16_t data[2] = {0, 1};
char serialData[17];

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

      case RUN:
      if(run(&radio, data, 4))
      {
        toString(data, serialData);
        Serial.println(serialData); 
      }
      else
      {
        Serial.println("timeout error.");
      }
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
      radio->stopListening();
      return false;
    }
  }

  radio->read(&request, sizeof(bool));
  radio->stopListening();
  return true;
}

bool run(RF24 *radio, int16_t *data, int sizeBytes)
{
  bool request = 1;

  radio->write(&request, sizeof(bool));
  unsigned long started_waiting_at = micros();

  radio->startListening();

  while(!radio->available())
  {
    if(micros() - started_waiting_at > 200000)
    {
      radio->stopListening();
      return false;
    }
  }

  while(radio->available())
  {
    radio->read(data, 4);
  }
  radio->stopListening();
  return true;
}

void toString(int *data, char *string)
{
  sprintf(string, "%d,%d",data[ARRAY_AMBIENT],data[ARRAY_OBJECT]);
}

