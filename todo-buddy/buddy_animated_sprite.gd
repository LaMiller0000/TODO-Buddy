extends AnimatedSprite2D

enum buddySkinOpts{
	defaultDead,
	defaultIdle,
	defaultSleep,
	evolvedDead,
	evolvedIdle,
	evolvedSleep,
}

enum buddySelectedOpts{
	blueHero, 
	blueSlime, 
	goldSlime, 
	orangeSlime, 
	redDemon, 
}

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#BuddySkin = buddy_selector_option_button
	#BuddySelected = buddy_selector_option_button
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
#changes animation
func _change_animation(newAnimation: int) -> void:
	
	match newAnimation:
		0: self.play("defaultDead");
		1: self.play("defaultIdle");
		2: self.play("defaultSleep");
		3: self.play("defaultDead");
		4: self.play("defaultIdle");
		5: self.play("defaultSleep");
		_: print(newAnimation) #default
	
	print(newAnimation);
	pass
	
#changes sprite set
func _change_sprite_set(newSprite: int) -> void:
	match newSprite:
		0: self.play();
		1: self.play();
		2: self.play();
		3: self.play();
		4: self.play();
		_: print(newSprite) #default
		
	print(newSprite);
	pass
