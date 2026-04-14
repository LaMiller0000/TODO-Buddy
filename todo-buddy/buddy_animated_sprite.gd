extends AnimatedSprite2D

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#BuddySkin = buddy_selector_option_button
	#BuddySelected = buddy_selector_option_button
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
#changes animation
func _change_animation(newAnimation: String) -> void:
	
	print(newAnimation);
	pass
	
#changes sprite set
func _change_sprite_set(newSprite: int) -> void:
	match newSprite:
		0: AnimatedSprite2D.play("") #blue hero
		1: print(newSprite) #blue slime
		2: print(newSprite) #gold slime
		3: print(newSprite) #green slime
		4: print(newSprite) #red demon
		_: print(newSprite) #default
	print(newSprite);
	pass
