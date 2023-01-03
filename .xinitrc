TERMINAL="alacritty"
setxkbmap -option caps:swapescape -option grp:win_space_toggle -option grp:switch -layout "us,ua" -variant ",winkeys"
xmodmap -e "keycode 107 = Multi_key"

xrdb -load ~/.config/xorg/Xresources
xwallpaper --zoom ~/.config/wallpaper &
xbacklight -set 70 &
redshift -o -l 51.31:25.99 &
flameshot &
xinput set-prop "Elan Touchpad" "libinput Tapping Enabled" 1 &
xset -b &

dwm
