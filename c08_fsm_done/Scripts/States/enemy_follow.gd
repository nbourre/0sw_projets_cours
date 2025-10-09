extends BaseState
class_name EnemyFollow

@export var enemy : DinoEnemy
@export var move_speed := 40.0

var player : Player

var move_direction : Vector2
var wander_time : float

func enter():
	player = get_tree().get_first_node_in_group("Player")

func update(delta: float) -> void:
	pass

func physics_update(delta: float) -> void:
	if not player :
		Transitioned.emit(self, "idle")
		return
	
	var dir = player.global_position - enemy.global_position
	
	if dir.length() > 25:
		enemy.velocity = dir.normalized() * move_speed
	else :
		enemy.velocity = Vector2()
		
	if dir.length() > 50 :
		Transitioned.emit(self, "idle")
	
	
