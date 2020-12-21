#!/bin/sh

A=$(echo -e "Blender\nGimp" | rofi -dmenu -p "Choose Profile")


case "$A" in

    "Blender")
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 1 "key ctrl z"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 2 "key f"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 3 "key shift"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 8 "key ctrl"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 9 "key ctrl r"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 10 "key shift r"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 2 "key alt"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 3 "key alt"
        ;;

    "Gimp")
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 1 "key ctrl z"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 2 "key f"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 3 "key shift"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 8 "key ctrl"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 9 "key ctrl r"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 10 "key shift r"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 2 "key ctrl"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 3 "key alt"
        ;;

esac
