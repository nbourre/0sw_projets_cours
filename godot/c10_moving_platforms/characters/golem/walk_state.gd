class_name WalkState
extends CharacterState

var player
var MAX_VELOCITY = 13;
var floor_detector : RayCast2D
var player_detector : RayCast2D
var new_velocity : bool = false
var walk_time : float
var next_dir : bool

func get_player() -> CharacterBody2D:
	return get_parent().get_parent().get_parent().get_node("Player") as CharacterBody2D
	
func change_direction() -> void:
	character.direction *= -1
	floor_detector.target_position.x *= -1
	player_detector.target_position.x *= -1
	
func randomize_walk_time():
	player = get_player()
	walk_time = randf_range(3, 6)

func handle_player():
	if (player_detector.is_colliding()) :
		var collider = player_detector.get_collider()
		
		if collider == player:
			# "Player detected!
			Transitioned.emit(self, "dash")
			return

func enter() -> void:
	character.get_animation_player().play("walk")
	player = get_player()
	floor_detector = character.get_node("FloorDetector") as RayCast2D
	player_detector = character.get_node("PlayerDetector") as RayCast2D
	randomize_walk_time()
	# print("Golem : Walk")

func update(delta: float) -> void:
	if not player:
		return
	
	if (walk_time > 0) :
		walk_time -= delta
	else :
		Transitioned.emit(self, "idle")
		return
	
	handle_player()
	
	character.velocity = character.direction * MAX_VELOCITY
	character.move_and_slide()
	
	if not floor_detector.is_colliding():
		change_direction()

func physics_update(delta: float) -> void:
	pass
