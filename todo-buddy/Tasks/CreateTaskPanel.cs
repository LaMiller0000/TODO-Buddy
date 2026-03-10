using Godot;
using System;

public partial class CreateTaskPanel : Control
{
    // when resizing the panel will go to the closest snap pos
    // measured as the ratio of the parent control's size
    //[Export] public float[] SizeSnapPos = new float[] {0.1f, 0.95f, 0.45f};
    [Export] public float ClosedPanelSizeRatio = 0.0f;
    [Export] public float HalfOpenPanelSizeRatio = 0.45f;
    [Export] public float FullOpenPanelSizeRatio = 0.95f;
    [Export] public bool EnableSnapping = true;
    [Export] public float SnapHomeingSpeed = 1.0f;
    // this is used when finding the closest snaping point
    [Export] public float FlickPower = 2; // allows flicking to move the pannel

    // when the acuall size of the pannel is within this number it stops updating the size
    public const float SNAPPING_CLOSENESS_FREEZE_THRESHOLD = 0.01f;

    //the ratio from the bottom of the screen = 0.0f to the top of the screen = 1.0f;
    private float _targetPanelSize_Ratio = 1.0f;

    private bool _isSlideResizing = false;
    private float _prevCursorPosY_px;
    private float cursorPosYDelta = 0;


    private Vector2 _parentSize;
    // multiply a sizeRatio by this to get it as a Size in pixels 
    private float _Ratio2Pixel_sizeY;
    // this is the cached value for turning a size in px to a size ratio
    // its value should be: 1 / Parent.Size.Y
    // its used like this: size.Y * _sizeY2RatioSizeY = sizeYratio
    private float _pixel2Ratio_sizeY;
    // automatically caclulates the sizeYratio using _sizeY2RatioSizeY
    private float sizeYAsRatio => this.Size.Y * _pixel2Ratio_sizeY;

    public void OnSlideResize_start()
	{
        _isSlideResizing = true;

        _prevCursorPosY_px = this.GetGlobalMousePosition().Y;
    }
    public void OnSlideResize_stop()
    {
		_isSlideResizing = false;

        if (EnableSnapping) FindClosestSnapPos(sizeYAsRatio + cursorPosYDelta * FlickPower);
    }

    // Finds the closest snap position
    private void FindClosestSnapPos(float panelSizeYRatio)
    {
        // calculates the midpoints between each panel size position 
        float avgPoint1 = (HalfOpenPanelSizeRatio + ClosedPanelSizeRatio) * 0.5f;
        float avgPoint2 = (FullOpenPanelSizeRatio + HalfOpenPanelSizeRatio) * 0.5f;

        if (sizeYAsRatio < avgPoint1) // Set to closed
        {
            GD.Print($"Set to closed");
            _targetPanelSize_Ratio = ClosedPanelSizeRatio;
        }
        else if (sizeYAsRatio < avgPoint2) // set to half open
        {
            GD.Print($"set to half open");
            _targetPanelSize_Ratio = HalfOpenPanelSizeRatio;
        }
        else // set to full open
        {
            GD.Print($"set to full open");
            _targetPanelSize_Ratio = FullOpenPanelSizeRatio;
        }
    }

    private void OnParentResize()
    // should run once on startup and every time the screen changes resolution
    {
        _parentSize = GetParent<Control>().Size;

        _Ratio2Pixel_sizeY = _parentSize.Y;
        // caches this division so it only has to be used once
        _pixel2Ratio_sizeY = 1 / _Ratio2Pixel_sizeY;

        if (EnableSnapping) FindClosestSnapPos(sizeYAsRatio);
    }

    public override void _Ready() // basically the node's start function
    {
        GetParent<Control>().Resized += OnParentResize;

        OnParentResize();
    }

    public override void _Process(double delta) // runs every tick, its the main loop for general every frame processes
	{
        // checks if currently resizing the panel
        if (_isSlideResizing)
        {
            // calculates distance the cursor traveled between this and the last frame
            float cursorPosY_px = this.GetGlobalMousePosition().Y;
            cursorPosYDelta = cursorPosY_px - _prevCursorPosY_px;
            _prevCursorPosY_px = cursorPosY_px;

            // uses cursor velocity to resize the pannel
            float sizeChange = cursorPosYDelta;
            this.Size -= new Vector2(0.0f, sizeChange);
            this.Position += new Vector2(0.0f, sizeChange);
        }
        else // when not resizing the pannel it moves towards the resize point
        {
            if (MathF.Abs(sizeYAsRatio - _targetPanelSize_Ratio) > SNAPPING_CLOSENESS_FREEZE_THRESHOLD)
            {
                float sizeDelta_Ratio = Mathf.MoveToward(sizeYAsRatio, _targetPanelSize_Ratio, SnapHomeingSpeed * (float)delta);

                // homes the size towards the closest snap position
                Vector2 sizeDelta_Px = new Vector2(this.Size.X, sizeDelta_Ratio * _Ratio2Pixel_sizeY);
                this.Size = sizeDelta_Px;
                this.Position = _parentSize - sizeDelta_Px;
                
            }
        }
    }
}


/*

GD.Print(
    $"_targetPanelSize_Ratio:{_targetPanelSize_Ratio},                      ",
    $"sizeYAsRatio:{sizeYAsRatio},                                          ",
    $"sizeDelta_Ratio:{sizeDelta_Ratio},                                    ",
    $"sizeDelta_Px:{sizeDelta_Px.Y},                                        ",
    $"this.Size.Y:{this.Size.Y},                                            ",
    $"this.Pos.Y:{this.Position.Y},                                         "
); //TODO:Remove this


if (MathF.Abs(sizeYAsRatio - _targetPanelSize_Ratio) > SNAPPING_CLOSENESS_FREEZE_THRESHOLD)
            {
                float sizeDelta_Ratio = Mathf.MoveToward(sizeYAsRatio, _targetPanelSize_Ratio, SnapHomeingSpeed);

                // homes the size towards the closest snap position
                Vector2 sizeDelta_Px = new Vector2(this.Size.X, sizeDelta_Ratio * _Ratio2Pixel_sizeY); // (0, (this.Size.Y - _closestSnapPos) / SnapHomeingSpeed);
                this.Size = sizeDelta_Px;
                this.Position = new Vector2(GetParent<Control>().Size.X, GetParent<Control>().Size.Y) - sizeDelta_Px;
                GD.Print(
                    $"_targetPanelSize_Ratio:{_targetPanelSize_Ratio},                      ",
                    $"sizeYAsRatio:{sizeYAsRatio},                                          ",
                    $"sizeDelta_Ratio:{sizeDelta_Ratio},                                    ",
                    $"sizeDelta_Px:{sizeDelta_Px.Y},                                        ",
                    $"this.Size.Y:{this.Size.Y},                                            ",
                    $"this.Pos.Y:{this.Position.Y},                                         "
                ); //TODO:Remove this
            }
            else GD.Print("CreateTaskPanel.cs: Done Moveing to snap position"); //TODO:Remove this 
*/
