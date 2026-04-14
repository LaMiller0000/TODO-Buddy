using Godot;
using System;

public partial class Interactions : Control
{
	[Export] public int test; 
	[Export] public OptionButton buddy_selector_option_button;
	[Export] public AnimatedSprite2D buddy_animated_sprite;
	
		

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		buddy_selector_option_button.Call("printWord", "booboo");
		GD.Print("booboo");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
