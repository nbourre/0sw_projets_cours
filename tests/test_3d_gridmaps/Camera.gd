extends Camera


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	Input.set_mouse_mode((Input.MOUSE_MODE_CAPTURED))
	pass # Replace with function body.

func _process(delta):
	translation -= global_transform.basis.z * delta * 10
	
func _input(event):
	if event is InputEventMouseMotion:
		rotation_degrees.y -= event.relative.x * 0.3
		rotation_degrees.x -= event.relative.y * 0.3
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
