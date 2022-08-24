#!/bin/bash

pw=" Вимкнення"
rb=" Перезавантаження"

option=$(echo -e "$rb\n$pw" | dmenu -p "Дія: ")

if [ "$option" = "$pw" ]
then
    poweroff
elif [ "$option" == "$rb" ]
then
    reboot
else
    exit 1
fi
