class_name PlayerData
extends Resource

@export var health := 100
@export var inventory := []

func change_health(value : int) -> void :
	health += value
	
func add_item_to_inventory(item_name : String) -> void:
	var item := Item.new(item_name)
	inventory.append(item)
