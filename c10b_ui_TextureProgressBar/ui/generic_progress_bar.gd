class_name GenericProgressBar
extends TextureProgressBar


@export var bar_name : String
@onready var text : Label = $BarText

func update_value (new_value : int, max : int):
	max_value = max
	value = new_value
	text.text = str(bar_name, " : ", int(value), " / ", int (max))
