class_name Item
extends Resource

@export var item_name := "undefined"

func _init(itemName = "undefined") -> void:
	item_name = itemName
