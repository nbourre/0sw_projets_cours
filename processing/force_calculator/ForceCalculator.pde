class ForceCalculator {
  // Voir : https://natureofcode.com/book/chapter-2-forces/#chapter02_section9
  PVector attractionForce(float mass_1, float mass_2, PVector position_1, PVector position_2) {
    float grav_constant = 0.1;
    PVector result = attractionForce (mass_1, mass_2, position_1, position_2, grav_constant);
    return result;
  }
  
  PVector attractionForce(float mass_1, float mass_2, PVector position_1, PVector position_2, float grav_constant) {
    PVector force = PVector.sub(position_1, position_2);
    float distance = force.mag();
    distance = constrain (distance, 5.0, 25.0);
    
    // Vecteur unitaire 'r'
    force.normalize();
    
    float strength = (grav_constant * mass_1 * mass_2) / (distance * distance);
    force.mult(strength);
    
    return force;
  }
}
