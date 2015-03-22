#lang racket

(provide parse-midi-file)
(provide header-division)
(provide track-chunk?)
(provide track-chunk-events)
(provide track-event-event)
(provide time-signature-cc)
(provide time-signature?)
(provide set-tempo)
(provide sequence-name)
(provide midi-event?)
(provide midi-event-delta)
(provide midi-event-instruction)
(provide midi-event-arg1)
(provide to-int)

(struct header (mthd length format n division) #:transparent)
(struct track-chunk (mtrk length [events #:auto]) #:mutable #:auto-value '() #:transparent)
(struct track-event (v-time event) #:transparent)
(struct time-signature (nn dd cc bb) #:transparent)
(struct set-tempo (tempo) #:transparent)
(struct sequence-name (name) #:transparent)
(struct instrument-name (name) #:transparent)
(struct midi-event (delta instruction arg1 arg2) #:transparent)
(struct key-signature (sf mi) #:transparent)
(struct smpte-offset (hr mn se fr ff) #:transparent)

(define time-signature-event '(255 88 4))
(define set-tempo-event '(255 81 3))
(define sequence-name-event '(255 3))
(define instrument-name-event '(255 4))
(define key-signature-event '(255 89 02))
(define smpte-offset-event '(255 84 05))
(define end-event '(255 47 0))


(define (sublist? l1 l2)
  (andmap (λ (i j)
            (= i j)) l1 (take l2 (length l1))))

(define (known-event? e1 e2)
  (or (sublist? e1 e2) (sublist? e1 (cdr e2))))

(define last 0)
(define rle? #f)

(define (interpret-voice-message e)
  (let f ([delta '()] [l e])
    (cond [(and (= (car l) 0) (= (cadr l) 0))
           (f (append delta `(,(car l))) (cdr l))]
          [(> (car l) 127)
           (f (append delta `(,(car l))) (cdr l))]
          [else (let ([delta (reverse (append delta `(,(car l))))])
                  (cond [(and (>= (cadr l) 192) (< (cadr l) 224))
                         `(,(midi-event delta (cadr l) (caddr l) -1) ,(cdddr l))]
                        [(>= (cadr l) 128)
                         (set! last (cadr l))
                         `(,(midi-event delta last (caddr l) (cadddr l)) ,(cddddr l))]
                        [else
                         `(,(midi-event delta last (cadr l) (caddr l)) ,(cdddr l))]))])))

(define (read-sequence-name l)
  (let f ([length '()] [l l])
    (if (> (car l) 127)
        (f (append length `(,(car l))) (cdr l))
        (let f ([length (to-int (append length `(,(car l))))] [data '()] [l (cdr l)])
          (if (= length 0)
              `(,(to-string data) ,l)
              (f (- length 1) (append data `(,(car l))) (cdr l)))))))

(define (interpret-event e)
  ; (displayln e)
  (define (drop-zero l n)
    (drop l (if (zero? (car l)) (+ 1 n) n)))
  (if (< (length e) 4)
      '()
      (cond ([known-event? time-signature-event e] [let ([l (drop-zero e 3)])
                                                     (cons 
                                                      (time-signature (car l) (cadr l) (caddr l) (cadddr l))
                                                      (interpret-event (drop l 4)))])
            ([known-event? set-tempo-event e] [let ([l (drop-zero e 3)])
                                                (cons
                                                 (set-tempo (to-int (take l 3)))
                                                 (interpret-event (drop l 3)))])
            ([known-event? key-signature-event e] [let ([l (drop-zero e 3)])
                                                    (cons
                                                     (key-signature (car l) (cadr l))
                                                     (interpret-event (drop l 2)))])
            ([known-event? smpte-offset-event e] [let ([l (drop-zero e 3)])
                                                   (cons
                                                    (smpte-offset (car l) (cadr l) (caddr l) (cadddr l) (cadr (cdddr l)))
                                                    (interpret-event (drop l 5)))])
            ([known-event? sequence-name-event e] [let* ([l (drop-zero e 2)] [name (read-sequence-name l)])
                                                    (cons (sequence-name (car name))
                                                          (interpret-event (cadr name)))])
            ([known-event? instrument-name-event e] [let* ([l (drop-zero e 2)] [name (read-sequence-name l)])
                                                      (cons (instrument-name (car name))
                                                            (interpret-event (cadr name)))])
            ([known-event? end-event e] '())
            
            (else [let ([event (interpret-voice-message e)])
                    (cons (car event)
                          (interpret-event (cadr event)))]))))

; (interpret-midi-events e))))


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