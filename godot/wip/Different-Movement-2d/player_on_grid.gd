extends Area2D

@onready var ray := $RayCast2D

var animation_speed := 3
var moving := false

var tile_size := 128
var inputs = {
	"right": Vector2(1, 0),
	"left": Vector2(-1, 0),
	"up": Vector2(0, -1),
	"down": Vector2(0, 1)
}

func _ready() -> void:
	# Permet de centrer le joueur dans la case de la grille
	position = position.snapped(Vector2.ONE * tile_size)
	position += Vector2.ONE * tile_size / 2


func _unhandled_input(event: InputEvent) -> void:
	if moving:
		return
	for dir in inputs.keys():
		if event.is_action_pressed(dir):
			move(dir)
		
func move(direction: String) -> void:
	var target := inputs[direction] as Vector2 * tile_size
	ray.target_position = target
	ray.force_raycast_update()
	if ! ray.is_colliding():
		var tween := create_tween()
		tween.tween_property(self, "position", position + target, 1.0 / animation_speed).set_trans(Tween.TRANS_SINE)
		moving = true
		await tween.finished
		moving = false

func _process(delta: float) -> void:
	pass
