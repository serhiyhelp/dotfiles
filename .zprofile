#!/bin/zsh

unsetopt PROMPT_SP

export PATH="/home/serhiy/.local/bin/dwmblocks:/home/serhiy/.local/bin/controls:$PATH"

# Default programs:
export EDITOR="nvim"
export TERMINAL="alacritty"
export BROWSER="surf"

# ~/ Clean-up:
export XDG_CONFIG_HOME="$HOME/.config"
export XDG_DATA_HOME="$HOME/.local/share"
# export XDG_DATA_DIRS="$HOME/.local/share:$XDG_DATA_DIRS"
export XDG_CACHE_HOME="$HOME/.cache"
export XINITRC="${XDG_CONFIG_HOME:-$HOME/.config}/x11/xinitrc"
#export XAUTHORITY="$XDG_RUNTIME_DIR/Xauthority" # This line will break some DMs.
#export NOTMUCH_CONFIG="${XDG_CONFIG_HOME:-$HOME/.config}/notmuch-config"
export GTK2_RC_FILES="${XDG_CONFIG_HOME:-$HOME/.config}/gtk-2.0/gtkrc-2.0"
export LESSHISTFILE="-"
export WGETRC="${XDG_CONFIG_HOME:-$HOME/.config}/wget/wgetrc"
export INPUTRC="${XDG_CONFIG_HOME:-$HOME/.config}/shell/inputrc"
export ZDOTDIR="${XDG_CONFIG_HOME:-$HOME/.config}/zsh"
#export ALSA_CONFIG_PATH="$XDG_CONFIG_HOME/alsa/asoundrc"
#export GNUPGHOME="${XDG_DATA_HOME:-$HOME/.local/share}/gnupg"
export WINEPREFIX="${XDG_DATA_HOME:-$HOME/.local/share}/wineprefixes/default"
#export KODI_DATA="${XDG_DATA_HOME:-$HOME/.local/share}/kodi"
export PASSWORD_STORE_DIR="${XDG_DATA_HOME:-$HOME/.local/share}/password-store"
#export TMUX_TMPDIR="$XDG_RUNTIME_DIR"
export ANDROID_SDK_HOME="${XDG_CONFIG_HOME:-$HOME/.config}/android"
export CARGO_HOME="${XDG_DATA_HOME:-$HOME/.local/share}/cargo"
export GOPATH="${XDG_DATA_HOME:-$HOME/.local/share}/go"
#export ANSIBLE_CONFIG="${XDG_CONFIG_HOME:-$HOME/.config}/ansible/ansible.cfg"
export UNISON="${XDG_DATA_HOME:-$HOME/.local/share}/unison"
export HISTFILE="${XDG_DATA_HOME:-$HOME/.local/share}/history"
export WEECHAT_HOME="${XDG_CONFIG_HOME:-$HOME/.config}/weechat"
export ZSH="${XDG_CONFIG_HOME:-$HOME/.config}/oh-my-zsh"
export NUGET_PACKAGES="/home/serhiy/.cache/nuget"

# Other program settings:
export DICS="/usr/share/stardict/dic/"
export SUDO_ASKPASS="$HOME/.local/bin/dmenupass"
export FZF_DEFAULT_OPTS="--layout=reverse --height 40%"
export LESS=-R
export LESS_TERMCAP_mb="$(printf '%b' '[1;31m')"
export LESS_TERMCAP_md="$(printf '%b' '[1;36m')"
export LESS_TERMCAP_me="$(printf '%b' '[0m')"
export LESS_TERMCAP_so="$(printf '%b' '[01;44;33m')"
export LESS_TERMCAP_se="$(printf '%b' '[0m')"
export LESS_TERMCAP_us="$(printf '%b' '[1;32m')"
export LESS_TERMCAP_ue="$(printf '%b' '[0m')"
export LESSOPEN="| /usr/bin/highlight -O ansi %s 2>/dev/null"
export MOZ_USE_XINPUT2="1"		# Mozilla smooth scrolling/touchpads.
export AWT_TOOLKIT="MToolkit wmname LG3D"	#May have to install wmname
export _JAVA_AWT_WM_NONREPARENTING=1	# Fix for Java applications in dwm
#export QT_STYLE_OVERRIDE=breeze
export R_LIBS="/home/serhiy/.local/share/R"
#export QT_QPA_PLATFORMTHEME="gtk3"	# Have QT use gtk2 theme.
export QT_QPA_PLATFORMTHEME=qt5ct
export TDESKTOP_I_KNOW_ABOUT_GTK_INCOMPATIBILITY=1
export ANDROID_SDK_ROOT="$HOME/.local/share/Android/Sdk"
export KRITA_NO_STYLE_OVERRIDE=1

xset -b
export NNN_BMS='d:~/media/pic'
export NNN_COLORS='1234'
export GROFF_TMAC_PATH=~/doc/groff/.env

