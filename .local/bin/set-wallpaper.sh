#!/bin/sh

echo $1
ln -sf $1 ~/.config/wallpaper
xwallpaper --zoom ~/.config/wallpaper
