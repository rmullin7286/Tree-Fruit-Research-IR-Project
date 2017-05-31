#ifndef PI_HPP
#define PI_HPP

#include <iostream>
#include <ctime>
#include <cstdlib>
#include <cstring>
#include <bcm2835.h>
#include <RF24/RF24.h>

using std::cout;
using std::endl;

#define ADDRESS 0x5a
#define AMBIENT 0x06
#define OBJECT 0x07

//radio pipe addresses for the nodes to communicate
const uint8_t pipes[][6] = {"1Node", "2Node"};

enum {ARRAY_AMBIENT, ARRAY_OBJECT};

void initialize(RF24 *radio);
void loop(RF24 radio);
void readIR(double *temps);


#endif
