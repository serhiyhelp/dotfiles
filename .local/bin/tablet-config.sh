#!/bin/sh

blender="Blender"
krita="Krita"
gimp="Gimp"
A=$(echo -e "$blender\n$krita\n$gimp" | dmenu -p "Choose the profile" -l 20)


case "$A" in

    $blender)
        #xsetwacom set "Wacom Intuos Pro M Pad pad" Button 1 "key ctrl z"
        #xsetwacom set "Wacom Intuos Pro M Pad pad" Button 2 "key space"
        #xsetwacom set "Wacom Intuos Pro M Pad pad" Button 3 "key shift"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 4 "0"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 5 "0"
        #xsetwacom set "Wacom Intuos Pro M Pad pad" Button 8 "key ctrl"
        #xsetwacom set "Wacom Intuos Pro M Pad pad" Button 9 "key f"
        #xsetwacom set "Wacom Intuos Pro M Pad pad" Button 10 "key ctrl r"
        #xsetwacom set "Wacom Intuos Pro M Pad pad" Button 11 "key shift r"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 2 "key alt"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 3 "key alt"
        ;;

    $gimp)
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 1 "key +ctrl z -ctrl"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 2 "key f"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 3 "key shift"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 8 "key x"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 9 "key ctrl r"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 10 "key shift r"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 2 "key alt"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 3 "2"
        ;;

    $krita)
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 1 "key +ctrl z -ctrl"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 2 "key f"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 3 "key shift"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 8 "key x"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 9 "key ctrl r"
        xsetwacom set "Wacom Intuos Pro M Pad pad" Button 10 "key shift r"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 2 "3"
        xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 3 "2"
        ;;

    esac
