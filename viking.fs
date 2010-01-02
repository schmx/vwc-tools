\
\ (c) Marcus Eskilsson <wtmarcus@gmail.com> 2010
\

\ Usage:
\
\ cMVO2test      -   cmvo2test
\ 15:15 protocol -   cadence 15:15
\ 36:36 protocol -   cadence 36:36
\

: s  ( n -- n )
	1000 *
;
: alarm  ( -- )
	bell 50 ms bell 50 ms bell
;
: pause-between-reps  ( n -- n )
	60 s swap /
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
	60 s alarm
;
: cmvo2-test  ( -- )
	10 test-minute
	14 test-minute
	18 test-minute
	22 test-minute
	last-test-minute
;





: cadence-time  ( n n -- n )
	s swap /
;
: wait  ( n -- )
	3 s - ms alarm 2900 ms
;
: beep  ( n n -- )
	1 u+do
		bell i . dup ms
	loop alarm 200 - ms alarm
	\ TODO : Add printout of last rep #
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
	cr
	36 1 u+do
		i . ." : " dup beep-36 wait-36 cr
	loop drop
;
