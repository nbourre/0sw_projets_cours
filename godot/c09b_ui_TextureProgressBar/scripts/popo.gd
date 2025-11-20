class_name Player
extends GenericCharacter

signal PlayerHit(value : int, max_value : int)
var life_timer : Timer = Timer.new()
var hp : int = 100
var max_hp : int = 100
var hp_increment : int = 2

func _ready() -> void:
	anim_player = $AnimationPlayer
	sprite = $Sprite2D
	anim_player.play("idle")
	add_child(life_timer)
	life_timer.wait_time = 1.0
	life_timer.autostart = true
	life_timer.timeout.connect(_on_life_timer_timeout)
	life_timer.start()
	

func _physics_process(delta: float) -> void:
	move_and_slide()

func _on_life_timer_timeout() -> void :
	
	if (hp <= 0 || hp >= max_hp):
		hp_increment = -hp_increment
	
	hp += hp_increment
			
	PlayerHit.emit(hp, max_hp)
