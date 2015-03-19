#lang racket

(require "midi-reader.rkt")

(define one 60)

(define (convert midi-data)
  (let ([division (header-division (car midi-data))]
        [delta-time (time-signature-cc (car (track-event-event (track-chunk-events (cadr midi-data)))))])    
    (to-level (car (filter (λ (i) (not (empty? i)))
                 (foldr (λ (i l)
                          (if (track-chunk? i)
                              (cons (foldr (λ (i l) 
                                             (if (midi-event? i)
                                                 (cons i l) l)) '() (track-event-event (track-chunk-events i))) l)
                              l)) '() midi-data))) division delta-time)))

(define (event-off? event)
  (and (> (midi-event-instruction event) 127)
       (< (midi-event-instruction event) 144)))

(define (event-on? event)
  (and (> (midi-event-instruction event) 143)
       (< (midi-event-instruction event) 160)))

(define (event-to-level event delta-time)
  (append (make-list (/ (to-int (midi-event-delta event)) delta-time) 0) '(1)))

(define (to-level events division delta-time)
  (pretty-display events)
  (let f ([level '()] [delta-sum 0] [events events])
    (displayln level)
    (if (empty? events)
        (foldr (λ (i l)
                 (if (= (length (car l)) (/ division delta-time))
                     (cons `(,i) l)
                     (append (append (car l) `(,i)) (cdr l)))) '() level)
        (if (event-on? (car events))
            (f (append level (event-to-level (car events) delta-time)) 0 (cdr events))
            (if (event-off? (car events))
              (f level (+ delta-time (to-int (midi-event-delta (car events)))) (cdr events))
               (f level delta-time (cdr events)))))))
            




(define (main)
  (convert (parse-midi-file (open-input-file "./test.mid" #:mode 'binary))))

(main)