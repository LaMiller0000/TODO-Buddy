using System;
using Godot;

public BuddyPageInteractions : public BuddyClass, OptionButton, AnimatedSprite2D {
	static var BuddySkin: string;
	static var BuddySelected: string;
	
	BuddyPageInteractions(){ //Constructor
		BuddySkin = AnimatedSprite2D._on_item_selected();
	}
	
	string getBuddySkin(){
		return BuddySkin;
	}
	string getBuddySelected(){
		return BuddySelected;
	}
	
	string setBuddySkin(){
		return BuddySkin;
	}
	string setBuddySelected(){
		return BuddySelected;
	}	
}
