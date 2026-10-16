class_name CharacterState
extends State

@export var character : GenericCharacter


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if (GenericCharacter == null) :
		printerr("character cannot be null in " + self.name)
		


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
