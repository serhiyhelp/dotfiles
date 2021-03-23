#!/bin/sh

cat ~/.local/share/emoji | dmenu -i -l 20 -p "Select an Emoji" | awk '{print $1}' | xclip -selection c
