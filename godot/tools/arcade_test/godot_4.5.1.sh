#!/bin/sh
printf '\033c\033]0;%s\a' Arcade Test
base_path="$(dirname "$(realpath "$0")")"
"$base_path/godot_4.5.1.x86_64" "$@"
