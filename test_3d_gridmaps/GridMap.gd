extends GridMap


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var noise := OpenSimplexNoise.new()

# Called when the node enters the scene tree for the first time.
func _ready()->void:
	for x in range(30):
		for y in range(30):
			for z  in range(30):
				if y < noise.get_noise_2d(x, z) * 5 + 10:
					set_cell_item(x, y, z, 0);
	pass
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
