extends RigidBody2D

#var buffer = 50
#
#var previous_position
#
## Called when the node enters the scene tree for the first time.
#func _ready():
#	pass # Replace with function body.
#
#func _physics_process(_delta):
#	if (!is_moving()):
#		queue_free()
#
#func is_moving():
#	if linear_velocity.length() > 30 :
#		previous_position = global_position
#		return true
#	else:
#		return false


func _on_timer_timeout():
	queue_free() # Replace with function body.
