# res://scripts/directional_sprite_set.gd
class_name DirectionalSpriteSet
extends RefCounted

var actions := {}            # action -> {angles, body, shadow}
var angle_offset_rad := PI/2 # 0 rad points north

func set_character(base_path: String) -> void:
    actions.clear()
    var dir := DirAccess.open(base_path)
    if dir == null:
        push_error("Character folder not found: %s" % base_path)
        return
    for action in dir.get_directories():
        _load_action(base_path.path_join(action), action)

func get_actions() -> Array[String]:
    return actions.keys()

func has_action(action: String) -> bool:
    return actions.has(action)

func get_textures(action: String, angle_rad: float) -> Dictionary:
    # Fallback to "Idle" or any action if requested not found
    if not actions.has(action):
        if actions.has("Idle"):
            action = "Idle"
        elif actions.size() > 0:
            action = actions.keys()[0]
        else:
            return {}
    var sprite_set = actions[action]
    var idx = _closest_index(sprite_set.angles, rad_to_deg(fposmod(angle_rad + angle_offset_rad, TAU)))
    return {
        "body": sprite_set.body[idx],
        "shadow": sprite_set.shadow[idx],
        "angle_deg": sprite_set.angles[idx],
        "index": idx,
        "action": action
    }

func _load_action(action_path: String, action_name: String) -> void:
    var d := DirAccess.open(action_path)
    if d == null: return

    var body_by_angle := {}
    var shadow_by_angle := {}

    for f in d.get_files():
        if not f.ends_with(".png"):
            continue
        var base := f.get_basename()          # e.g., "Walk_Body_030"
        var parts := base.split("_")
        if parts.size() < 2:
            continue
        var kind := parts[parts.size()-2]     # "Body" or "Shadow"
        var angle_s := parts[parts.size()-1]  # "0", "030", "315"
        if not angle_s.is_valid_int():
            continue
        var angle_i := int(angle_s)
        var full_path := "%s/%s" % [action_path, f]
        if kind == "Body":
            body_by_angle[angle_i] = load(full_path)
        elif kind == "Shadow":
            shadow_by_angle[angle_i] = load(full_path)

    # Intersect angles that have both body and shadow
    var angles := []
    for a in body_by_angle.keys():
        if shadow_by_angle.has(a):
            angles.append(a)
    angles.sort() # 0..360, irregular supported

    if angles.is_empty():
        push_warning("No matching Body/Shadow in %s" % action_path)
        return

    var sprite_set := {
        "angles": PackedInt32Array(angles),
        "body": [],
        "shadow": []
    }
    for a in angles:
        sprite_set.body.append(body_by_angle[a])
        sprite_set.shadow.append(shadow_by_angle[a])

    actions[action_name] = sprite_set

func _closest_index(angles: PackedInt32Array, target_deg: float) -> int:
    target_deg = fposmod(target_deg + 360.0, 360.0)
    var best_idx := 0
    var best_diff := 1e9
    for i in range(angles.size()):
        var a := float(angles[i])
        var diff := absf(fposmod(target_deg - a + 180.0, 360.0) - 180.0)
        if diff < best_diff:
            best_diff = diff
            best_idx = i
    return best_idx

func slice_grid(tex: Texture2D, frame_size: Vector2i, margin: Vector2i = Vector2i.ZERO, spacing: Vector2i = Vector2i.ZERO) -> Array[Texture2D]:
    var frames: Array[Texture2D] = []
    if tex == null: return frames
    var cols := int((tex.get_width()  - margin.x * 2 + spacing.x) / (frame_size.x + spacing.x))
    var rows := int((tex.get_height() - margin.y * 2 + spacing.y) / (frame_size.y + spacing.y))
    for r in range(rows):
        for c in range(cols):
            var pos := Vector2i(
                margin.x + c * (frame_size.x + spacing.x),
                margin.y + r * (frame_size.y + spacing.y)
            )
            if pos.x + frame_size.x <= tex.get_width() and pos.y + frame_size.y <= tex.get_height():
                var atlas := AtlasTexture.new()
                atlas.atlas = tex
                atlas.region = Rect2i(pos, frame_size)
                frames.append(atlas)
    return frames

func build_sprite_frames_from_sheet(tex: Texture2D, frame_size: Vector2i, fps: float, anim_name: String, margin:=Vector2i.ZERO, spacing:=Vector2i.ZERO) -> SpriteFrames:
    var sf := SpriteFrames.new()
    sf.add_animation(anim_name)
    sf.set_animation_speed(anim_name, fps)
    sf.set_animation_loop(anim_name, true)
    for f in slice_grid(tex, frame_size, margin, spacing):
        sf.add_frame(anim_name, f)
    return sf

func build_animplayer_for_angle(ap: AnimationPlayer, anim_name: String, body_tex: Texture2D, shadow_tex: Texture2D, frame_size: Vector2i, fps: float, margin:=Vector2i.ZERO, spacing:=Vector2i.ZERO) -> void:
    var body_frames  = slice_grid(body_tex,   frame_size, margin, spacing)
    var shadow_frames = slice_grid(shadow_tex, frame_size, margin, spacing)
    var count := mini(body_frames.size(), shadow_frames.size())
    if count == 0: return
    var anim := Animation.new()
    anim.length = float(count) / fps
    anim.loop_mode = Animation.LOOP_LINEAR
    # Ensure ap.root_node targets your Spider node so these paths resolve:
    # e.g., in _ready(): animation_player.root_node = get_path()
    var t_body   = anim.add_track(Animation.TYPE_VALUE)
    anim.track_set_path(t_body,   "Body:texture")
    var t_shadow = anim.add_track(Animation.TYPE_VALUE)
    anim.track_set_path(t_shadow, "Shadow:texture")
    for i in range(count):
        var t := float(i) / fps
        anim.track_insert_key(t_body,   t, body_frames[i])
        anim.track_insert_key(t_shadow, t, shadow_frames[i])
    ap.add_animation(anim_name, anim)

func build_animplayer_for_action(ap: AnimationPlayer, action: String, frame_size: Vector2i, fps: float, margin:=Vector2i.ZERO, spacing:=Vector2i.ZERO, prefix: String = "") -> void:
    if not actions.has(action): return
    var action_set = actions[action]  # {angles, body, shadow}
    for i in range(action_set.angles.size()):
        var a := int(action_set.angles[i])
        var name := "%s%s_%03d" % [prefix, action, a]  # e.g., "Walk_045"
        build_animplayer_for_angle(ap, name, action_set.body[i], action_set.shadow[i], frame_size, fps, margin, spacing)