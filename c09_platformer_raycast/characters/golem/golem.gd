class_name Golem
extends GenericCharacter

var gravity : float = ProjectSettings.get_setting("physics/2d/default_gravity")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	anim_player = $AnimationPlayer
	sprite = $Sprite2D
	anim_player.play("idle")
	direction = Vector2(scale.x, 0)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	handle_gravity(delta)
	
		
	if (velocity.x > 0):
		sprite.flip_h = false
	elif (velocity.x < 0):
		sprite.flip_h = true
		
func handle_gravity(delta : float):
	if not is_on_floor():
		velocity.y += gravity * delta
	
