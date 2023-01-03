(define (script-fu_kp24_sample_point image drawable)
  (let*
	(
	 )
	(gimp-drawable-edit-fill drawable 0)
	)
  )

(script-fu-register "script-fu_kp24_sample_point"
					"Sample Point coordinates"
					"Sample Point coordinates"
					"Me"
					"Me"
					"2021.01.06"
					"*"
					SF-IMAGE		"Image"	   0
					SF-DRAWABLE     "Drawable" 0
					)
(script-fu-menu-register "script-fu_kp24_sample_point" "<Image>")
