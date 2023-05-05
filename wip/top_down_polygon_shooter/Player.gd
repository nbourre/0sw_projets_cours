extends CharacterBody2D

@export var speed = 1000
@export var acceleration = 0.1
@export var friction = 0.05

func get_input():
	var input = Vector2()
	
	input.x = Input.get_action_strength("right") - Input.get_action_strength("left")
	input.y = Input.get_action_strength("down") - Input.get_action_strength("up")
	
	return input

func _physics_process(delta):
	var direction = get_input()
	
	direction = direction.normalized()
	
	if (direction.length() > 0):
		velocity = velocity.lerp(direction * speed, acceleration)
	else :
		velocity = velocity.lerp(Vector2.ZERO, friction)
		
	look_at(get_global_mouse_position())
	
	move_and_slide()
	
	


