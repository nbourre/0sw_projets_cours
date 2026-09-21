Mover m;

int currentTime = 0;
int previousTime = 0;
int deltaTime = 0;

void setup() {
  size (800, 600);
  
  m = new Mover();
  m.setColor(color(0, 200, 200));
}

void draw() {
  // Gestion du temps
  currentTime = millis();
  deltaTime = currentTime - previousTime;
  previousTime = currentTime;
  
  update(deltaTime);  
  display();
}

void update(int dt) {
  m.update(dt);
}

void display() {
  background(50);
  m.display();
}
