class_name State
extends Node

signal Transitioned

# Receives events from the `_unhandled_input()` callback.
func handle_inputs(input_event: InputEvent) -> void:
	return
	
func update(delta: float) -> void:
	pass

func physics_update(delta: float) -> void:
	pass

func enter() -> void:
	pass

func exit() -> void:
	pass
