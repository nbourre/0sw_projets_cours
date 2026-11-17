shader_type canvas_item;

uniform sampler2D noise_texture : repeat_enable;
uniform float amplitude : hint_range(0.0, 0.5, 0.01) = 0.2;
uniform float time_scale : hint_range(0.0, 5.0, 0.01) = 0.04;
uniform float noise_scale : hint_range(0.0, 2.0, 0.0001) = 0.001;
uniform float rotation_strength : hint_range(0.0, 5.0, 0.1) = 1.0;
uniform vec2 rotation_pivot = vec2(0.5, 1.0); // Pied de l'arbre (centre-bas)

// Variable transmise de vertex() à fragment()
varying vec2 world_position;

void vertex() {
    // Position du sommet dans le monde
    world_position = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).xy;
}

// Fait pivoter un point autour d'un pivot
vec2 rotate_vec(vec2 vec, vec2 pivot, float rotation) {
    float cosa = cos(rotation);
    float sina = sin(rotation);
    vec -= pivot;
    return vec2(
        cosa * vec.x - sina * vec.y,
        cosa * vec.y + sina * vec.x
    ) + pivot;
}

void fragment() {
    // 1. Lire le bruit. La position et le temps le font « défiler ».
    vec2 noise_pos = world_position * noise_scale + TIME * time_scale;
    float noise_amount = texture(noise_texture, noise_pos).r - 0.5;

    // 2. Angle de rotation selon le bruit (entre -0.5 et 0.5)
    float rotation = amplitude * noise_amount;
    vec2 rotated_uv = rotate_vec(UV, rotation_pivot, rotation);

    // 3. Plus on s'éloigne du pivot, plus la rotation compte
    float dist = distance(UV, rotation_pivot) * rotation_strength;
    vec2 result_uv = mix(UV, rotated_uv, dist);

    // 4. Lire la texture à la nouvelle position
    COLOR = texture(TEXTURE, result_uv);
}