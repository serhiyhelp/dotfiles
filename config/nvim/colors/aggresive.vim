hi clear
if exists('syntax on')
    syntax reset
endif

let bg=             'NONE'
let fg=             '#7ea2b4'

let black=          '#161b1d'
let red=            '#d22d72'
let green=          '#568c3b'
let yellow=         '#8a8a0f'
let blue=           '#257fad'
let magenta=        '#5d5db1'
let cyan=           '#2d8f6f'
let white=          '#7ea2b4'

let br_black=       '#5a7b8c'
let br_red=         '#d22d72'
let br_green=       '#568c3b'
let br_yellow=      '#8a8a0f'
let br_blue=        '#257fad'
let br_magenta=     '#5d5db1'
let br_cyan=        '#2d8f6f'
let br_white=       '#ebf8ff'

exe'hi ColorColumn	guibg='red

exe'hi Comment      guibg='bg
exe'hi Comment      guifg='green
hi Comment	gui=italic

exe'hi Cursor		guibg='bg
exe'hi Cursor		guifg='fg

exe'hi CursorIM	    guibg='bg
exe'hi CursorIM	    guifg='fg

exe'hi CursorColumn	guibg='br_white
exe'hi CursorLine	guibg='br_white

exe'hi Directory	guibg='bg
exe'hi Directory	guifg='blue

exe'hi DiffAdd		guibg='green
exe'hi DiffAdd		guifg='black

exe'hi DiffChange	guibg='yellow
exe'hi DiffChange	guifg='black

exe'hi DiffDelete	guibg='red
exe'hi DiffDelete	guifg='black

exe'hi DiffText	    guibg='bg
exe'hi DiffText	    guifg='fg

exe'hi EndOfBuffer	guibg='bg
exe'hi EndOfBuffer	guifg='blue

exe'hi ErrorMsg	    guibg='red
exe'hi ErrorMsg	    guifg='black

exe'hi VertSplit	guibg='blue
exe'hi VertSplit	guifg='blue

exe'hi Folded		guibg='magenta
exe'hi Folded		guifg='black

exe'hi FoldColumn	guibg='magenta
exe'hi FoldColumn	guifg='black

exe'hi SignColumn	guibg='bg
exe'hi SignColumn	guifg='fg

exe'hi IncSearch	guibg='black
exe'hi IncSearch	guifg='yellow

exe'hi Substitute	guibg='black
exe'hi Substitute	guifg='yellow

exe'hi LineNr		guibg='bg
exe'hi LineNr		guifg='blue

exe'hi CursorLineNr	guibg='bg
exe'hi CursorLineNr	guifg='magenta
hi CursorLineNr	gui=bold

exe'hi MatchParen	guibg='green
exe'hi MatchParen	guifg='white

exe'hi ModeMsg		guibg='bg
exe'hi ModeMsg		guifg='fg

exe'hi MsgArea		guibg='bg
exe'hi MsgArea		guifg='fg

exe'hi MsgSeparator	guibg='bg
exe'hi MsgSeparator	guifg='green

exe'hi MoreMsg		guibg='red
exe'hi MoreMsg		guifg='black

exe'hi NonText		guibg='bg
exe'hi NonText		guifg='fg

exe'hi Normal		guibg='bg
exe'hi Normal		guifg='fg

exe'hi NormalFloat	guibg='bg
exe'hi NormalFloat	guifg='fg

exe'hi NormalNC	    guibg='bg
exe'hi NormalNC	    guifg='fg

exe'hi Pmenu		guibg='br_white
exe'hi Pmenu		guifg='black

exe'hi PmenuSel	    guibg='cyan
exe'hi PmenuSel	    guifg='black

exe'hi PmenuSbar	guibg='br_white
exe'hi PmenuThumb	guibg='cyan

exe'hi Question	    guibg='magenta
exe'hi Question 	guifg='black

exe'hi QuickFixLine	guibg='bg
exe'hi QuickFixLine	guifg='magenta

exe'hi Search		guibg='yellow
exe'hi Search		guifg='black

exe'hi SpecialKey	guibg='bg
exe'hi SpecialKey	guifg='fg

exe'hi SpellBad	    guibg='bg
exe'hi SpellBad	    guifg='red
hi SpellBad gui=underline

exe'hi SpellCap	    guibg='bg
exe'hi SpellCap	    guifg='fg
hi SpellCap gui=underline

exe'hi SpellLocal	guibg='bg
exe'hi SpellLocal	guifg='fg

exe'hi SpellRare	guibg='bg
exe'hi SpellRare	guifg='fg
hi SpellRare gui=underline

exe'hi StatusLine	guifg='blue
exe'hi StatusLineNC	guifg='blue

exe'hi TabLine		guibg='bg
exe'hi TabLine		guifg='fg

exe'hi TabLineFill	guibg='bg
exe'hi TabLineFill	guifg='fg

exe'hi TabLineSel	guibg='bg
exe'hi TabLineSel	guifg='fg

exe'hi Title		guibg='bg
exe'hi Title		guifg='fg

exe'hi Visual		guibg='green
exe'hi Visual		guifg='black

exe'hi VisualNOS	guibg='white
exe'hi VisualNOS	guifg='black

exe'hi WarningMsg	guibg='yellow
exe'hi WarningMsg	guifg='black

exe'hi Whitespace	guibg='bg
exe'hi Whitespace	guifg='fg

exe'hi WildMenu	    guibg='white
exe'hi WildMenu	    guifg='black
hi WildMenu gui=underline

"Logical highlight

exe'hi Constant	    guifg='fg
exe'hi String	    guifg='red
exe'hi Character	guifg='yellow
exe'hi Number	    guifg='fg
exe'hi Float        guifg='fg
exe'hi Boolean      guifg='magenta

exe'hi Identifier	guifg='blue
exe'hi Function	    guifg='red

exe'hi Statement	guifg='cyan
exe'hi Conditional	guifg='cyan
exe'hi Repeat   	guifg='cyan
exe'hi Label       	guifg='cyan
exe'hi Operator     guifg='fg
exe'hi Keyword      guifg='cyan
exe'hi Exception    guifg='cyan

exe'hi Type	        guifg='blue
exe'hi StorageClass guifg='blue
exe'hi Structure    guifg='blue
exe'hi Typedef      guifg='cyan

exe'hi PreProc	    guifg='br_black

exe'hi Special      guifg='red

exe'hi Error        guibg='red
exe'hi Error	    guifg='black
hi Error gui=underline

exe'hi Todo         guibg='yellow
exe'hi Todo	        guifg='black
hi Todo gui=underline

