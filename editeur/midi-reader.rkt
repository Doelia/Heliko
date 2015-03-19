#lang racket

(provide parse-midi-file)
(provide header-division)
(provide track-chunk?)
(provide track-chunk-events)
(provide track-event-event)
(provide time-signature-cc)
(provide set-tempo)
(provide sequence-name)
(provide midi-event?)
(provide midi-event-delta)
(provide midi-event-instruction)
(provide to-int)

(struct header (mthd length format n division) #:transparent)
(struct track-chunk (mtrk length [events #:auto]) #:mutable #:auto-value '() #:transparent)
(struct track-event (v-time event) #:transparent)
(struct time-signature (nn dd cc bb) #:transparent)
(struct set-tempo (tempo) #:transparent)
(struct sequence-name (name) #:transparent)
(struct midi-event (delta instruction arg1 arg2) #:transparent)

(define time-signature-event '(255 88 4))
(define set-tempo-event '(255 81 3))
(define sequence-name-event '(255 3))

(define (sublist? l1 l2)
  (andmap (λ (i j)
            (= i j)) l1 (take l2 (length l1))))

(define (interpret-midi-events e)
  (let* ([l (drop e 2)]
         [s (sequence-name (to-string (take (cdr l) (car l))))]
         [l (reverse (foldl (λ (i l)
                              (cond
                                [(and (= (length (car l)) 1) (> (car (flatten l)) 127))
                                 (append `((,(cons i (car l)))) (cdr l))]
                                ;[(= (length (car l)) 1) (append `((,(cons i (car l)))) (cdr l))]
                                [(= (length (car l)) 4) (cons `(,i) l)]
                                [else (append `(,(append (car l) `(,i))) (cdr l))])) '(()) (drop (cdr l) (car l))))])
    (map (λ (i)
           (if (= (length i) 4)
               (midi-event (flatten (list (car i))) (cadr i) (caddr i) (cadddr i)) '())) l)))

(define (interpret-event e)
  (cond ([sublist? time-signature-event e] [let ([l (drop e 3)])
                                             `(,(time-signature (car l) (cadr l) (caddr l) (cadddr l)))])
        ([sublist? set-tempo-event e] [let ([l (drop e 3)])
                                        `(,(set-tempo (to-int (take l 3))))])
        (else (interpret-midi-events e))))


(define (read-header in)
  (header (to-string (read-n-bytes in 4))
          (to-int (read-n-bytes in 4))
          (to-int (read-n-bytes in 2))
          (to-int (read-n-bytes in 2))
          (to-int (read-n-bytes in 2))))

(define (read-track-chunk in)
  (let ([c (track-chunk (to-string (read-n-bytes in 4))
                        (to-int (read-n-bytes in 4)))])
    (set-track-chunk-events! c (read-track-events in (track-chunk-length c)))
    c))

(define (read-chunks in)
  (let f ([data '()])
    (let ([x (peek-byte in)])
      (if (eof-object? x)
          data
          (f (append data `(,(read-track-chunk in))))))))

(define (read-track-events in length)
  (track-event
   (let f([data '()])
     (let ([x (read-byte in)])
       (set! length (- length 1))
       (if (< x 128)
           (to-int (append data `(,x)))
           (f (append data `(,x))))))
   (let f([data '()] [length length])
     (if (= length 0)
         (interpret-event data)
         (f (append data `(,(read-byte in))) (- length 1))))))

(define (read-n-bytes in n)
  (let f([data '()] [i 0])
    (if (= i n)
        data
        (let ([x (read-byte in)])
          (f (append data `(,x)) (+ i 1))))))

(define (to-string l)
  (bytes->string/utf-8 (list->bytes l)))

(define (to-int l)
  (let ([n 1])
    (foldr (λ (i sum)
             (let ([x (* i n)])
               (set! n (* n 256))
               (+ sum x))) 0 l)))

(define (parse-midi-file in)
  (let* ([h (read-header in)]
         [chunks (read-chunks in)])
    (cons h chunks)))