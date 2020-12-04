#!/bin/sh

sleep 2

echo "hello" >> ~/debug.log

export XAUTORITY=~/.Xauthority
export DISPLAY=:0

xsetwacom set "Wacom Intuos Pro M Pad pad" Button 1 "key +ctrl z -ctrl"
xsetwacom set "Wacom Intuos Pro M Pad pad" Button 2 "key +ctrl s -ctrl"
xsetwacom set "Wacom Intuos Pro M Pad pad" Button 3 "key shift"
xsetwacom set "Wacom Intuos Pro M Pad pad" Button 8 "key ctrl"

xsetwacom set "Wacom Intuos Pro M Pad pad" Button  9 "key f"
xsetwacom set "Wacom Intuos Pro M Pad pad" Button 10 "key +ctrl r -ctrl"
xsetwacom set "Wacom Intuos Pro M Pad pad" Button 11 "key +shift r -shift"
xsetwacom set "Wacom Intuos Pro M Pad pad" Button 12 "key +ctrl f -ctrl"

xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 2 "key alt"
xsetwacom set "Wacom Intuos Pro M Pen stylus" Button 3 "key alt"
