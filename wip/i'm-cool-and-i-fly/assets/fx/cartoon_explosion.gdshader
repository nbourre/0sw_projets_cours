shader_type canvas_item;

// Define the maximum time for the animation to stop
#define iTime TIME
#define iResolution 1.0/SCREEN_PIXEL_SIZE // Screen resolution (inverted for scaling)
#define fragColor COLOR // Fragment color output

uniform bool loop = false;

// Uniforms for color palettes
uniform vec3 boom_pal1 = vec3(.2, .15, .3);
uniform vec3 boom_pal2 = vec3(.9, .15, .05);
uniform vec3 boom_pal3 = vec3(.9, .5, .1);
uniform vec3 boom_pal4 = vec3(.95, .95, .35);

uniform vec3 smoke_pal1 = vec3(.2, .15, .3);
uniform vec3 smoke_pal2 = vec3(.35, .3, .45);
uniform vec3 smoke_pal3 = vec3(.5, .45, .6);

// Controls for the explosion (boom) and smoke effect parameters
uniform float size : hint_range(0.1, 50.0, 0.1) = 5.0;
uniform float disperse : hint_range(0.1, 10.0, 0.1) = 1.0;
uniform float boom_repeat : hint_range(0.0, 10.0, 0.1) = 1.0;
uniform float boom_shape : hint_range(0.1, 20.0, 0.1) = 12.0;
uniform float boom_distortion : hint_range(0.0, 10.0, 0.1) = 0.5;
uniform float boom_bubbles : hint_range(0.0, 10.0, 0.1) = 0.5;
uniform float boom_bw : hint_range(0.0, 10.0, 0.1) = 0.5;

uniform float smoke_repeat : hint_range(0.0, 10.0, 0.1) = 1.0;
uniform float smoke_shape : hint_range(0.0, 30.0, 0.1) = 16.0;
uniform float smoke_distortion : hint_range(0.0, 10.0, 0.1) = 1.5;
uniform float smoke_bubbles : hint_range(0.0, 10.0, 0.1) = 0.5;
uniform float smoke_bw : hint_range(0.0, 10.0, 0.1) = 0.75;

uniform vec2 disp = vec2(0.0); // Displacement vector for effect position offset

// Function to generate a random normalized vector based on input position
vec3 n_rand3(vec3 p) {
    vec3 r = fract(
        sin(vec3(
            dot(p, vec3(127.1, 311.7, 371.8)),
            dot(p, vec3(269.5, 183.3, 456.1)),
            dot(p, vec3(352.5, 207.3, 198.67))
        )) * 43758.5453
    ) * 2.0 - 1.0;
    return normalize(vec3(r.x/cos(r.x), r.y/cos(r.y), r.z/cos(r.z)));
}

// 3D noise function to add texture to effects
float noise(vec3 p) {
    vec3 fv = fract(p);
    vec3 nv = floor(p);
    vec3 u = fv * fv * fv * (fv * (fv * 6.0 - 15.0) + 10.0);
    
    return (
        mix(
            mix(
                mix(
                    dot(n_rand3(nv + vec3(0.0, 0.0, 0.0)), fv - vec3(0.0, 0.0, 0.0)), 
                    dot(n_rand3(nv + vec3(1.0, 0.0, 0.0)), fv - vec3(1.0, 0.0, 0.0)), 
                    u.x
                ), 
                mix(
                    dot(n_rand3(nv + vec3(0.0, 1.0, 0.0)), fv - vec3(0.0, 1.0, 0.0)), 
                    dot(n_rand3(nv + vec3(1.0, 1.0, 0.0)), fv - vec3(1.0, 1.0, 0.0)), 
                    u.x
                ), 
                u.y
            ),
            mix(
                mix(
                    dot(n_rand3(nv + vec3(0.0, 0.0, 1.0)), fv - vec3(0.0, 0.0, 1.0)), 
                    dot(n_rand3(nv + vec3(1.0, 0.0, 1.0)), fv - vec3(1.0, 0.0, 1.0)), 
                    u.x
                ), 
                mix(
                    dot(n_rand3(nv + vec3(0.0, 1.0, 1.0)), fv - vec3(0.0, 1.0, 1.0)), 
                    dot(n_rand3(nv + vec3(1.0, 1.0, 1.0)), fv - vec3(1.0, 1.0, 1.0)), 
                    u.x
                ), 
                u.y
            ),
            u.z
       )
    );
}

// Worley noise function for cellular pattern, used in explosion and smoke effects
float worley(vec3 s) {
    vec3 si = floor(s);
    vec3 sf = fract(s);
    float m_dist = 1.0;  

    for (int y = -1; y <= 1; y++) {
        for (int x = -1; x <= 1; x++) {
            for (int z = -1; z <= 1; z++) {
                vec3 neighbor = vec3(float(x), float(y), float(z));
                vec3 point = fract(n_rand3(si + neighbor));
                point = 0.5 + 0.5 * sin(disperse * iTime + 6.2831 * point);
                vec3 diff = neighbor + point - sf;
                float dist = length(diff);
                m_dist = min(m_dist, dist);
            }
        }
    }
    return m_dist;
}

