using Godot;
using System;
using System.Linq;

//[Tool]
public partial class MainTabContainer : Container
{
    //[Export] public int currentTab = -1;
    [Export] public Control tabContainer;

    // this controls how fast you have to drag to change tabs
    [Export] public float TabChangeSwipeSpeed = 10.0f; //TODO: remove if unused
    // if the swipe goes above this threshold before it it will lock out the horizontal 
    [Export] public float VerticalSwipeSpeedTabChangeLockout = 10.0f;

    private Vector2 _defaultPosition = Vector2.Zero;

    private SwipeDirection _swipeDirection = SwipeDirection.None;

    private float _screenSwipePercentage = 0.0f;

    //private Godot.Collections.Array<Node> children;
    private Control[] _children;

    private enum SwipeDirection
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
    }

    public override void _Ready()
    {
        _defaultPosition = Position;
    }

    public override void _Notification(int what)
    {
        //GD.Print($"_Notification: {what}");

        if (what == NotificationTransformChanged && Engine.IsEditorHint())
        {
            _defaultPosition = Position;
            //GD.Print("NotificationTransformChanged");
        }

        if (what == NotificationSortChildren)
        {
            //GD.Print($"NotificationSortChildren");

            _children = GetChildren().OfType<Control>().ToArray<Control>();

            foreach (Control child in _children)
            {
                //GD.Print($"  -  {child.Name}");
                FitChildInRect(child,
                    new Rect2(x: 0, y: 0, Size));
                child.Visible = false;
            }

            MoveNextAndPrevChildren();

        }

    }

    private void MoveNextAndPrevChildren()
    {
        Control current = _children[0];
        current.Visible = true;
        current.Position = Vector2.Zero;

        //move the next one to the right side
        Control next = _children[1];
        next.Visible = true;
        next.Position = new Vector2(this.Size.X, 0);

        // moves the last on the the left side
        Control prev = _children[_children.Length - 1];
        prev.Visible = true;
        prev.Position = new Vector2(-this.Size.X, 0);
    }

    private void Display Next 

    public override void _Input(InputEvent @event)
    {
        if (Engine.IsEditorHint()) return; // stops if running in the editor

        if (@event is InputEventScreenDrag dragEvent)
        {
            // can only set the swipe direction if the direction hasn't been set
            if (_swipeDirection == SwipeDirection.None)
            {
                // Vertical swipe lock in
                if (Math.Abs(dragEvent.ScreenRelative.Y) > VerticalSwipeSpeedTabChangeLockout)
                {
                    _swipeDirection = SwipeDirection.Vertical;
                }

                // Horizontal swipe lock in
                if (Math.Abs(dragEvent.ScreenRelative.X) > TabChangeSwipeSpeed)
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

        }

        if (@event is InputEventScreenTouch touchEvent)
        {
            //GD.Print($"touchEvent: {touchEvent.Pressed}");
            if (!touchEvent.Pressed) // runs when stop swiping
            {
                if (_swipeDirection == SwipeDirection.Horizontal)
                {
                    EndSwipe();
                }

                _swipeDirection = SwipeDirection.None;

            }
        }
    }

    public void Swipe(InputEventScreenDrag dragEvent)
    {
        if (Engine.IsEditorHint()) return; // stops if running in the editor

        float positionDelta = dragEvent.ScreenRelative.X;

        /*_children[0].*/Position += new Vector2(positionDelta, 0);

        float totalPosDelta = (0 - Position.X);

        totalPosDelta += dragEvent.ScreenRelative.X * 1;

        _screenSwipePercentage = totalPosDelta / MainScene.Instance.ViewportResolution.X;

        GD.Print($"screenSwipePercentage: {_screenSwipePercentage}");

    }

    // when swiping ends this will round to whatever screen is closest
    public void EndSwipe()
    {
        //TODO: fully implement
        GD.Print($"screenSwipePercentage: {_screenSwipePercentage}");
        switch (_screenSwipePercentage)
        {
            case > 0.5f:
                MoveChild(_children[0], _children.Length - 1);
                //Position = _defaultPosition - new Vector2(Size.X, 5);
                break;
            case < -0.5f:
                MoveChild(_children[_children.Length - 1], 0);
                //Position = _defaultPosition + new Vector2(Size.X, 5);
                break;
            default:
                break;
        }

        //tabContainer.
        Position = _defaultPosition;
    }
    
}


