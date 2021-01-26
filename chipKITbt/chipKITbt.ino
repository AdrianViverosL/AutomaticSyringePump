#include <SoftwareSerial.h>

SoftwareSerial portOne(10,11);

char incomingByte = 0; // for incoming serial data

void setup() {

  pinMode(2, OUTPUT);
  pinMode(12, OUTPUT);
  pinMode(13, OUTPUT);
  
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }
  portOne.begin(9600);
}

void loop() {
  // reply only when you receive data:

  /*digitalWrite(2, LOW);
  digitalWrite(12, LOW);
  digitalWrite(13, LOW);*/
  
  if (portOne.available() > 0) {
    // read the incoming byte:
    incomingByte = portOne.read();

    // say what you got:
    Serial.print("I received: ");
    Serial.println(incomingByte);


    if(incomingByte == 'a'){
            //Serial.println("Encendiendo");
            digitalWrite(2, HIGH);
            digitalWrite(12, LOW);
            digitalWrite(13, LOW);
          }
    else if(incomingByte == 'b'){
            //Serial.println("Apagando");
            digitalWrite(2, LOW);
            digitalWrite(12, HIGH);
            digitalWrite(13, LOW);
          }
    else if(incomingByte == 'c'){
            //Serial.println("Apagando");
            digitalWrite(2, LOW);
            digitalWrite(12, LOW);
            digitalWrite(13, HIGH);
          }
    else{
        digitalWrite(2, LOW);
        digitalWrite(12, LOW);
        digitalWrite(13, LOW);
    }
  }

}
