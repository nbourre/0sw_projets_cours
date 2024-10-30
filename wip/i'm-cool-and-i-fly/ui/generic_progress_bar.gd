@tool
class_name GenericProgressBar
extends TextureProgressBar

@export var bar_name : String:
	get:
		return bar_name
	set(value):
		if value != bar_name:
			bar_name = value
					
@onready var text : Label = $BarText

func update_value (new_value : int, max : int):
	max_value = max
	value = new_value
	text.text = str(bar_name, " : ", int(value), " / ", int (max))

func _process(delta: float) -> void:
	if Engine.is_editor_hint():
		text.text = str(bar_name, " : 100 / 100")
