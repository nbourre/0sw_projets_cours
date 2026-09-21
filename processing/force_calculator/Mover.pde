
class Mover extends GraphicObject {

  float topSpeed;
  float mass;
  float radiusFactor = 16;
  float diameter;
  float radius;
  float elasticity = 0.8;
  
  float angle = 0;
  float angleAcceleration = 0;
  
  // Type de vérification des bordures
  // 0 = Aucune, 1 = Rebond, 2 = Réapparition
  int checkEdgeType = 0;
    
  Mover () {
    
    
    this.velocity = new PVector (0, 0);
    this.acceleration = new PVector (0 , 0);
    
    setMass(1.0);
    
    this.location = new PVector (random (radius, width - radius), random (radius, height - radius));
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
  
  void setCheckEdgeType (int _type) {
    checkEdgeType = _type;
  }
  
  
  private void updateRadius() {
    this.diameter = mass * radiusFactor;
    this.radius = diameter / 2;
  }
  
  
  void update (int deltaTime) {
    checkEdges();
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
    boolean depasseDroite = location.x + radius > width;
    boolean depasseGauche = location.x - radius < 0;
    
    if (depasseDroite || depasseGauche) {
      if (checkEdgeType == 1) {
        velocity.x = -velocity.x;
      } else if (checkEdgeType == 2) {
        location.x = depasseDroite ? radius : width - radius;
      }
    }
    
    boolean depasseBas = location.y + radius > height;
    boolean depasseHaut = location.y - radius < 0;

    
    if (depasseBas || depasseHaut) {
      if (checkEdgeType == 1) {
        velocity.y = -velocity.y;
      } else if (checkEdgeType == 2) {
        location.y = depasseBas ? radius : height - radius;
      }
    }
  }
  
  
  void applyForce (PVector force) {
    PVector f = PVector.div (force, mass);
   
    this.acceleration.add(f);
  }

}
