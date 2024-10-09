extends CharacterBody2D

@export var speed = 200
var bullet_scene : PackedScene = preload("res://bullet.tscn")

func get_input():
	look_at(get_global_mouse_position())
	
	
	var dir = Input.get_vector("left", "right", "forward", "back")
	velocity = dir * speed
	
	if Input.is_action_just_pressed("shoot"):
		shoot()
	
	
func _physics_process(delta):
	get_input()
	
	move_and_slide()
	
func shoot():
	var b = bullet_scene.instantiate()
	get_parent().add_child(b)
	b.global_transform = $Muzzle.global_transform
	#b.position = $Muzzle.position
