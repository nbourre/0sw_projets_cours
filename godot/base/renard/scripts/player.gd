extends CharacterBody2D


var speed : float = 300.0
var jump_velocity : float = -400.0

var run_flag : bool = false

@onready var animation_player = $AnimationPlayer

func _input(event: InputEvent) -> void:
	if event is InputEventKey:
		if event.is_action_pressed("run"):
			run_flag = true
		elif event.is_action_released("run"):
			run_flag = false
		
		print(run_flag)

func _physics_process(delta: float) -> void:
	# Add the gravity.
	if not is_on_floor():
		velocity += get_gravity() * delta

	# Handle jump.
	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = jump_velocity

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction := Input.get_axis("left", "right")
	if direction:
		
		if run_flag:
			animation_player.play("run")
			speed = 500.0
		elif not run_flag:
			animation_player.play("walk")
			speed = 300.0

		velocity.x = direction * speed
	else:
		velocity.x = move_toward(velocity.x, 0, speed)
		animation_player.play("idle1")


	if direction > 0:
		$Sprite2D.flip_h = false
	elif direction < 0:
		$Sprite2D.flip_h = true

	move_and_slide()
