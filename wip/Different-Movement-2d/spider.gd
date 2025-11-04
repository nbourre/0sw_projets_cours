#  Spider.gd
class_name Spider
extends CharacterBody2D

var speed : float = 100.0

@onready var animation_player : AnimationPlayer = $AnimationPlayer


var sprites := DirectionalSpriteSet.new()

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	sprites.set_character("res://assets/spritesheets/spider")
	animation_player.root_node = get_path()
	# Example: Walk sheets are grids of 256x256 at 12 fps
	sprites.build_animplayer_for_action(animation_player, "Walk", Vector2i(256, 256), 12.0)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta: float) -> void:
	var debug : bool = false

	var mouse := get_local_mouse_position()

	if (Input.is_action_pressed("left_mouse") and mouse.length() > 50):
		position += mouse.normalized() * speed * delta
		move_and_slide()

	var angle_rad = mouse.angle()
	var anim_name = sprites.animation_name_for("Walk", angle_rad)
	if anim_name != "" and animation_player.current_animation != anim_name:
		animation_player.play(anim_name)

