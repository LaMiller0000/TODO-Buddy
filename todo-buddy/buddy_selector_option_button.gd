extends OptionButton


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func _on_item_selected(index: int) -> void: #ER, On Item Selected in Drop Down
	match index:
		0: print(index) #blue hero
		1: print(index) #blue slime
		2: print(index) #gold slime
		3: print(index) #green slime
		4: print(index) #red demon
		_: print(index) #default
	pass
