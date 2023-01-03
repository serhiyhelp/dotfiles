export ZSH="/home/serhiy/.config/zsh"

HYPHEN_INSENSITIVE="true"
DISABLE_MAGIC_FUNCTIONS="true"
HISTFILE=~/.cache/zsh/history
HISTSIZE=50000
SAVEHIST=10000
ZSH_CACHE_DIR="/home/serhiy/.cache/zsh"

fpath=($ZSH/functions $ZSH/completions $fpath)

fpath=($ZSH/plugins/zsh-syntax-highlighting $fpath)
fpath=($ZSH/plugins/zsh-vim-mode $fpath)
fpath=($ZSH/plugins/git $fpath)
fpath=(/home/serhiy/.local/bin $fpath)
source $ZSH/plugins/fd
autoload -U compaudit compinit

#ZSH_COMPDUMP="${ZDOTDIR}/zcompdump"

#compinit -i -C -d "${ZSH_COMPDUMP}"
compinit

source $ZSH/lib/clipboard.zsh
source $ZSH/lib/completion.zsh
source $ZSH/lib/functions.zsh
source $ZSH/lib/git.zsh
source $ZSH/lib/key-bindings.zsh
source $ZSH/lib/spectrum.zsh
source $ZSH/lib/termsupport.zsh
source $ZSH/lib/theme-and-appearance.zsh

source $ZSH/plugins/zsh-vim-mode/zsh-vim-mode.plugin.zsh
source $ZSH/plugins/git/git.plugin.zsh
source /home/serhiy/.config/zsh/plugins/fzf/fzf.plugin.zsh
source $ZSH/theme


MODE_CURSOR_VIINS="#blinking bar"
MODE_CURSOR_REPLACE="$MODE_CURSOR_VIINS "
MODE_CURSOR_VICMD="block"
MODE_CURSOR_SEARCH="#steady underline"
MODE_CURSOR_VISUAL="$MODE_CURSOR_VICMD steady bar"
MODE_CURSOR_VLINE="$MODE_CURSOR_VISUAL "

alias vim=nvim
alias ip='ip -color=auto'
alias diff='diff --color=auto'
alias grep='grep   --colour=auto --exclude-dir={.bzr,CVS,.git,.hg,.svn,.idea,.tox}'
alias egrep='egrep --colour=auto --exclude-dir={.bzr,CVS,.git,.hg,.svn,.idea,.tox}'
alias fgrep='fgrep --colour=auto --exclude-dir={.bzr,CVS,.git,.hg,.svn,.idea,.tox}'
alias cls=clear

alias history='fc -lE'

## History command configuration
setopt extended_history       # record timestamp of command in HISTFILE
setopt hist_expire_dups_first # delete duplicates first when HISTFILE size exceeds HISTSIZE
setopt hist_ignore_dups       # ignore duplicated commands history list
setopt hist_ignore_space      # ignore commands that start with space
setopt hist_verify            # show command with history expansion to user before running it
setopt share_history          # share command history data

autoload -Uz is-at-least

## jobs
setopt long_list_jobs

env_default 'PAGER' 'less'
env_default 'LESS' '-R'

setopt auto_pushd
setopt pushd_ignore_dups
setopt pushdminus

alias -g ...='../..'

alias -- -='cd -'

alias md='mkdir -p'
alias rd=rmdir

function d () {
  if [[ -n $1 ]]; then
    dirs "$@"
  else
    dirs -v | head -n 10
  fi
}
compdef _dirs d

alias l='ls -lAh'
alias la='ls -lAh'
setopt interactivecomments

n ()
{
    if [ -n $NNNLVL ] && [ "${NNNLVL:-0}" -ge 1 ]; then
        echo "nnn is already running"
        return
    fi

    export NNN_TMPFILE="${XDG_CONFIG_HOME:-$HOME/.config}/nnn/.lastd"

    nnn "$@"

    if [ -f "$NNN_TMPFILE" ]; then
            . "$NNN_TMPFILE"
            rm -f "$NNN_TMPFILE" > /dev/null
    fi
}

alias nnn='n -e'
alias dragon='dragon-drop --all --and-exit'
alias dropin='dragon-drop --target --print-path --and-exit'
alias sxiv='nsxiv -o'
alias tadd='transmission-remote --add'

NNN_BMS="p:$HOME/media/vids/porn;b:$HOME/src/packages/Prykoliakhy;u:$HOME/src/unity"
#NNN_PLUG=""
NNN_ORDER="s:/home/serhiy/vids/porn"
#NNN_ARCHIVE=""
#NNN_FCOLORS="c1e2272e006033f7c6d6abc4"
#NNN_MCLICK='^R'
#NO_COLOR=1
alias o='xdg-open'

BLK="0B"
CHR="0B"
DIR="04"
EXE="64"
REG="00"
HARDLINK="06"
SYMLINK="06"
MISSING="00"
ORPHAN="09"
FIFO="06"
SOCK="0B"
OTHER="0B"
LK="0B"
CHR="0B"
DIR="04"
EXE="64"
REG="00"
HARDLINK="06"
SYMLINK="06"
MISSING="00"
ORPHAN="09"
FIFO="06"
SOCK="0B"
OTHER="06"
NNN_FCOLORS="$BLK$CHR$DIR$EXE$REG$HARDLINK$SYMLINK$MISSING$ORPHAN$FIFO$SOCK$OTHER"

NNN_PLUG="d:dragdrop"

source $ZSH/plugins/zsh-syntax-highlighting/zsh-syntax-highlighting.plugin.zsh
