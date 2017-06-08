# Tree-Fruit-Research-IR-Project
Raspberry pi/Arduino project for the WSU TreeFruit Research Center

This is a project done for the WSU Tree Fruit Research Center to transmit IR tempurature data from the fields to a Windows 10 computer.
The project consists of 3 separate programs: The irpi, arduino, and TFREC IR App.

REQUIRED HARDWARE:
  - A Raspberry Pi 3
  - Preferably a case for the pi to protect it. We used a Pelican 1050
  - Female to Female Breadboard Jumper Wires for connection x 18
  - A soldering iron to solder parts together
  - nRF24l01+ RF transmitters/recievers for communication x 2 (  https://www.amazon.com/Makerfocus-Wireless-NRF24L01-Antistatic-Compatible/dp/B01IK78PQA/ref=sr_1_3?s=electronics&ie=UTF8&qid=1496954930&sr=1-3&keywords=nrf24l01)
  - An MLX90614 IR Sensor(https://www.amazon.com/UCTRONICS-MLX90614-Contact-Thermometer-MLX90614ESF-BAA-000-TU/dp/B01DILCUE6/ref=sr_1_1?ie=UTF8&qid=1496955487&sr=8-1&keywords=mlx90614)
  -An Arduino Nano
  -A windows 10 PC
  
  DEPENDENCIES:
    -The bcm2835 library for i2c communication:(http://www.airspayce.com/mikem/bcm2835/)
    -The RF24 library for communication with nrf24l01+: (https://github.com/nRF24/RF24)
    -.Net 4.5 or later
    -The Arduino IDE
