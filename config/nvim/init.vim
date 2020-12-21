call plug#begin('~/.vim/plugged')
"Plug 'OmniSharp/omnisharp-vim'
"Plug 'dense-analysis/ale'
call plug#end()


set background=dark
syntax on
set encoding=utf-8
set number
set relativenumber
set hlsearch
set title
set wildmode=longest,list,full
set tabstop=4
set shiftwidth=4
set expandtab

autocmd BufEnter */i3/* :set syn=sh

colorscheme aggresive
set termguicolors

" set langmap=йq,цw,уe,кr,еt,нy,гu,шi,щo,зp,х[,ї],фa,іs,вd,аf,пg,рh,оj,лk,дl,ж\\;,є',ґ\\,яz,чx,сc,мv,иb,тn,ьm,ю.,./,ЙQ,ЦW,УE,КR,ЕT,НY,НY,ГU,ШI,ЩO,ЗP,Х{,Ї},ФA,ІS,ВD,АF,ПG,РH,ОJ,ЛK,ДL,Ж\\:,Є\\",Ґ<bar>,ЯZ,ЧX,СC,МV,ИB,ТN,ЬM,Б\\<,Ю>,№#

let g:ale_linters = { 'cs': ['OmniSharp'] }

inoremap <expr> <Tab> pumvisible() ? '<C-n>' :
\ getline('.')[col('.')-2] =~# '[[:alnum:].-_#$]' ? '<C-x><C-o>' : '<Tab>'


"markdown
autocmd BufEnter *.md :! pandoc -o %.pdf %
autocmd BufEnter *.md :! zathura %.pdf &
autocmd BufWritePost *.md :! pandoc -o %.pdf %

