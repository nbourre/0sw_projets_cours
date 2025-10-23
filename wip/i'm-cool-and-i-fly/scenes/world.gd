extends Node2D

@onready var player : Player = $Player
@onready var level : LevelOne = $Level

@onready var hp_bar : GenericProgressBar = $CanvasLayer/GenericProgressBar

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
	hp_bar.bar_name = "HP"
	hp_bar.update_value(100, 100)
	player.PlayerHit.connect(hp_bar.update_value)
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	manage_end_game()
	manage_limits()
