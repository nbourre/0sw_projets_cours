
class Mover extends GraphicObject {

  float topSpeed;
  float mass;
  float radiusFactor = 16;
  float diameter;
  float radius;
  float elasticity = 0.8;
  
  float angle = 0;
  float angleAcceleration = 0;
    
  Mover () {
    
    this.location = new PVector (random (width), random (height));    
    this.velocity = new PVector (0, 0);
    this.acceleration = new PVector (0 , 0);
    
    setMass(1.0);
  }
  
  Mover (PVector loc, PVector vel) {
    this.location = loc;
    this.velocity = vel;
    this.acceleration = new PVector (0 , 0);
    
    this.topSpeed = 100;
    setMass(1.0);
  }
  
  Mover (float x, float y, float m) {
    
    location = new PVector (x, y);
    
    velocity = new PVector(0, 0);
    acceleration = new PVector(0, 0);
    
    setMass(m);
  }
  
  void setMass(float m) {
    this.mass = m;
    updateRadius();
  }
  
  void setColor (color c) {
    fillColor = c;
  }
  
  
  private void updateRadius() {
    this.diameter = mass * radiusFactor;
    this.radius = diameter / 2;
  }
  
  
  void update (int deltaTime) {
    velocity.add (acceleration);
    location.add (velocity);

    acceleration.mult (0);
    
    // Permet de faire pivoter le mover
    angle += map (velocity.mag(), 0, 10, 0, 1) * ( velocity.x < 0 ? -1 : 1 );
  }
  
  void display () {
    pushMatrix();
    
    translate(location.x, location.y);
    
    
    strokeWeight(1);
    stroke (0);
    fill (fillColor);
    
    ellipse (0, 0, diameter, diameter); // Dimension à l'échelle de la masse
    
    popMatrix();
  }
  
  void checkEdges() {
    if (location.x + radius > width) {
      location.x = width - radius;
      velocity.x *= -1;
    } else if (location.x < radius) {
      velocity.x *= -1;
      location.x = radius;
    }
    
    if (location.y + radius > height) {
      if (abs(velocity.y) > 0.05) {
        velocity.y *= -elasticity;
      } else {
        velocity.y = 0;
      }
        
      location.y = height - radius;
    }
  }
  
  
  void applyForce (PVector force) {
    PVector f = PVector.div (force, mass);
   
    this.acceleration.add(f);
  }
  
  // Voir : https://natureofcode.com/book/chapter-2-forces/#chapter02_section9
  PVector attractionForce(Mover m) {
    PVector force = PVector.sub(location, m.location);
    float distance = force.mag();
    distance = constrain (distance, 5.0, 25.0);
    
    // Vecteur unitaire 'r'
    force.normalize();
    
    float strength = (.1 * mass * m.mass) / (distance * distance);
    force.mult(strength);
    
    return force;
  }
}
