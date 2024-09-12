extends Node

var noise_map;
var altitude_noise_layer = {}

@onready var terrain_tile_map = $TileMap

@export var map_size : Vector2i = Vector2i(600, 400)
@export var alt_freq : float = 0.005

@export var oct : int = 4

@export var lac : int = 2

@export var gain : float = 0.5

@export var amplitude : float = 1.0

# Called when the node enters the scene tree for the first time.
func _ready():
	altitude_noise_layer = generate_noise(randi(), alt_freq, oct, lac, gain)
	generate_biomes(map_size.x, map_size.y)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

# generate altitude noise map from map_size, seed, freq, and octave

func generate_noise(noise_seed, frequency, octaves, lacunarity, _gain):
	noise_map = FastNoiseLite.new()
	noise_map.noise_type = FastNoiseLite.TYPE_SIMPLEX
	
	noise_map.seed = noise_seed
	noise_map.frequency = frequency
	noise_map.fractal_octaves = octaves
	noise_map.fractal_lacunarity = lacunarity
	noise_map.fractal_gain = _gain
	
	var grid = {}
	
	for x in map_size.x:
		for y in map_size.y:
			var rand = abs (noise_map.get_noise_2d(x, y) * 2 - 1)
			grid[Vector2i(x, y)] = rand
	
	return grid

func generate_biomes(width, height):
# loop thru noise maps
	var tileset = terrain_tile_map.tile_set


	for x in width:
		for y in height:
			var pos = Vector2i(x, y)
			var alt = altitude_noise_layer[pos]
			
			
			
			# generate water
			if alt < 0.75:
				terrain_tile_map.set_cell(0, pos, 0, Vector2i(2,2))
			# generate shallow water
			elif alt > 0.75 and alt < 1.0:
				terrain_tile_map.set_cell(0, pos, 0, Vector2i(0,2))
			# generate everything else
			else:

				terrain_tile_map.set_cell(0, pos, 0, Vector2i(0,0))	
	
