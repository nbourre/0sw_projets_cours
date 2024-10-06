extends GenericCharacter
class_name DinoEnemy


func _ready() -> void:
	anim_player = $AnimationPlayer
	sprite = $Sprite
	anim_player.play("idle")

func _physics_process(delta: float) -> void:
	move_and_slide()
	
	if velocity.length() > 0:
		anim_player.play("run")
	
	if (velocity.x > 0) :
		sprite.flip_h = false
	else:
		sprite.flip_h = true
