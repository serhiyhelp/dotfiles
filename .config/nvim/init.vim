call plug#begin('~/.local/share/nvim/plugged')"
Plug 'itchyny/lightline.vim'
Plug 'junegunn/fzf.vim'
Plug 'lyokha/vim-xkbswitch'
call plug#end()

source ~/.config/nvim/theme.vim
source ~/.config/nvim/lightline.vim

syntax on
filetype on
filetype plugin on
filetype indent on
set encoding=utf-8
set nowrap
set hlsearch
"noremap // :nohls<CR>

set title
set titlestring=%-25.55F\ %a%r%m 
set titlelen=70

set autoindent
set tabstop=4
set shiftwidth=4
set clipboard+=unnamedplus
set previewheight=10
set timeoutlen=500
set splitbelow splitright

set completeopt-=preview
set completeopt+=longest,menuone,noselect,noinsert

set wildmode=longest,list,full
set wildmenu

au BufWritePre *\(.vim\)\@<! %s/\s\+$//e"

augroup SHADA
    autocmd!
    autocmd CursorHold,TextYankPost,FocusGained,FocusLost *
                \ if exists(':rshada') | rshada | wshada | endif
augroup END

let g:XkbSwitchEnabled = 1

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

noremap <a-h> <c-w>h
noremap <a-j> <c-w>j
noremap <a-k> <c-w>k
noremap <a-l> <c-w>l

set listchars=tab:\┆\ 
set list
match TabGroup /\t/
hi TabGroup guifg=#084454

inoremap <c-j> <c-n>
inoremap <c-k> <c-p>
inoremap <c-space> <c-x><c-o>

set mouse=nv

" Замінити всі зразки слова
noremap <a-r> <ESC>"ryiw:%s/\<<c-r>r\>//g\|''<Left><Left><Left><Left><Left>

augroup c_cmd
	au FileType c :set foldmethod=syntax
augroup END
