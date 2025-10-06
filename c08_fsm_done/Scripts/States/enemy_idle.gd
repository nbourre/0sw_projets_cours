extends BaseState
class_name EnemyIdle

@export var enemy : DinoEnemy
@export var move_speed := 10.0

var move_direction : Vector2
var wander_time : float

var player : Player

func randomize_wander():
	move_direction = Vector2(randf_range(-1, 1), randf_range(-1, 1)).normalized()
	wander_time = randf_range(1, 3)

func enter():
	randomize_wander()

func fetch_player():
	if not player :
		player = get_tree().get_first_node_in_group("Player")

func update(delta: float) -> void:
	fetch_player()
	
	if wander_time > 0 :
		wander_time -= delta	
	else:
		randomize_wander()

func physics_update(delta: float) -> void:
	if enemy:
		enemy.velocity = move_direction * move_speed
	
	if not player : return
	
	var dir = player.global_position - enemy.global_position
	
	if (dir.length() < 30) :
		Transitioned.emit(self, "Follow")
