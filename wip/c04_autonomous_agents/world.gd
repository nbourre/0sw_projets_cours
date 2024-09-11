extends Node2D

@export_range(3, 500, 2) var num_boids : int = 50  # Nombre cible de boids
@export var debugging : bool = false
@onready var boid_scene = preload("res://boid.tscn")

var previous_debugging_state : bool = false

func _ready():
	randomize()  # Initialisation aléatoire
	adjust_boids()  # Ajuste le nombre de boids initialement

# Ajuste dynamiquement le nombre de boids pour correspondre à "num_boids"
func adjust_boids():
	var current_boids = get_children().filter(func(n): return n is Boid)
	var current_count = current_boids.size()

	if current_count < num_boids:
		# Ajouter des boids manquants
		add_boids(num_boids - current_count)
	elif current_count > num_boids:
		# Retirer des boids en excès
		remove_boids(current_count - num_boids)

# Ajouter les boids manquants
func add_boids(count: int):
	for i in range(count):
		var boid_instance = boid_scene.instantiate()
		boid_instance.position = Vector2(randi_range(0, get_viewport_rect().size.x), randi_range(0, get_viewport_rect().size.y))
		add_child(boid_instance)

# Retirer aléatoirement les boids en excès
func remove_boids(count: int):
	var current_boids = get_children().filter(func(n): return n is Boid)
	for i in range(count):
		var random_boid = current_boids[randi_range(0, current_boids.size() - 1)]
		current_boids.erase(random_boid)
		random_boid.queue_free()

func set_debug():
	var current_boids = get_children().filter(func(n): return n is Boid)
	
	for i in range(num_boids):
		if (i != 0) :
			current_boids[i].set_debug(debugging)
			current_boids[i].is_chosen = false
		else :
			current_boids[0].is_chosen = debugging
	

# Cette fonction peut être appelée à tout moment pour ajuster le nombre de boids
func _process(delta):
	# Appel d'ajustement pour synchroniser le nombre de boids si le champ change
	if num_boids != get_children().filter(func(n): return n is Boid).size():
		adjust_boids()
		
	if debugging != previous_debugging_state :
		previous_debugging_state = debugging
		set_debug()
