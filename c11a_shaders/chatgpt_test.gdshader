shader_type canvas_item;

void vertex() {
    float noise_factor = sin(VERTEX.x * 5.0 + TIME * 2.0);
    VERTEX.y += noise_factor * 5.0;
}