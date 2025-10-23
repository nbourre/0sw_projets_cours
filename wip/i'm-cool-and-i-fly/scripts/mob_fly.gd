extends BaseState
class_name MobFly

@export var mob : GenericCharacter
var anim_player : AnimationPlayer

func enter():
	if mob != null :
		anim_player = mob.get_animation_player()	
	
func update(delta: float) -> void:
	if mob == null : return
	
	if not anim_player :
		anim_player = mob.get_animation_player()
	
func physics_update(delta: float) -> void:
	if not anim_player : return
		
	anim_player.play("fly")
