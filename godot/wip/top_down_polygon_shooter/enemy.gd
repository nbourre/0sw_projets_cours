extends CharacterBody2D

var player
var hp = 1
var speed = 5

@onready var shape = $Polygon2D
static var count = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	player = get_parent().get_node("Player")

	count += 1
	
	if (count > 5):
		hp = randi_range(1, 5)
	
	
	match (hp):
		1:
			shape.color = Color(1, 1, 1)
		2:
			shape.color = Color(0, 0.8, 0)
		3:
			shape.color = Color(0, 0.8, 0)
		4:
			shape.color = Color(0, 0, 0.8)
		5:
			shape.color = Color(0.8, 0, 0)
			scale.x = 1.5
			scale.y = 1.5
			
	

func _physics_process(_delta):
	var direction = (player.position - position)
	direction = direction.normalized() * speed
	
	look_at(player.position)
	
	var _collision = move_and_collide(direction)	
	

func _on_area_2d_area_entered(area):
	if "Bullet" in area.get_parent().name:
		hp -= 1;
		print(hp)
		if (hp <= 0):
			count -= 1
			area.get_parent().queue_free()
			queue_free();
		
		if (count <= 0):
			var _tmp = get_tree().reload_current_scene()
		
	
