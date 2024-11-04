extends Node2D

@onready var sprite : Sprite2D = $Icon
@onready var timer : Timer = $Flashtimer
var shader : ShaderMaterial

const NB_FLASH : int = 8
var flash_counter : int
var flash_value : int = 0;

func _ready() -> void:
	shader = sprite.material as ShaderMaterial

func _on_flashtimer_timeout() -> void:
	shader.set_shader_parameter("flash_modifier", flash_value * 0.5);
	flash_value = !flash_value

	if flash_counter < NB_FLASH:
		flash_counter += 1
	else:
		flash_counter = 0
		shader.set_shader_parameter("flash_modifier", 0);
		timer.stop()

func _process(delta: float) -> void:
	if (Input.is_action_just_pressed("ui_select")):
		timer.start()
