#!/bin/sh

msgfmt --statistics uk.po -o blender.mo
mv blender.mo /usr/share/blender/2.93/datafiles/locale/uk/LC_MESSAGES/blender.mo
