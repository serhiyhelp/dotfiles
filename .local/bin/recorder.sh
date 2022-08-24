#!/bin/sh

if [ -f /tmp/record-mark ]
then
	rm /tmp/record-mark
	killall -q ffmpeg
	notify-send "Record saved" "home"
else
	touch /tmp/record-mark
	#ffmpeg -video_size 1920x1080 -framerate 50 -f x11grab -i :0.0+0,0 -f alsa -ac 2 -i pulse -acodec aac -strict experimental -vf format=yuv420p `date +%Y-%m-%d_%H-%M_%S`.mp4 &
	ffmpeg -video_size 1920x1080 -framerate 50 -f x11grab -i :0.0+0,0  -vf format=yuv420p `date +%Y-%m-%d_%H-%M_%S`.mp4 &
	#ffmpeg --enable-libx265 -video_size 1920x1080 -framerate 50 -f x11grab -i :0.0+0,0 out.mp4 &
fi
