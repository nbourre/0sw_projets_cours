extends BaseState
class_name PlayerHit

@export var player : GenericCharacter
var anim_player : AnimationPlayer
var timer : Timer
var MAX_SPEED : float = 200;

func manage_input() -> Vector2:		
	var dir : Vector2 = Input.get_vector("left", "right", "up", "down").normalized()
		
	return dir

func enter():
	timer = Timer.new()
	
	anim_player = player.get_animation_player()	
	
func update(delta: float) -> void:
	if not anim_player :
		anim_player = player.get_animation_player()
		
	var dir := manage_input()
	
	player.velocity = dir * (MAX_SPEED)
	
func physics_update(delta: float) -> void:
	if not anim_player : return
		
	anim_player.play("hit")
	
