extends ParallaxBackground

@export var cloud_speed: float = -10.0

@onready var cloud_layer: ParallaxLayer = $clouds
@onready var cloud_sprite: Sprite2D = $clouds/Sprite2D


func _process(delta: float) -> void:
	cloud_layer.motion_offset = Vector2(cloud_layer.motion_offset.x + cloud_speed * delta, 0)
