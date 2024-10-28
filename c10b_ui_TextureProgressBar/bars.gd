extends CanvasLayer

var player : Player
var health_bar : GenericProgressBar

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	player = get_parent().get_node("Player") as Player
	health_bar = $GenericProgressBar
	player.PlayerHit.connect(update_health)
	
	update_health(100, 100)
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
func update_health(value : int, max_value : int):
	health_bar.update_value(value, max_value)
