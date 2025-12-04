extends Node2D

@onready var img = $Icon
@onready var label : Label = $Label

var move_speed : float = 10;

func _input(event: InputEvent) -> void:
	if event is InputEventJoypadButton :
		var j = (event as InputEventJoypadButton)
		var device_id :=	event.device
		label.text = "Device id : " + str(device_id) + " -- " + j.as_text()

	if event.is_action_pressed("hotkey") :
		label.text = "hotkey"
	
	if event.is_action_pressed("quit") :
		label.text = "quit"
	
	
func is_on_arcade() -> bool:
	return OS.get_executable_path().to_lower().contains("retropie")

func manage_end_game() -> void:
	if is_on_arcade() :
		if Input.is_action_pressed("hotkey") and Input.is_action_pressed("quit"):
			get_tree().quit()
	else :
		if Input.is_action_just_pressed("quit"):
			get_tree().quit()
			
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	label.text = "OS : " + OS.get_executable_path() # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	manage_end_game()
			
	var dir  = Input.get_vector("ui_left", "ui_right", "ui_up", "ui_down").normalized()
	
	
	
	if dir.length() > 0 :
		img.position += move_speed * dir
	
	
