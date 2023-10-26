extends CharacterBody2D

@export var speed = 1000
@export var acceleration = 0.1
@export var friction = 0.05

# TODO : Ajouter timer au jeu
var bullet_speed = 1000
var bullet = preload("res://Bullet.tscn")

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
	
	if (Input.is_action_just_pressed("fire")) :
		fire()
	
func fire():
	
	var bullet_instance = bullet.instantiate()
	bullet_instance.position = get_global_position() + (Vector2.from_angle(rotation) * 25)
	bullet_instance.rotation_degrees = rotation_degrees
	bullet_instance.apply_impulse(Vector2(bullet_speed, 0).rotated(rotation))
	get_tree().get_root().call_deferred("add_child", bullet_instance)

func _on_area_2d_area_entered(area):
	print(area.get_parent().name)
	if "Enemy" in area.get_parent().name :
		var _tmp = get_tree().reload_current_scene()
