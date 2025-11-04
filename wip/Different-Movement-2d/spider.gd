#  Spider.gd
class_name Spider
extends CharacterBody2D

var angle_increment : int = 30
var current_angle : float = 0.0

var speed : float = 100.0

# Create a collection to hold references to each angle of the animation spritesheet.
var animation_frames: Array = []
var animation_frames_shadow: Array = []

const AVAILABLE_ANGLES = [0, 30, 45, 60, 90, 120, 135, 150, 180, 210, 225, 240, 270, 300, 315, 330]

var current_animation : String = "walk"
var animation_idx : int = 0
var animation_slices : float = 1.0
var previous_animation_idx : int = -1

@onready var body_sprite : Sprite2D = $Body
@onready var shadow_sprite : Sprite2D = $Shadow
@onready var animation_player : AnimationPlayer = $AnimationPlayer

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	animation_slices = 360.0 / angle_increment
	preload_all_frames()

func preload_all_frames() -> void:
	await get_tree().create_timer(0.1).timeout  # Small delay to ensure loading is complete.

	var number_string = "0"
	# Load the animation frames into the array.
	for i in AVAILABLE_ANGLES:
		if (i == 0):
			number_string = "0"
		else :
			number_string = "%03d" % i

		var body_path = "res://assets/spritesheets/spider/Walk/Walk_Body_" + number_string + ".png"
		var shadow_path = "res://assets/spritesheets/spider/Walk/Walk_Shadow_" + number_string + ".png"

		var body = load(body_path)
		var shadow = load(shadow_path)

		if body and shadow:
			animation_frames.append(body)
			animation_frames_shadow.append(shadow)
		else:
			push_warning("Failed to load sprite at angle: " + str(i))

func get_closest_angle_index(target_degree: float) -> int:
	if AVAILABLE_ANGLES.is_empty():
		return 0
	target_degree = fposmod(target_degree + 360.0, 360.0)
	var best_idx := 0
	var best_diff := 9999.0
	for i in range(AVAILABLE_ANGLES.size()):
		var a := float(AVAILABLE_ANGLES[i])
		var diff := absf(fposmod(target_degree - a + 180.0, 360.0) - 180.0)
		if diff < best_diff:
			best_diff = diff
			best_idx = i
	return best_idx

func get_frame_for_angle(angle: float) -> Texture2D:
	# Normalize the angle to be within 0-360 degrees.
	angle = fmod(angle + 360.0, 360.0)
	
	# Determine the index based on the angle and angle increment.
	var index = int(round(angle / angle_increment)) % animation_frames.size()
	return animation_frames[index]

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta: float) -> void:
	var debug : bool = false

	if not animation_frames.size():
		return

	# Récupère la position de la souris par rapport au spider
	var mouse = get_local_mouse_position()

	# Adjustement pour l'orientation
	var angle = mouse.angle() + PI / 2.0  

	# Calcule l'index de l'animation en fonction de l'angle
	# TAU = 2 * PI
	# var step = TAU / animation_slices
	# animation_idx = int(snappedf(angle, step) / step)
	# animation_idx = wrapi(animation_idx, 0, int(animation_slices))
	animation_idx = get_closest_angle_index(rad_to_deg(angle))


	if (Input.is_action_pressed("left_mouse") and mouse.length() > 50):
		position += mouse.normalized() * speed * delta
		move_and_slide()

	if animation_idx != previous_animation_idx:
		previous_animation_idx = animation_idx
		print("Animation frames size: ", animation_frames.size())
		print("Updating animation to index: ", animation_idx)

		# Met à jour les textures des sprites en fonction de l'index calculé
		body_sprite.texture = animation_frames[animation_idx]
		shadow_sprite.texture = animation_frames_shadow[animation_idx]

	animation_player.play(current_animation)

	if debug:
		print("Angle: ", rad_to_deg(mouse.angle()), " | Animation Index: ", animation_idx, " | Current Texture: ", body_sprite.texture.resource_path)
	
	
