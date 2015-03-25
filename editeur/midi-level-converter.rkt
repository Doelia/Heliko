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
(define strict #f)
(define cpt 0)

(define (get-time-signature midi-data)
  (car (foldr (λ (i l)
                (if (track-chunk? i)
                    (foldr (λ (i l)
                             (if (time-signature? i)
                                 `(,i) l))
                           '() (track-event-event (track-chunk-events i))) l)) '() midi-data)))

(define (convert midi-data)
  (let ([division (header-division (car midi-data))]
        [delta-time (time-signature-cc (get-time-signature midi-data))]
        [open? #f])
    (to-level (filter midi-event?
                      (track-event-event (track-chunk-events (last midi-data)))) division delta-time)))

(define (event-off? event)
  (and (>= (midi-event-instruction event) 128)
       (< (midi-event-instruction event) 144)))

(define (event-on? event)
  (and (>= (midi-event-instruction event) 144)
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
  (set! cpt (+ 1 cpt))
  (let ([n (max 0 (- (/ (+ delta-sum (vlq->int (midi-event-delta event))) (/ division 4)) 1))])
    (unless (exact-nonnegative-integer? n)
      (displayln (~a "fichier mal formé : temps incorrects\n\tdelta = " n "\n\tindex : " cpt " \n\tevent : " event "\n"))
      (when strict (displayln "le programme va quitter") (exit)))
    (append (make-list (if (< n 0) 0 (inexact->exact (round n))) 0) `(,(hash-ref notes (midi-event-arg1 event) #\?)))))

(define (export level out extra-zeros)
  (let* ([extra (make-list extra-zeros 0)]
        [level (map (λ (i)
                      (append i extra)) level)])
    (define begin? #t)
    (for-each (λ (i)
                (set! begin? #t)
                (for-each (λ (i)
                            (unless begin? (write-char #\space out))
                            (set! begin? #f)
                            (write i out)) i) (write-char #\newline out)) level)
    (close-output-port out)))

(define (to-level events division delta-time)
  (let f ([level '()] [delta-sum 0] [events events])
    (if (empty? events)
        (transpose (map (λ (i)
                          (if (< (length i) 4)
                              (append i (make-list (- 4 (length i)) 0))
                              i))
                        (reverse
                         (foldl (λ (i l)
                                  (cond[(= (length (car l)) 4) (cons `(,i) l)]
                                       [else (append `(,(append (car l) `(,i))) (cdr l))])) '(()) level))))
        (cond [(event-on? (car events))
               (f (append level (event-to-level (car events) division delta-sum)) 0 (cdr events))]
              [else (f level (+ delta-sum (vlq->int (midi-event-delta (car events)))) (cdr events))]))))

(define (get-input-file args)
  (let ([input-file (cadr (member "-i" (vector->list args)))])
    (unless (= 2 (length (string-split input-file ".")))
      (error (~a "format de fichier non reconnu : " input-file)))
    input-file))

(define (get-extra-zeros args)
  (let ([extra (string->number (cadr (member "-z" (vector->list args))))])
    (unless extra
      (error (~a "un entier est attendu: " extra)))
    extra))

(define (strict? args)
  (let ([s (member "--strict" (vector->list args))])
    (set! strict (list? s))))

(define (help? args)
  (list? (member "-h" (vector->list args))))

(define (display-help)
  (displayln (~a "utilisation : \n\t"
                 "-i <input-file>" "\n\t"
                 "-z <integer>" " : zéros supplémentaires à la fin du fichier\n\t"
                 "--strict : " "arrêt en cas d'erreur dans le fichier")))

(define (main args)
  (cond [(or (< (vector-length args) 2) (help? args))
         (display-help)]
        [else 
         (strict? args)
         (let ([input-file (get-input-file args)] [extra-zeros (get-extra-zeros args)])
           (export (convert (parse-midi-file (open-input-file input-file #:mode 'binary)))
                   (open-output-file (~a (car (string-split input-file ".mid")) ".txt") #:mode 'binary #:exists 'replace)
                   extra-zeros))]))

;(main #("-i" "./logic4.mid" "-z" "8"))
;main #("-i" "./Tamborine.mid" "--strict"))
;(main #("./test.mid"))
(main (current-command-line-arguments))