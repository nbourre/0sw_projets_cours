shader_type canvas_item;

void vertex() {
    // Stocke la position originale du sommet
    vec2 og_pos = VERTEX;
    
    // Paramètres d'ondulation
    float amplitude = 100.0;  // Amplitude de l'onde
    float frequency = 5.0 * TIME;   // Fréquence de l'onde

    // Calcul de l'oscillation sur l'axe X en fonction de la position Y du sommet
    float wave_offset = sin(frequency) * amplitude;

    // Applique l'offset d'ondulation à la position X du sommet
    VERTEX.x += wave_offset;
}