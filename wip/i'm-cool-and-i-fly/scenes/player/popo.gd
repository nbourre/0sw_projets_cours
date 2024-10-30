class_name Player
extends GenericCharacter

signal PlayerHit(value : int, max_value : int)
var hp : int = 100
var max_hp : int = 100

@export var bullet_scene : PackedScene = load("res://scenes/projectiles/bullet.tscn")

func _ready() -> void:
	anim_player = $AnimationPlayer
	sprite = $Sprite2D
	anim_player.play("idle")

func _physics_process(delta: float) -> void:
	if Input.is_action_just_pressed("shoot"):
		shoot()
		
	move_and_slide()
	
func shoot():
	var b = bullet_scene.instantiate()
	get_parent().add_child(b)
	b.global_transform = $Marker2D.global_transform
	
