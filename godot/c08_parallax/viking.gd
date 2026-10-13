extends CharacterBody2D

# Source : https://www.youtube.com/watch?v=OUvpe9FMkLI&t=339s

const DEBUGGING_RATE := 0.5

var is_debugging := true
var max_velocity: float
var max_jump: float = -300
var max_fall_speed: float = 200
var halt_speed: float = 0.325
var acceleration: int = 10
var gravity: int = 20

var debugging_acc := 0.0
var facing_right := true
var vel := Vector2.ZERO
var input_vector := Vector2.ZERO

@onready var current_sprite: Sprite2D = $Sprite2D
@onready var anim_tree: AnimationTree = $AnimationTree
var state_machine: AnimationNodeStateMachinePlayback


func _ready() -> void:
	init_values()
	anim_tree.active = true
	state_machine = anim_tree.get("parameters/playback")


func init_values() -> void:
	max_velocity = 80 * scale.x
	max_jump = -300 * scale.y
	max_fall_speed = 200 * scale.y
	acceleration = 10 * int(scale.x)
	gravity = 20 * int(scale.y)
	halt_speed = 0.325 * scale.x


func get_input() -> Vector2:
	var v := Vector2.ZERO
	v.x = Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	v.y = Input.get_action_strength("ui_down") - Input.get_action_strength("ui_up")
	return v.normalized()


func _process(delta: float) -> void:
	debugging_acc += delta

	input_vector = get_input()

	if is_on_floor():
		vel.y = 0
		if Input.is_action_just_pressed("jump"):
			vel.y = max_jump
			print("Jumping")
			print("Velocity.Y : ", vel.y)
		else:
			if input_vector != Vector2.ZERO:
				state_machine.travel("run")
			else:
				state_machine.travel("idle")

	vel.y += gravity

	if vel.y > max_fall_speed:
		vel.y = max_fall_speed

	if input_vector.x > 0:
		facing_right = true
		current_sprite.flip_h = false
	elif input_vector.x < 0:
		facing_right = false
		current_sprite.flip_h = true

	if input_vector == Vector2.ZERO:
		vel = vel.lerp(Vector2.ZERO, 0.2)
	else:
		vel.x += acceleration * input_vector.x

	if not is_on_floor():
		if vel.y < 0:
			print("rising")
			state_machine.travel("jump")
		if vel.y > 0:
			print("falling")
			state_machine.travel("fall")

	if absf(vel.x) < 0.01:
		vel.x = 0

	vel.x = clampf(vel.x, -max_velocity, max_velocity)

	velocity = vel
	move_and_slide()

	if debugging_acc >= DEBUGGING_RATE:
		debugging_acc -= DEBUGGING_RATE
		debugging()


func debugging() -> void:
	if not is_debugging:
		return
	print("Velocity : (", vel.x, ", ", vel.y, ")")
