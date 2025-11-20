shader_type canvas_item;

uniform float scale : hint_range(0.0, 1.0, 0.1)= 0.1;

// Need to pass as a varying because VERTEX is in a different coordinate space
// in fragment() and vertex()
varying vec2 vertex_vertex;

void vertex() {
    // Pass as a varying.
    vertex_vertex = VERTEX;
}

void fragment() {
    // Multiply by scale to see a repeating pattern. 
    // If scale is 0.1, the pattern repeats every 10 local-space units (pixels, in 2D)
    vec2 fractional_vertex = fract(vertex_vertex * scale);
    
    // Output that as color. The first two channels are the coordinates, and the
    // last channel is alpha.
    COLOR = vec4(fractional_vertex, 0.0, 1.0);
    
    // To see where the origin is, try this one.
    // It renders positive coordinates as a color and negative coordinates as black. 
    // COLOR = vec4(vertex_vertex, 0.0, 1.0);
}