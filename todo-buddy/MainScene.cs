using Godot;
using System;
using System.Collections.Generic;

public partial class MainScene : Control
{
	static public MainScene Instance { get; private set; }

	public Vector2 ViewportResolution { get => GetViewport().GetVisibleRect().Size; }

	/// <remarks> Use the methods in TaskHelper.cs to change this.</remarks>
	public List<Task> _Tasks = new List<Task>();

	public override void _Ready()
	{
		Instance = this;
	}


}
