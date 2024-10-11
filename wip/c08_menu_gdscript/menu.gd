extends Control

var game_scene : PackedScene
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	$VBoxContainer/StartButton.grab_focus()
	game_scene = preload("res://world.tscn")

func _on_start_button_pressed() -> void:
	get_tree().change_scene_to_file("res://world.tscn")
	#à.change_scene("res://world.tscn")

func _on_option_button_pressed() -> void:
	var options = preload("res://option_menu.tscn").instantiate()
	get_tree().current_scene.add_child(options)


func _on_quit_button_pressed() -> void:
	get_tree().quit()
