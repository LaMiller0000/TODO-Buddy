using System;
using Godot;

namespace TODOBuddy;

public class BuddyPageInteractions : BuddyClass
{
	static string BuddySkin;
	static string BuddySelected;
	
	BuddyPageInteractions(){ //Constructor
		BuddySkin = AnimatedSprite2D._on_item_selected();
	}
	
	string getBuddySkin(){
		return BuddySkin;
	}
	string getBuddySelected(){
		return BuddySelected;
	}
	
	string setBuddySkin(SelectedSkin: String){
		BuddySkin = SelectedSkin;
		return SelectedSkin;
	}
	string setBuddySelected(Selected: String){
		BuddySelected = Selected;
		return Selected;
	}	
}
