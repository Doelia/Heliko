#lang racket

(struct header (mthd length format n division) #:transparent)
(struct track-chunk (mtrk length [events #:auto]) #:mutable #:auto-value '() #:transparent)
(struct track-event (v-time event) #:transparent)
(struct time-signature (nn dd cc bb) #:transparent)
(struct set-tempo (tempo) #:transparent)
(struct sequence-name (name) #:transparent)

(define time-signature-event '(255 88 4))
(define set-tempo-event '(255 81 3))
(define sequence-name-event '(255 3))

;(define (get-length l)
  

(define (sublist? l1 l2)
  (andmap (λ (i j)
           (= i j)) l1 (take l2 (length l1))))

(define (interpret-midi-event e)
  (display e))

(define (interpret-event e)
  (cond ([sublist? time-signature-event e] [let ([l (drop e 3)])
                                             (time-signature (car l) (cadr l) (caddr l) (cadddr l))])
        ([sublist? set-tempo-event e] [let ([l (drop e 3)])
                                             (set-tempo (to-int (take l 3)))])
        ([sublist? sequence-name-event e] [let ([l (drop e 2)])
                                                   (sequence-name (to-string (reverse (take (cdr l) (car l)))))])
        (else e)))
  

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
           (to-int (cons x data))
           (f (cons x data)))))
   (let f([data '()] [length length])
     (if (= length 0)
         (interpret-event (reverse data))
         (f (cons (read-byte in) data) (- length 1))))))

(define (read-n-bytes in n)
  (let f([data '()] [i 0])
    (if (= i n)
        data
        (let ([x (read-byte in)])
          (f (cons x data) (+ i 1))))))

(define (to-string l)
  (bytes->string/utf-8 (list->bytes (reverse l))))

(define (to-int l)
  (let ([n 1])
    (foldl (λ (i sum)
             (let ([x (* i n)])
               (set! n (* n 256))
               (+ sum x))) 0 l)))

(define (read-midi-file in)
  (let* ([h (read-header in)]
         [chunks (read-chunks in)])
    (displayln h)
    (display chunks)))

(define (main)
  (read-midi-file (open-input-file "/home/noe/Téléchargements/test.mid" #:mode 'binary)))

(main)

