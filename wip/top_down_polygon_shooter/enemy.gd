extends CharacterBody2D

var player
var hp = 1
var speed = 5

# Called when the node enters the scene tree for the first time.
func _ready():
	player = get_parent().get_node("Player")

func _physics_process(_delta):
	var direction = (player.position - position)
	direction = direction.normalized() * speed
	
	look_at(player.position)
	
	var _collision = move_and_collide(direction)	
	
	if (hp <= 0):
		queue_free()


func _on_area_2d_area_entered(area):
	if "Bullet" in area.get_parent().name:
		hp -= 1;
		area.get_parent().queue_free()
		queue_free();
	
