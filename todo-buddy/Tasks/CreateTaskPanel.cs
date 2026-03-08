using Godot;
using System;

public partial class CreateTaskPanel : Control
{
    // when resizing the panel will go to the closest snap pos
    // measured as the ratio of the parent control's size
    [Export] public float[] SizeSnapPos = new float[] {0.1f, 0.95f, 0.45f};
    [Export] public bool EnableSnapping = true;
    [Export] public float SnapHomeingSpeed = 2;
    // this is used when finding the closest snaping point
    [Export] public float SnapVelocityExtrapolation = 2; // allows flicking to move the pannel

    // when the acuall size of the pannel is within this number it stops updating the size
    public const float SNAPPING_CLOSENESS_FREEZE_THRESHOLD = 1.0f;

    private float _closestSnapPos = 100;

    private bool _isSlideResizing = false;
    private float _prevCursorPosY;
    private float cursorPosYDelta = 0;

	public void OnSlideResize_start()
	{
        _isSlideResizing = true;

        _prevCursorPosY = this.GetGlobalMousePosition().Y;
    }
    public void OnSlideResize_stop()
    {
		_isSlideResizing = false;

        if (EnableSnapping)
            FindClosestSnapPos(ref _closestSnapPos);
    }

    private void FindClosestSnapPos(ref float closestSnapPos)
    {
        // Finds the closest snap position
        float smallestSnapPosDifference = 9999999;
        for (int i = 0; i < SizeSnapPos.Length; i++)
        {
            float currentSnapPosDifference = 
                MathF.Abs( // scaler not vector so sign doesn't matter
                    (SizeSnapPos[i] * GetParent<Control>().Size.Y) // translates the screen ratio to pixels
                    - (this.Size.Y + cursorPosYDelta * SnapVelocityExtrapolation)); // takes into account the cursor velocity so the pannel can be flicked into place

            //GD.Print($"{this.Size.Y} vs {(this.Size.Y + cursorPosYDelta * SnapVelocityExtrapolation)}");

            if (currentSnapPosDifference > smallestSnapPosDifference) continue; 

            smallestSnapPosDifference = currentSnapPosDifference;
            closestSnapPos = SizeSnapPos[i] * GetParent<Control>().Size.Y; // MainScene.Instance.ViewportResolution.Y;

        }
    }

    public void OnResize()
    {
        if (EnableSnapping) FindClosestSnapPos(ref _closestSnapPos);
    }

    public override void _Ready()
    {
        GetParent<Control>().Resized += OnResize;
        if (EnableSnapping) FindClosestSnapPos(ref _closestSnapPos);
    }

    public override void _Process(double delta)
	{
        if (_isSlideResizing)
        {
            // calculates cursor velocity
            float cursorPosY = this.GetGlobalMousePosition().Y;
            cursorPosYDelta = cursorPosY - _prevCursorPosY;
            _prevCursorPosY = cursorPosY;

            //GD.Print($"this.size.Y: {this.Size.Y}");
            //GD.Print($"mouse Pos: {cursorPosY}, delta: {cursorPosYDelta}");

            // uses cursor velocity to resize the pannel
            float sizeChange = cursorPosYDelta;
            this.Size -= new Vector2(0.0f, sizeChange);
            this.Position += new Vector2(0.0f, sizeChange);
        }
        else
        {
            if (MathF.Abs(this.Size.Y - _closestSnapPos) > 0.1f)
            {
                //GD.Print($"this.size.Y: {this.Size.Y}, delta: {_closestSnapPos}");

                // homes the size towards the closest snap position
                Vector2 sizeDelta = new Vector2(0, (this.Size.Y - _closestSnapPos) / SnapHomeingSpeed);
                this.Size -= sizeDelta;
                this.Position += sizeDelta;
            }
        }
    }
}
