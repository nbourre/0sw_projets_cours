extends Node2D

var mob_scene : PackedScene = preload("res://mob.tscn")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	input_management()
	
func input_management():
	if (Input.is_action_just_pressed("spawn")):
		spawn_mob()

func spawn_mob():
	var mob = mob_scene.instantiate()
	
	var mob_sprite = mob.get_node("AnimatedSprite2D") as AnimatedSprite2D
	var mob_width = 64
	
	var direction = randf_range(-1, 1)
	
	var screen_size = get_viewport_rect().size
	var screen_offset = get_viewport().get_visible_rect()
	var random_x = 0.0
	var random_y = 0.0
	
	if direction > 0:
		random_x = -mob_width + screen_offset.position.x
		random_y = randf_range(0, screen_size.y / 2)
		mob.velocity.x = abs(mob.velocity.x)
	else :
		random_x = screen_size.x + mob_width + screen_offset.position.x
		random_y = randf_range(0, screen_size.y / 2)
		mob.velocity.x = -abs(mob.velocity.x)
	
	mob.position = Vector2(random_x, random_y)
	add_child(mob)
