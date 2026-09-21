ForceCalculator calc;

ArrayList<Mover> movers;
final int NB_MOVERS = 10;


int currentTime = 0;
int previousTime = 0;
int deltaTime = 0;

void setup() {
  size (800, 600);
  
  calc = new ForceCalculator();
  
  movers = new ArrayList<Mover>();
  
  for (int i = 0; i < NB_MOVERS; i++) {
    Mover m = new Mover();
    m.setMass (random(1, 5));
    m.setColor(color(0, 200, 200));
    m.setCheckEdgeType(2);
    movers.add(m);
  }
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
  for (int i = 0; i < NB_MOVERS; i++) {
    var m1 = movers.get(i);
    
    var m2 = movers.get(0);
    
    if (i < NB_MOVERS - 1) {
      m2 = movers.get(i + 1);
    }
    
    PVector f = calc.attractionForce(m1.mass, m2.mass, m1.location, m2.location);
    m1.applyForce(f);
    m1.update(dt);
  }
}

void display() {
  background(50);
  
  for (int i = 0; i < NB_MOVERS; i++) {
    var m1 = movers.get(i);
    
    var m2 = movers.get(0);
    
    if (i < NB_MOVERS - 1) {
      m2 = movers.get(i + 1);
    }
    
    line (m1.location.x, m1.location.y, m2.location.x, m2.location.y);
    m1.display();
  }
}
