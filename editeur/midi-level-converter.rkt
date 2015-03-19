#lang racket

(require "midi-reader.rkt")

(define (convert midi-data)
  (car (filter (λ (i) (not (empty? i)))
               (foldr (λ (i l)
                        (if (track-chunk? i)
                            (cons (foldr (λ (i l) 
                                           (if (midi-event? i)
                                               (cons i l) l)) '() (track-event-event (track-chunk-events i))) l)
                            l)) '() midi-data))))


(define (main)
  (convert (parse-midi-file (open-input-file "./test.mid" #:mode 'binary))))

(main)