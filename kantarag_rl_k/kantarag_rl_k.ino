
long randNumber;
char t;

void setup()
{ 
  Serial.begin(9600);
}
void loop(){
  if(Serial.available()>0)
  t=Serial.read();
  if(t=='1')
  {
    // 10'dan 19'a kadar rastgele bir sayı bas
  randNumber = random(2000, 3000);
  Serial.println(randNumber);
  delay(50);
  } 
}
