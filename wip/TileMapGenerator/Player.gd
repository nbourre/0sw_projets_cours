extends CharacterBody2D

const GRAVITY = 35
const JUMP_FORCE = 700
const SPEED = 300
const ACCEL = 0.4

@onready var sprite = $Sprite2D
var state_machine
var is_dead = false
var dir = 0
var facing_right = 1

var velocity = Vector2.ZERO

var knockback = 1000
var knockup = -750

func _ready():
	state_machine = $AnimationTree.get("parameters/playback")

func _physics_process(_delta):
	
	if is_dead:
		return

	dir = Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	if dir != 0:
		sprite.scale.x = dir
		facing_right = dir
	
	if is_on_floor():
		if dir != 0:
			state_machine.travel("Run")
		else:
			state_machine.travel("Idle")
		
		if Input.is_action_just_pressed("jump"):
			velocity.y = -JUMP_FORCE
			state_machine.travel("Jump")
		
		if Input.is_action_pressed("Attack_1"):
			state_machine.travel("Attack_1")
			return
		
		if Input.is_action_pressed("Attack_2"):
			state_machine.travel("Attack_2")
			return
			
		if Input.is_action_pressed("Attack_3"):
			state_machine.travel("Attack_3")
			return

		if Input.is_action_just_pressed("Suicide"):
			state_machine.travel("Dead")
			is_dead = true
			return
	else:
		if velocity.y > 0:
			state_machine.travel("Falling")
		
	
	velocity.y += GRAVITY
	velocity.x = lerp(velocity.x, SPEED * dir, ACCEL)
	set_velocity(velocity)
	set_up_direction(Vector2.UP)
	move_and_slide()
	velocity = velocity;

func _on_Hurtbox_area_entered(area:Area2D):
	if (area.is_in_group("enemy_hitbox")):
		velocity.x = lerp(velocity.x, knockback * -facing_right, 0.5)
		velocity.y = lerp(0, knockup, 0.6)
		set_velocity(velocity)
		set_up_direction(Vector2.UP)
		move_and_slide()
		velocity = velocity;
		blink()

func blink():
	$Hurtbox.set_deferred("monitoring", false)
	$Hurtbox.set_deferred("monitorable", false)
	
	sprite.visible = false
	await get_tree().create_timer(0.05).timeout
	sprite.visible = true
	await get_tree().create_timer(0.07).timeout
	sprite.visible = false
	await get_tree().create_timer(0.1).timeout
	sprite.visible = true

	$Hurtbox.set_deferred("monitoring", true)
	$Hurtbox.set_deferred("monitorable", true)