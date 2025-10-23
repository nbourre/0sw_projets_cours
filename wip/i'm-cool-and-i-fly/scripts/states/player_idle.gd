extends BaseState
class_name PlayerIdle

@export var player : GenericCharacter
var anim_player : AnimationPlayer

func manage_input() -> void:	
	var dir : Vector2 = Input.get_vector("left", "right", "up", "down").normalized()
	
	if (dir.x > 0):
		Transitioned.emit(self, "forward")
	if (dir.x < 0):
		Transitioned.emit(self, "backward")
	if (dir.y < 0):
		Transitioned.emit(self, "up")
	if (dir.y > 0):
		Transitioned.emit(self, "down")


func enter():
	anim_player = player.get_animation_player()	
	
func update(delta: float) -> void:
	if not anim_player :
		anim_player = player.get_animation_player()
	manage_input()
	
func physics_update(delta: float) -> void:
	if not anim_player : return
		
	anim_player.play("idle")
