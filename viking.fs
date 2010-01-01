\
\ (c) Marcus Eskilsson <wtmarcus@gmail.com> 2010
\

\ Usage:
\
\ cMVO2test      -   cmvo2test
\ 15:15 protocol -   cadence 15:15
\ 36:36 protocol -   cadence 36:36
\


60000 constant ms-in-a-minute

: alarm  ( -- )
	bell 50 ms bell 50 ms bell
;
: pause-between-reps  ( n -- n )
	ms-in-a-minute swap /
;


\
\ CMVO2 test
\
	
: test-minute  ( n -- )
	\ Warning: This thing adds 2x50ms
	\          for each test-minute.
	\          That is 4/10th of a second
	\	   each test. I do not think
	\	   this is an issue. Avoiding it
	\	   would require some ugly kludge
	\	   for the first rep each test-minute.
	dup pause-between-reps
	1 rot 1+ swap u+do
		i . bell dup ms
	loop drop alarm cr
;
: last-test-minute  ( -- )
	ms-in-a-minute ms alarm
;
: cmvo2-test  ( -- )
	10 test-minute
	14 test-minute
	18 test-minute
	22 test-minute
	last-test-minute
;


: s  ( n -- n)
	1000 *
;

: cadence-time  ( n n -- n )
	s swap /
;

: wait  ( n -- )
	1 s - ms alarm 900 ms
;

: beep  ( n n -- )
	1 u+do
		bell i . dup ms
	loop 100 - ms alarm
; 

\
\ 15:15
\

: cadence-time-15  ( n -- n )
	15 cadence-time
;

: beep-15  ( n -- )
	dup cadence-time-15 swap beep
;

: wait-15  ( n -- )
	15 s wait
;
: 15:15  ( n -- )
	\ Supply with # of reps you be wanting.
	\ TODO: factor this + 36:36
	cr
	81 1 u+do
		i . ." : " dup beep-15 wait-15 cr
	loop drop
;

\
\ 36:36
\

: cadence-time-36  ( n -- n )
	36 cadence-time	
;

: wait-36  ( -- )
	36 s wait
;

: beep-36  ( n -- )
	dup cadence-time-36 swap beep
;

: 36:36  ( n -- )
	\ Supply with # of reps you be wanting.
	\ TODO: factor this + 15:15
	cr
	35 1 u+do
		i . ." : " dup beep-36 wait-36 cr
	loop drop
;