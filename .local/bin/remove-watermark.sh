#!/bin/sh

for X in *
do
    convert $X -crop -0-14 $X
done
