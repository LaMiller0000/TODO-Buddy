using Godot;
using System;

public partial class CreateTaskPanel : Control
{

	private bool _isSlideResizing = false;
    private float _prevCursorPosY;

	public void OnSlideResize_start()
	{
        _isSlideResizing = true;

        _prevCursorPosY = this.GetLocalMousePosition().Y;
    }
    public void OnSlideResize_stop()
    {
		_isSlideResizing = false;
    }

    public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
        //GD.Print($"_isSlideResizing: {_isSlideResizing}");
        if (_isSlideResizing)
        {
            //float cursorPosY = this.GetLocalMousePosition().Y;
            //float cursorPosYDelta = cursorPosY - _prevCursorPosY;
            //_prevCursorPosY = cursorPosY;

            //GD.Print($"mouse Pos: {cursorPosY}, delta: {cursorPosYDelta}");

            //float sizeChange = cursorPosYDelta;
            //this.Size -= new Vector2(0.0f, sizeChange);
            //this.Position += new Vector2(0.0f, sizeChange);

            ////float sizeChange = cursorPosY;
            ////this.Size = new Vector2(this.Size.X, MainScene.Instance.ViewportResolution.Y - sizeChange);
            ////this.Position = new Vector2(this.Position.X, cursorPosY);
        }
    }
}
