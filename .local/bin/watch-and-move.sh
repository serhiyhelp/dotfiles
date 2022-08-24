#!/bin/sh

nsxiv -o * | while read line; do mv "$line" "$1"; done
