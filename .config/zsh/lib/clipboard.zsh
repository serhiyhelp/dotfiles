# clipcopy - Copy data to clipboard
#
# Usage:
#
#  <command> | clipcopy    - copies stdin to clipboard
#
#  clipcopy <file>         - copies a file's contents to clipboard
#
##
#
# clippaste - "Paste" data from clipboard to stdout
#
# Usage:
#
#   clippaste   - writes clipboard's contents to stdout
#
#   clippaste | <command>    - pastes contents and pipes it to another process
#
#   clippaste > <file>      - paste contents to a file
#
# Examples:
#
#   # Pipe to another process
#   clippaste | grep foo
#
#   # Paste to a file
#   clippaste > file.txt
#
function detect-clipboard() {
  emulate -L zsh

  if [ -n "${DISPLAY:-}" ] && (( ${+commands[xclip]} )); then
    function clipcopy() { xclip -in -selection clipboard < "${1:-/dev/stdin}"; }
    function clippaste() { xclip -out -selection clipboard; }
  elif [ -n "${WAYLAND_DISPLAY:-}" ] && (( ${+commands[wl-copy]} )) && (( ${+commands[wl-paste]} )); then
    function clipcopy() { wl-copy < "${1:-/dev/stdin}"; }
    function clippaste() { wl-paste; }
  elif [ -n "${TMUX:-}" ] && (( ${+commands[tmux]} )); then
    function clipcopy() { tmux load-buffer "${1:--}"; }
    function clippaste() { tmux save-buffer -; }
  else
    function _retry_clipboard_detection_or_fail() {
      local clipcmd="${1}"; shift
      if detect-clipboard; then
        "${clipcmd}" "$@"
      else
        print "${clipcmd}: Platform $OSTYPE not supported or xclip/xsel not installed" >&2
        return 1
      fi
    }
    function clipcopy() { _retry_clipboard_detection_or_fail clipcopy "$@"; }
    function clippaste() { _retry_clipboard_detection_or_fail clippaste "$@"; }
    return 1
  fi
}

# Detect at startup. A non-zero exit here indicates that the dummy clipboards were set,
# which is not really an error. If the user calls them, they will attempt to redetect
# (for example, perhaps the user has now installed xclip) and then either print an error
# or proceed successfully.
detect-clipboard || true
