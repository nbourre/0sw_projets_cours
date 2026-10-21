extends Control


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	$VBoxContainer/StartButton.grab_focus()

func _on_StartButton_pressed() -> void:
	get_tree().change_scene("res://sceneTest.tscn")

func _on_OptionsButton_pressed() -> void:
	var options = preload("res://optionsMenu.tscn").instantiate()
	get_tree().current_scene.add_child(options)

func _on_QuitButton_pressed() -> void:
	get_tree().quit()
