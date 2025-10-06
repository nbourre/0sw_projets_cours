extends CharacterBody2D

# Constantes de mouvement
const UP = Vector2(0, -1)
const GRAVITY = 20
const MAXFALLSPEED = 200
const MAXSPEED : float = 100
const JUMPFORCE = 300
const ACCEL : float = 10

# Variables de mouvement
var motion = Vector2()
var v_zero = Vector2()
var facing_right = true

# Références aux nœuds enfants
var current_sprite: Sprite2D
var anim_player: AnimationPlayer

# Appelé quand le nœud entre dans l'arbre de scène
func _ready():
	current_sprite = $Sprite2D
	anim_player = $AnimationPlayer

# Fonction principale de physique appelée à chaque frame
func _physics_process(delta : float) -> void :
	# Calcul de la direction (non utilisé dans le code original - imperfection conservée)
	var dir = Input.get_axis("left", "right")
	
	# Application de la gravité
	motion.y += GRAVITY
	
	# Limitation de la vitesse de chute
	if motion.y > MAXFALLSPEED:
		motion.y = MAXFALLSPEED
	
	# Gestion de l'orientation du sprite
	if facing_right:
		current_sprite.flip_h = false
	else:
		current_sprite.flip_h = true
	
	# Gestion des mouvements horizontaux
	if Input.is_action_pressed("left"):
		motion.x -= ACCEL
		facing_right = false
		anim_player.play("Run")

	elif Input.is_action_pressed("right"):
		motion.x += ACCEL
		facing_right = true
		anim_player.play("Run")
	else:
		# Ralentissement progressif quand aucune touche n'est pressée
		motion.x = lerp(motion.x, 0.0, 0.2)
		#motion = motion.lerp(Vector2.ZERO, 0.2)
		anim_player.play("Idle")
	
	
	# Gestion du saut (seulement quand on est au sol)
	if is_on_floor():
		# On ne regarde qu'une seule fois et non le maintien de la touche
		if Input.is_action_just_pressed("ui_jump"):
			motion.y = -JUMPFORCE
			print("motion.y = ", motion.y)
	
	# Gestion des animations en l'air
	if not is_on_floor():
		if motion.y < 0:
			anim_player.play("jump")
		elif motion.y > 0:
			anim_player.play("fall")
	
	# Bug conservé : Si on saute, le X est automatiquement réduit, car l'impulsion est trop forte
	# motion.x = motion.limit_length(MAXSPEED).x
	
	# Calcul problématique de la vitesse horizontale (imperfection conservée)
	motion.x = lerp(motion.x, MAXSPEED * dir, ACCEL / MAXSPEED)
		
	# Application du mouvement
	velocity = motion
	move_and_slide()
