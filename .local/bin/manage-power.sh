#!/bin/bash

pw=" poweroff"
rb=" reboot"

option=$(echo -e "$rb\n$pw" | dmenu -p "select an option")

if [ "$option" = "$pw" ]
then
    poweroff
elif [ "$option" == "$rb" ]
then
    reboot
else
    exit 1
fi
