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
  (pretty-display midi-data)
  (let ([division (header-division (car midi-data))]
        [delta-time (time-signature-cc (get-time-signature midi-data))]
        [open? #f])
    (to-level (filter midi-event?
                      (track-event-event (track-chunk-events (last midi-data)))) division delta-time)))

(define (event-on? event)
  (and (> (midi-event-instruction event) 127)
       (< (midi-event-instruction event) 144)))

(define (event-off? event)
  (and (> (midi-event-instruction event) 143)
       (< (midi-event-instruction event) 160)))

(define (chan-change? event)
  (and (> (midi-event-instruction event) 191)
       (< (midi-event-instruction event) 208)))

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
  (displayln (/ (+ delta-sum (vlq->int (midi-event-delta event))) (/ division 4)))
  (let ([n (- (/ (+ delta-sum (vlq->int (midi-event-delta event))) (/ division 4)) 1)])
    (append (make-list (if (< n 0) 0 (inexact->exact (round n))) 0) `(,(hash-ref notes (midi-event-arg1 event) #\?)))))

(define (export level out)
  (define begin? #t)
  (for-each (λ (i)
              (set! begin? #t)
              (for-each (λ (i)
                          (unless begin? (write-char #\space out))
                          (set! begin? #f)
                          (write i out)) i) (write-char #\newline out)) level)
  (close-output-port out))

(define (to-level events division delta-time)
  (let f ([level '()] [delta-sum 0] [events events])
    ;(displayln delta-sum)
    (if (empty? events)
        (transpose (filter (λ (i)
                             (= (length i) 4))
                           (reverse
                            (foldl (λ (i l)
                                     (cond[(= (length (car l)) 4) (cons `(,i) l)]
                                          [else (append `(,(append (car l) `(,i))) (cdr l))])) '(()) level))))
        (cond [(event-off? (car events))
               (f (append level (event-to-level (car events) division delta-sum)) 0 (cdr events))]
              [(event-on? (car events))
               (f level (+ delta-sum (vlq->int (midi-event-delta (car events)))) (cdr events))]
              [(chan-change? (car events))
               (f level (+ delta-sum (vlq->int (midi-event-delta (car events)))) (cdr events))]
              [else 
               (f level 0 (cdr events))]))))

(define (main)
 (export (convert (parse-midi-file (open-input-file "./Tamborine_flstudio.mid" #:mode 'binary))) (open-output-file "out.txt" #:mode 'binary #:exists 'replace)))
;(export (convert (parse-midi-file (open-input-file "./Tamborine.mid" #:mode 'binary))) (open-output-file "out.txt" #:mode 'binary #:exists 'replace)))
 ; (export (convert (parse-midi-file (open-input-file "./test4.mid" #:mode 'binary))) (open-output-file "out.txt" #:mode 'binary #:exists 'replace)))

(main)