#lang racket

(require "midi-reader.rkt")

(define one 60)
(define notes (hash 60 1
                    61 2
                    62 3
                    63 4
                    72 1
                    73 2
                    74 3
                    75 4))

(define (get-time-signature midi-data)
  (car (foldr (λ (i l)
                (if (track-chunk? i)
                    (foldr (λ (i l)
                             (if (time-signature? i)
                                 `(,i) l))
                           '() (track-event-event (track-chunk-events i))) l)) '() midi-data)))

(define (convert midi-data)
 ; (pretty-display midi-data)
  (let ([division (header-division (car midi-data))]
        [delta-time (time-signature-cc (get-time-signature midi-data))]
        [open? #f])
    (to-level (filter (λ (i)
                        (if (midi-event? i)
                            (begin
                              (when (= (midi-event-instruction i) 192) (set! open? (not open?)))
                              open?)
                            #f))
                            (track-event-event (track-chunk-events (last midi-data)))) division delta-time)))

(define (event-off? event)
  (and (> (midi-event-instruction event) 127)
       (< (midi-event-instruction event) 144)))

(define (event-on? event)
  (and (> (midi-event-instruction event) 143)
       (< (midi-event-instruction event) 160)))

(define (vlq->int l)
  (let f ([i 0] [l l] [sum 0])
    (if (empty? l)
        sum
        (f (+ 1 i) (cdr l) (+ sum (* (if (> (car l) 127) (- (car l) 128) (car l)) (expt 128 i)))))))

(define (transpose matrix)
  (if (empty? (car matrix))
      '()
      (cons (map (λ (i)
                   (car i)) matrix) (transpose (map cdr matrix)))))

(define (event-to-level event division delta-sum)
  (let ([n (- (/ (+ delta-sum (vlq->int (midi-event-delta event))) (/ division 4)) 1)])
  (append (make-list (if (or (< n 0) (not (exact-nonnegative-integer? n))) 0 n) 0) `(,(hash-ref notes (midi-event-arg1 event) #\?)))))

(define (export level out)
  (for-each (λ (i)
              (for-each (λ (i)
                          (write i out)) i) (write-char #\newline out)) level)
  (close-output-port out))

(define (to-level events division delta-time)
  (let f ([level '()] [delta-sum 0] [events events])
    (if (empty? events)
        (transpose (filter (λ (i)
                             (= (length i) 4))
                           (reverse
                            (foldl (λ (i l)
                                     (cond[(= (length (car l)) 4) (cons `(,i) l)]
                                          [else (append `(,(append (car l) `(,i))) (cdr l))])) '(()) level))))
        (if (event-on? (car events))
            (f (append level (event-to-level (car events) division delta-sum)) 0 (cdr events))
            (if (event-off? (car events))
                (f level (+ delta-sum (vlq->int (midi-event-delta (car events)))) (cdr events))
                (f level delta-sum (cdr events)))))))

(define (main)
  (export (convert (parse-midi-file (open-input-file "/home/noe/Documents/oufmania/music/Tamborine.mid" #:mode 'binary))) (open-output-file "out.txt" #:mode 'binary #:exists 'replace)))

(main)