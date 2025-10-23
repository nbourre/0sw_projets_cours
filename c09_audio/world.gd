extends Node2D

@onready var sfx : AudioStreamPlayer = $Sfx
@onready var music : AudioStreamPlayer = $Music
@onready var btnPlay : Button = $PlayMusic
@onready var slider : HSlider = $HSlider

var music_pos : float = 0.0



func _on_play_sfx_pressed() -> void:
	sfx.play()

func _on_play_music_pressed() -> void:
	if (music.playing):
		music_pos = music.get_playback_position()
		music.playing = false
		btnPlay.text = "Jouer"
	else :
		music.play(music_pos)
		btnPlay.text = "Pause"

func _on_h_slider_drag_ended(value_changed: bool) -> void:
	var val = slider.value
	music.volume_db = linear_to_db(val)
	print ("val : " + str(val) + "\tdb : " + str(music.volume_db))
	
