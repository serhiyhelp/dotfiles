#!/bin/sh

echo "before $(du -sh .)"

if ls *.png > /dev/null 2>&1
then
    echo "convert png to jpeg ($(ls *.png | wc -l) files)"
    for f in *.png
    do
        convert $f "${f%.png}.jpeg"
    done

    echo "remove png"
    rm *.png
fi

if [ -z $1 ]
then
    echo "resize images"
    for f in *.jpeg
    do
        convert $f -resize 3500x1800\> $f
    done
fi

echo "after $(du -sh .)"
