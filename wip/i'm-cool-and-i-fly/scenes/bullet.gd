class_name Bullet
extends Node2D

var speed = 750

func _physics_process(delta):
	position += transform.x * speed * delta

func _on_Bullet_body_entered(body):
	# On utilise mobs, mais cela peut être n'importe quel groupe
	if body.is_in_group("mobs"):
		body.queue_free()
	queue_free()


func _on_timer_timeout() -> void:
	queue_free() # Replace with function body.
