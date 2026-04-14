extends AnimatedSprite2D

@onready var _buddy = $AnimatedSprite2D;

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
func _change_animation(newAnimation: String) -> void:
	
	match newAnimation:
		0: _buddy.play("defaultDead");
		1: _buddy.play("defaultIdle");
		2: _buddy.play("defaultSleep");
		3: _buddy.play("defaultDead");
		4: _buddy.play("defaultIdle");
		5: _buddy.play("defaultSleep");
		_: print(newAnimation) #default
	
	print(newAnimation);
	pass
	
#changes sprite set
func _change_sprite_set(newSprite: int) -> void:
	match newSprite:
		0: _buddy.play();
		1: _buddy.play();
		2: _buddy.play();
		3: _buddy.play();
		4: _buddy.play();
		_: print(newSprite) #default
		
	print(newSprite);
	pass
