spdir:1
spset:65406
call:main
halt
label:&
pop:r0
callr:r0
ret:0
label:+
pop:r1
pop:r2
add:r3,r2,r1
push:r3
ret:0
label:-
pop:r1
pop:r2
sub:r3,r2,r1
push:r3
ret:0
label:/
pop:r1
pop:r2
div:r3,r2,r1
push:r3
ret:0
label:*
pop:r1
pop:r2
mul:r3,r2,r1
push:r3
ret:0
label:%
pop:r1
pop:r2
mod:r3,r2,r1
push:r3
ret:0
label:^
pop:r1
pop:r2
xor:r3,r2,r1
push:r3
ret:0
label:atoi
pop:r1
call:dup
pop:r0
loadi:r2,13
int:r2,r1,r0
ret:0
label:itoa
pop:r1
call:dup
pop:r0
loadi:r2,14
int:r2,r0,r1
ret:0
label:dup
pop:r0
push:r0
push:r0
ret:0
label:!
pop:r0
pop:r1
push:r0
push:r1
ret:0
label:!2
pop:r0
pop:r1
pop:r2
push:r2
push:r1
push:r0
ret:0
label:.
pop:r1
loadi:r0,65500
savrr:r1,r0
loadi:r1,7
int:r1,r0,r0
ret:0
label:,
loadi:r1,65500
loadi:r0,2
int:r0,r1,r1
redrr:r0,r1
push:r0
ret:0
label:printStr
loadi:r0,1
pop:r2
pop:r1
int:r0,r1,r2
ret:0
label:getStr
loadi:r0,2
pop:r1
call:dup
pop:r2
int:r0,r1,r2
ret:0
label:.$
pop:r0
loadi:r1,0
cmp:r0,r1
jne:loop$
ret:0
label:loop$
push:r0
pushi:65257
call:writeMem
call:.
pushi:65257
call:readMem
pop:r0
loadi:r1,1
sub:r0,r0,r1
push:r0
call:.$
ret:0
label:CR
pushi:13
call:.
ret:0
label:.m
pop:r0
loadi:r1,1
int:r1,r0,r0
ret:0
label:,m
pop:r0
pop:r1
loadi:r2,2
int:r2,r1,r0
ret:0
label:readMem
pop:r0
redrr:r1,r0
push:r1
ret:0
label:writeMem
pop:r1
pop:r0
savrr:r0,r1
ret:0
label:_
pop:r0
pop:r1
savrr:r1,r0
ret:0
label:ife
pop:r3
pop:r2
pop:r0
pop:r1
push:r2
push:r3
cmp:r0,r1
jne:INE
pop:r0
pop:r1
push:r0
call:&
ret:0
label:INE
pop:r0
call:&
ret:0
label:sqr
call:dup
call:*
ret:0
label:add1
loadi:r0,50000
loadi:r1,1
add:r0,r1,r0
loadi:r1,1
savrr:r1,r0
loadi:r0,50000
loadi:r1,1
add:r0,r0,r1
redrr:r1,r0
push:r1
call:+
ret:0
label:generate64
loadi:r0,50000
loadi:r1,1
add:r0,r1,r0
loadi:r1,8
savrr:r1,r0
loadi:r0,50000
loadi:r1,1
add:r0,r0,r1
redrr:r1,r0
push:r1
call:sqr
ret:0
label:generateA
call:generate64
call:add1
ret:0
label:printA
call:generateA
call:.
ret:0
label:main
call:printA
ret:0
