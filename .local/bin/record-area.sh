#!/bin/sh

area=`xrectsel`

x=`awk '{ print $1 }' <<< $area`
y=`awk '{ print $2 }' <<< $area`
w=`awk '{ print $3 }' <<< $area`
h=`awk '{ print $4 }' <<< $area`


echo $w $h

ffmpeg -video_size "$w"x"$h" -framerate 30 -f x11grab -i :0.0+$x,$y output.mp4
