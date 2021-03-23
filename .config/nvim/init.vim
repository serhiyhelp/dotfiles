"fold{{{
au BufWinLeave init.vim set foldmethod=marker
au BufWinLeave init.vim mkview!
au BufWinEnter init.vim silent loadview
"}}}

"plugins {{{
call plug#begin('~/.vim/plugged')"
Plug 'Shougo/unite.vim'
Plug 'junegunn/goyo.vim'
Plug 'dense-analysis/ale'
Plug 'preservim/nerdtree'
Plug 'OmniSharp/omnisharp-vim'
Plug 'prabirshrestha/asyncomplete.vim'
call plug#end()"}}}

"options{{{
syntax on
filetype on
filetype plugin on
filetype indent on
set encoding=utf-8
set nowrap
set nohlsearch
set title
set tabstop=4
set shiftwidth=4
set clipboard+=unnamedplus
set expandtab
set previewheight=10
set timeoutlen=500
set splitbelow splitright
au BufWritePre * %s/\s\+$//e"
au BufEnter * :ALEDisableBuffer
"}}}

"theme{{{
let g:neosolarized_contrast = "normal"
let g:neosolarized_visibility = "normal"
let g:neosolarized_vertSplitBgTrans = 1
let g:neosolarized_bold = 1
let g:neosolarized_underline = 1
let g:neosolarized_italic = 1
let g:neosolarized_termBoldAsBright = 1
let g:solarized_termcolors=256
colorscheme NeoSolarized
set termguicolors
set background=dark"}}}


"common remaps{{{
map <C-h> <C-w>h
map <C-j> <C-w>j
map <C-k> <C-w>k
map <C-l> <C-w>l
inoremap <Left><Left> <Esc>/<++><Enter>"_c4l
noremap <Up> <Nop>
noremap <Down> <Nop>
noremap <Left> <Nop>
noremap <Right> <Nop>
inoremap <Up> <Nop>
inoremap <Down> <Nop>
inoremap <Left> <Nop>
inoremap <Right> <Nop>
noremap x "_x
noremap X "_X
noremap <C-b> a<Return><Esc>
noremap <C-t> :tabn<cr>
noremap <C-g> :tabp<cr>


"}}}


"set filetypes{{{
autocmd BufRead,BufNewFile *.ms,*.me,*.mom,*.man,tmac.* set filetype=groff
autocmd BufRead,BufNewFile *.html,*.htm set filetype=html
autocmd BufRead,BufNewFile *.css set filetype=css
autocmd BufRead,BufNewFile *.cs set filetype=cs"
"}}}


"groff{{{
au BufWritePost *.ms :silent exec "! pdfroff -dpaper=a4 -mspdf -ke \"%\" > \"/tmp/preview.pdf\""
au BufEnter *.ms let lang = "ua"
au InsertLeave *.ms :silent exec "! xkb-switch -s us; pkill -RTMIN+9 dwmblocks"
au InsertEnter *.ms :silent exec "! xkb-switch -s" lang "; pkill -RTMIN+9 dwmblocks"
"}}}

"html{{{
au filetype html inoremap ;d <div class=""><++></div><Esc>F"i
au Filetype html inoremap ;S <link rel="stylesheet" href=""><Esc>F"i
au Filetype html inoremap ;J <script type="text/javascript" src=""><Esc>F"i
au Filetype html inoremap ;p <p ></p><Esc>F>a
au Filetype html inoremap ;m <img src="" alt="<++>"><Esc>3F"i
au Filetype html inoremap ;l <a href=""><++></a><Esc>F"i
au Filetype html inoremap ;ll <li></li><Esc>F>a
au Filetype html inoremap ;lll <ul><Return></ul><Esc>O
"css
au FileType css inoremap /c . {<Return><++><Return>}<Esc>2k0a
au FileType css inoremap /* /**/<Esc>2hi"}}}

" c#{{{
"ale
let g:ale_sign_error         = '!'
let g:ale_sign_warning       = '•'
let g:ale_sign_info          = '·'
let g:ale_sign_style_error   = '·'
let g:ale_sign_style_warning = '·'
let g:ale_linters            = { 'cs': ['OmniSharp'] }

"autocomplete
set completeopt=longest,menuone,preview,noinsert
let g:asyncomplete_auto_popup       = 1
let g:asyncomplete_auto_completeopt = 1
inoremap <expr> <Tab>   pumvisible() ? "\<C-n>" : "\<Tab>"
inoremap <expr> <S-Tab> pumvisible() ? "\<C-p>" : "\<S-Tab>"
inoremap <expr> <cr>    pumvisible() ? asyncomplete#close_popup() : "\<cr>"

"omnisharp
let g:OmniSharp_popup               = 1
let g:OmniSharp_popup_options       = {  'winhl': 'Normal:NormalFloat' }
let g:OmniSharp_selector_ui         = 'unite'
au filetype cs noremap <C-a> :OmniSharpGetCodeActions<cr>

au FileType cs set foldmethod=syntax
"}}}
