extends Node2D

@onready var bg : TextureRect = $TextureRect

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	bg.expand_mode = TextureRect.EXPAND_IGNORE_SIZE
	bg.stretch_mode = TextureRect.STRETCH_SCALE
	get_tree().get_root().size_changed.connect(_on_screen_resized)
	_on_screen_resized()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if (Input.is_action_just_pressed("pause")):
		pause()
		print("Pause!")

func _on_screen_resized() -> void:
	bg.global_position = Vector2.ZERO
	bg.size = get_viewport_rect().size

func pause() -> void:
	$PauseMenu.pause()
