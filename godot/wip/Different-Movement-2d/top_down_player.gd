extends CharacterBody2D


const SPEED := 300.0

var acceleration := 0.1
var friction := 0.05

var dir := Vector2.ZERO

@onready var sprite_body : Sprite2D = $Body
@onready var sprite_barrel : Sprite2D = $Barrel

func _physics_process(delta: float) -> void:
	dir.x = Input.get_action_strength("right") - Input.get_action_strength("left")
	dir.y = Input.get_action_strength("down") - Input.get_action_strength("up")

	dir = dir.normalized()

	# The sprite points up, so we need to rotate it by 90 degrees
	sprite_barrel.look_at(get_global_mouse_position())
	sprite_barrel.rotation += -PI/2	

	if dir != Vector2.ZERO:
		velocity = velocity.lerp(dir * SPEED, acceleration)

		sprite_body.rotation = lerp_angle(sprite_body.rotation, dir.angle() + PI/2, 0.2)
	else:
		velocity = velocity.lerp(Vector2.ZERO, friction)


	move_and_slide()