// Function to add octaves of noise
float oct_noise(vec3 pos, float o) {
    float ns = 0.0;
    float d = 0.0;
    int io = int(o);
    float fo = fract(o);
    
    for (int i = 0; i <= io; ++i) {
        float v = pow(2.0, float(i));
        d += 1.0 / v;
        ns += noise(pos * v) * (1.0 / v);
    }
    
    float v = pow(2.0, float(io + 1));
    d += 1.0 * fo / v;
    ns += noise(pos * v) * (1.0 * fo / v);
    
    return ns / d;
}

// Explosion (boom) effect based on position and time
float boom(vec2 p) {
    float repeat = mod(iTime * boom_repeat, 2.0);
    float shape = 1.0 - pow(distance(vec3(p, 0.0), vec3(0.0)), 2.0) / (repeat * boom_shape) - repeat * 2.0;
    
    float distortion = noise(vec3(p * boom_distortion, iTime * 0.5));
    float bubbles = boom_bubbles - pow(worley(vec3(p * 1.2, iTime * 2.0)), 3.0);
    float bw = boom_bw;
    float effects = (bw * bubbles + (1.0 - bw) * distortion);

    return shape + effects;
}

// Smoke effect based on position and time
float smoke(vec2 p) {
    float repeat = mod(iTime * smoke_repeat, 2.0);
    float shape = 1.0 - pow(distance(vec3(p - vec2(0, 2) * pow(repeat / 1.45, 2.0) * 1.5, 0.0), vec3(0.0)), 2.0) / (repeat * smoke_shape) - pow(repeat * 1.5, 0.5);
    
    float distortion = noise(vec3(p * smoke_distortion - vec2(0, 2) * pow(repeat / 1.45, 2.0) * 1.5, iTime * 0.1));
    float bubbles = smoke_bubbles - pow(worley(vec3((p / pow(repeat, 0.35)) - vec2(0, 2) * pow(repeat / 1.65, 2.0) * 1.5, iTime * 0.1)), 2.0);
    float bw = smoke_bw;
    float effects = (bw * bubbles + (1.0 - bw) * distortion);

    return shape + effects;
}

// Determines final value between boom and smoke effects
float f(vec2 p) {
    float b = boom(p);
    float s = smoke(p);
    return b > s ? b : s;
}

// Calculate gradient for borders
vec2 grad(vec2 x) {
    vec2 h = vec2(0.01, 0.0);
    return vec2(f(x + h.xy) - f(x - h.xy), f(x + h.yx) - f(x - h.yx)) / (2.0 * h.x);
}

// Compute border effects with gradients
float border(vec2 uv) {
    float b = f(uv);
    vec2 g = grad(uv);
    float de = abs(b) / length(g);
    float eps = 0.01;
    return smoothstep(1.0 * eps, 2.0 * eps, de);
}

// Posterization function to quantize values
float posterize(float v, int n) {
    float fn = float(n);
    return floor(v * fn) / (fn - 1.0);
}

// Main fragment function for rendering
void fragment() {
    // Color palettes for boom and smoke effects
    vec3 boom_pal[4] = { boom_pal1, boom_pal2, boom_pal3, boom_pal4 };
    vec3 smoke_pal[] = { smoke_pal1, smoke_pal2, smoke_pal3 };

    // Normalized UV coordinates and position adjustments
    vec2 uv = UV;
    uv.y = 1.0 - uv.y;
    vec2 pos = uv - vec2(0.5, 0.4);
    pos.x /= SCREEN_PIXEL_SIZE.x / SCREEN_PIXEL_SIZE.y;
    pos *= 1.0 * size;
    pos = pos - disp;

    int bpl = boom_pal.length();
    int spl = smoke_pal.length();

    // Calculate boom and smoke colors based on effects and positions
    float boom_val = boom(pos);
    float boom_a = step(0.0, boom_val);
    vec3 boom_col = boom_pal[int(posterize(boom_val, bpl) * float(bpl))] - vec3(1.0 - boom_a);
    
    float smoke_val = smoke(pos);
    float smoke_a = step(0.0, smoke_val);
    vec3 smoke_col = smoke_pal[int(posterize(smoke_val, spl) * float(spl))] - vec3(1.0 - smoke_a);
    
    float b = step(1.0, border(pos));
    
    float bw = step(smoke_val * 1.25, boom_val);
    
    // Final color and alpha values based on blending boom and smoke effects
    vec3 color = bw * boom_col + (1.0 - bw) * smoke_col;
    float alpha = bw * boom_a + (1.0 - bw) * smoke_a;
    
    vec4 result = vec4(alpha == 1.0 ? color : vec3(0.5), alpha) - vec4(vec3(1.0 - b), 1.0);

    // Output the final color to the fragment
    fragColor = vec4(result.rgb, alpha);
}
