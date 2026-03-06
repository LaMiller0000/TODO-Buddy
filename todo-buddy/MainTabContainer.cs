using Godot;
using System;

[Tool]
public partial class MainTabContainer : Container
{
    //[Export] public int currentTab = -1;
    [Export] public Control tabContainer;

    // this controls how fast you have to drag to change tabs
    [Export] public float TabChangeSwipeSpeed = 10.0f; //TODO: remove if unused
    // if the swipe goes above this threshold before it it will lock out the horizontal 
    [Export] public float VerticalSwipeSpeedTabChangeLockout = 10.0f;

    private SwipeDirection _swipeDirection = SwipeDirection.None;

    private float _screenSwipePercentage = 0.0f;

    private enum SwipeDirection
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
    }

    public override void _Notification(int what)
    {
        if (what == NotificationSortChildren)
        {
            GD.Print($"NotificationSortChildren");
        }
    }

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
                _swipeDirection = SwipeDirection.None;
                EndSwipe();
            }
        }
    }

    public void Swipe(InputEventScreenDrag dragEvent)
    {
        if (Engine.IsEditorHint()) return; // stops if running in the editor

        float positionDelta = dragEvent.ScreenRelative.X;

        tabContainer.Position += new Vector2(positionDelta, 0);

        float totalPosDelta = (0 - tabContainer.Position.X);

        _screenSwipePercentage = totalPosDelta / MainScene.Instance.ViewportResolution.X;

        //GD.Print($"ScreenSwipePercentage: {_screenSwipePercentage}");
    }

    // when swiping ends this will round to whatever screen is closest
    public void EndSwipe()
    {
        //TODO: fully implement
        tabContainer.Position = Vector2.Zero;
    }
    
}


