extends BaseState
class_name PlayerUp

@export var player : GenericCharacter
var anim_player : AnimationPlayer

var MAX_SPEED : float = 200;

func manage_input() -> Vector2:		
	var dir : Vector2 = Input.get_vector("left", "right", "up", "down").normalized()
	
	if (dir.length() == 0):
		Transitioned.emit(self, "idle")		
		
	if (dir.x > 0):
		Transitioned.emit(self, "forward")
	if (dir.x < 0):
		Transitioned.emit(self, "backward")
	if (dir.y > 0):
		Transitioned.emit(self, "down")
		
	return dir

func enter():
	anim_player = player.get_animation_player()	
	
func update(delta: float) -> void:
	if not anim_player :
		anim_player = player.get_animation_player()
		
	var dir := manage_input()
	
	player.velocity = dir * (MAX_SPEED)
	
func physics_update(delta: float) -> void:
	if not anim_player : return
		
	anim_player.play("up")
	
