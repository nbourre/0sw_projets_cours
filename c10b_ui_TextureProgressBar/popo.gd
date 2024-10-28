extends GenericCharacter
class_name Player

signal PlayerHit(value : int, max_value : int)

func _ready() -> void:
	anim_player = $AnimationPlayer
	sprite = $Sprite2D
	anim_player.play("idle")

func _physics_process(delta: float) -> void:
	move_and_slide()
	
