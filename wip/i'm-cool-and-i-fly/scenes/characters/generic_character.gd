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
