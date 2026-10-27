class_name PauseMenu
extends CanvasLayer

@onready var resume_button : Button = %ResumeButton
@onready var options_button : Button = %OptionsButton
@onready var main_menu_button : Button = %MainMenuButton
@onready var options_menu : OptionsMenu = %OptionsMenu

func _ready() -> void:
	hide()
	resume_button.pressed.connect(resume)
	options_button.pressed.connect(options_menu.open)
	main_menu_button.pressed.connect(_on_main_menu_pressed)
	options_menu.closed.connect(options_button.grab_focus)

func _unhandled_input(event : InputEvent) -> void:
	if event.is_action_pressed("ui_cancel"):
		if visible:
			resume()
		else:
			pause()
		get_viewport().set_input_as_handled()

func pause() -> void:
	get_tree().paused = true
	show()
	resume_button.grab_focus()

func resume() -> void:
	get_tree().paused = false
	options_menu.hide()
	hide()

func _on_main_menu_pressed() -> void:
	get_tree().paused = false
	get_tree().change_scene_to_file("res://scenes/menus/main_menu.tscn")
