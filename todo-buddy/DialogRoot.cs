using Godot;
using System;
using System.Drawing;

public partial class DialogRoot : Node2D
{
	[Export] public string[] txt = 
	{
		"Test Writing section one.",
		"Test Writing section two.", 
	};
	
	[Export] public Vector2 dimension = new Vector2(300, 300);
	[Export] public int textScale = 24;
	[Export] public float textSpeed = 0.1f;
	private DialogPlayer _dialogPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_dialogPlayer = new DialogPlayer(textSpeed, txt, dimension, textScale);
		AddChild(_dialogPlayer);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
