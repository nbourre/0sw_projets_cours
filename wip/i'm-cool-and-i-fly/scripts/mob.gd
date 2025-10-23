class_name Mob
extends GenericCharacter

signal PlayerHit(value : int, max_value : int)
var hp : int = 100
var max_hp : int = 100

@onready var hurt_box : Area2D = $Area2D

func _ready() -> void:
	anim_player = $AnimationPlayer
	sprite = $Sprite2D
	anim_player.play("fly")

func _physics_process(_delta: float) -> void:
	if velocity.x > 0 :
		sprite.flip_h = false
	elif velocity.x < 0 :
		sprite.flip_h = true
		
	move_and_slide()

func is_touched() -> bool:
	return false


func _on_area_2d_body_entered(body: Node2D) -> void:
	if body is Player:
		(body as Player).PlayerDied.emit()


func _on_area_2d_area_entered(area: Area2D) -> void:
	if area.get_parent() is Bullet :
		queue_free()
