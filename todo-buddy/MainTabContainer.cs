using Godot;
using System;
using System.Linq;

[Tool]
public partial class MainTabContainer : Container
{

	[Export] public float SmoothScreenTransition = 2; // cannot be 0
	// multiplies how much the final swipe velocity affects the final swipe position
	// when done swiping this multiplys the final position with the velocity 
	// low values = you need to fully swipe past the screen transition to switch screens
	// high values = you would only need to swipe a little distance at a fast speed to switch screens
	[Export] public float ChangeTabFlickPower = 5.0f;

	[ExportGroup("Swipe Speed Threshold")]
	[Export] public float HorizontalSwipeSpeedThreshold = 10.0f; // this controls how fast you have to drag to change tabs
	[Export] public float VerticalSwipeSpeedThreshold = 10.0f; // if the swipe goes above this threshold before it will lock out the horizontal 

	private Vector2 _defaultPosition;
	private Control[] _children;
	private SwipeDirection _swipeDirection = SwipeDirection.None; // this keeps track if the swipe is horizontal (or vertical) 
	
	private enum SwipeDirection
	{
		None = 0,
		Horizontal = 1,
		Vertical = 2,
	}

	// The distance the swipe is between screens
	// 0 is not moved at all,
	// 1 is all the way to Next screen,
	// -1 is all the way to the Previous screen,
	private float _screenSwipeRatio;
	
	public override void _Ready()
	{
		_defaultPosition = Position;
	}
	
	public override void _Input(InputEvent @event)
	{
		if (Engine.IsEditorHint()) return; // stops if running in the editor

		switch (@event)
		{
			case InputEventScreenDrag dragEvent:
				// can only set the swipe direction if the direction hasn't been set
				if (_swipeDirection == SwipeDirection.None)
				{
					// Vertical swipe lock in
					if (Math.Abs(dragEvent.ScreenRelative.Y) > VerticalSwipeSpeedThreshold)
					{
						_swipeDirection = SwipeDirection.Vertical;
					}

					// Horizontal swipe lock in
					if (Math.Abs(dragEvent.ScreenRelative.X) > HorizontalSwipeSpeedThreshold)
					{
						_swipeDirection = SwipeDirection.Horizontal;
					}

					//GD.Print($"ScreenSwipeDirection: {_swipeDirection}");
				}


				if (_swipeDirection == SwipeDirection.Horizontal)
				{
					//GD.Print($"dragEvent: {dragEvent.ScreenRelative}");
					Swipe(dragEvent);
				}
				
				break;
			
			case InputEventScreenTouch touchEvent:
				//GD.Print($"touchEvent: {touchEvent.Pressed}");
				if (!touchEvent.Pressed) // runs when stop swiping
				{
					if (_swipeDirection == SwipeDirection.Horizontal)
					{
						EndSwipe();
					}

					_swipeDirection = SwipeDirection.None;

				}
				
				break;
		}
	}

	public override void _Notification(int what)
	{
		switch (what)
		{
			// updates the default position when this control moves or anything like that
			case (int)NotificationTransformChanged:
				if (Engine.IsEditorHint()) _defaultPosition = Position;
				break;

			// updates the container's children 
			case (int)NotificationSortChildren:
				//GD.Print("NotificationSortChildren");
				_children = GetChildren().OfType<Control>().ToArray<Control>();

				foreach (Control child in _children)
				{
					FitChildInRect(child,
						new Rect2(x: 0, y: 0, Size));
					child.Visible = false;
				}

				Control current = _children[0];
				current.Visible = true;
				current.Position = Vector2.Zero;

				//if (_swipeDirection == SwipeDirection.Horizontal)
				switch (_screenSwipeRatio)
				{
					case (> 0f): DisplayNextTab(); break;
					case (< 0f): DisplayPrevTab(); break;
				}
				break;
		}


	}

	private void DisplayNextTab() // moves the next one to right side
	{
		Control next = _children[1];
		next.Visible = true;
		next.Position = new Vector2(Size.X, 0); //FitChildInRect(next, new Rect2(this.Size.X, 0, Size));
	}
	private void DisplayPrevTab() // moves the last one to the left side
	{
		Control prev = _children[_children.Length - 1];
		prev.Visible = true;
		prev.Position = new Vector2(Size.X * -1, 0);
	}
	
	public void Swipe(InputEventScreenDrag dragEvent)
	{
		if (Engine.IsEditorHint()) return; // stops if running in the editor

		float positionDelta = dragEvent.ScreenRelative.X;
		Position += new Vector2(positionDelta, 0);

		float totalPosDelta = (0 - Position.X);
		totalPosDelta -= dragEvent.ScreenRelative.X * ChangeTabFlickPower;

		_screenSwipeRatio = totalPosDelta / MainScene.Instance.ViewportResolution.X;

		// redraws the children 
		_Notification((int)NotificationSortChildren);
	}

	// when swiping ends this will round to whatever screen is closest
	public void EndSwipe()
	{
		switch (_screenSwipeRatio)
		{
			case > 0.5f:
				MoveChild(_children[0], _children.Length - 1);
				Position = new Vector2(-Position.X, 0);
				break;
			case < -0.5f:
				MoveChild(_children[_children.Length - 1], 0);
				Position = new Vector2(-Position.X, 0);
				break;
		}

		//Position = _defaultPosition;
	}

	public override void _Process(double delta)
	{
		if (_swipeDirection != SwipeDirection.Horizontal)
		{
			if (Position.DistanceTo(_defaultPosition) > 0.1f)
			{
				Position -= (Position - _defaultPosition) / SmoothScreenTransition;
				//Position *= (float)delta;
			}
			else
			{
				Position = _defaultPosition;
				//EndSwipe();
			}
		}
	}
}
