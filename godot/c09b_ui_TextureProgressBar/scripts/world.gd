class_name World
extends Node2D

@onready var player : Player = $Player
@onready var level : LevelOne = $Level

#region Arcade stuff
func is_on_arcade() -> bool:
	return OS.get_executable_path().to_lower().contains("retropie")

func manage_end_game() -> void:
	if is_on_arcade() :
		if Input.is_action_pressed("hotkey") and Input.is_action_pressed("quit"):
			get_tree().quit()
	else :
		if Input.is_action_just_pressed("quit"):
			get_tree().quit()
#endregion

func manage_limits() -> void:
	player.position.y = clamp(player.position.y, level.top, level.bottom)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	manage_end_game()
	manage_limits()
	
