using Godot;
using System;
using System.Globalization;

public partial class CreateTaskPanel : Control
{
	// when the size_ratio of the pannel is within this number it stops updating the size
	public const float SNAPPING_CLOSENESS_FREEZE_THRESHOLD = 0.01f;

	[Export] public bool EnableSnapping = true;

	// how far the panel will move in one second
	[Export] public float SnapHomeingSpeed = 6.0f;

	// allows flicking to move the pannel
	// this is used when finding the closest snaping point
	[Export] public float FlickPower = 0;
	
	[Export] public TaskDisplayList TaskDisplayList;
	[Export] public LineEdit NameInput;
	[Export] public TextEdit DescriptionInput;

	[Export] public SpinBox Due_MonthInput;
	[Export] public SpinBox Due_DayInput;
	[Export] public SpinBox Due_YearInput;

    [Export] public SpinBox Start_MonthInput;
    [Export] public SpinBox Start_DayInput;
    [Export] public SpinBox Start_YearInput;

    [Export] public OptionButton ProgressInput;

    [Export] public Button CancelButton;
	[Export] public Button SaveButton;
	[Export] public Button DeleteButton;

	[ExportGroup("Snap Points")]
	// ***** Snap points ***** //
	// after resizing, the panel will go to the closest snap pos
	// measured as the ratio of the parent control's size
	[Export] public float ClosedPanelSizeRatio = 0.0f;
	[Export] public float HalfOpenPanelSizeRatio = 0.35f;
	[Export] public float FullOpenPanelSizeRatio = 0.95f;
	
	


	public event Action PannelClosed;

	private Task _editingTask = null;

	// true when the user is swiping the panel up or down
	private bool _isResizing = false;
	private float _prevCursorPosY_px;
	private float cursorPosYDelta = 0;

	//the ratio from the bottom of the screen = 0.0f to the top of the screen = 1.0f;
	private float _targetPanelSize_Ratio = 1.0f;

	private Vector2 _parentSize_Px;
	// used to convert sizeY ratio value to sizeY pixel value
	// multiply a sizeRatio by this to get it as a Size in pixels 
	private float _Ratio2Pixel_sizeY;
	// used to convert sizeY pixel value to sizeY ratio value
	// multiply a sizeY ratio value by this to get it as a SizeY in pixels 
	// this is the cached value for turning a size in px to a size ratio
	// its value should be: 1 / Parent.Size.Y
	private float _pixel2Ratio_sizeY;
	// automatically caclulates the sizeYratio using _sizeY2RatioSizeY
	private float sizeYAsRatio => this.Size.Y * _pixel2Ratio_sizeY;

	public void OpenPanel()
	{
		this.Visible = true;
		SetPanelPosition(HalfOpenPanelSizeRatio);
	}
	
	public void OpenPanelEdit(Task task)
	{
		_editingTask = task;

		OpenPanel();

		DeleteButton.Visible = true;

		NameInput.Text = task.Name;
		DescriptionInput.Text = task.Description;
		Start_DayInput.Value = task.CreationDate.Day;
		Start_MonthInput.Value = task.CreationDate.Month;
		Start_YearInput.Value = task.CreationDate.Year;
		Due_YearInput.Value = (int)task.DueDate?.Year;
		Due_MonthInput.Value = (int)task.DueDate?.Month;
		Due_DayInput.Value = (int)task.DueDate?.Day;
		ProgressInput.Select((int)task.Progress);
	}
	
	public void SetPanelPosition(float newSize_ratio)
	{
		FindClosestSnapPos(newSize_ratio);
	}
	public void ClosePanel()
	{
		SetPanelPosition(ClosedPanelSizeRatio);
		PannelClosed?.Invoke(); 
	}

	public void OnSlideResize_start()
	{
		_isResizing = true;

		_prevCursorPosY_px = this.GetGlobalMousePosition().Y;
	}
	public void OnSlideResize_stop()
	{
		_isResizing = false;

		if (EnableSnapping) FindClosestSnapPos(sizeYAsRatio + cursorPosYDelta * FlickPower);
	}

	// Finds the closest snap position
	private void FindClosestSnapPos(float panelSizeYRatio)
	{
		// calculates the midpoints between each panel size position 
		float avgPoint1 = (HalfOpenPanelSizeRatio + ClosedPanelSizeRatio) * 0.5f;
		float avgPoint2 = (FullOpenPanelSizeRatio + HalfOpenPanelSizeRatio) * 0.5f;

		if (panelSizeYRatio < avgPoint1) // Set to closed
		{
			GD.Print($"Set to closed");

			PannelClosed?.Invoke(); 
			_targetPanelSize_Ratio = ClosedPanelSizeRatio;
		}
		else if (panelSizeYRatio < avgPoint2) // set to half open
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
		_parentSize_Px = GetParent<Control>().Size;

		_Ratio2Pixel_sizeY = _parentSize_Px.Y;
		// caches this division so it only has to be used once
		_pixel2Ratio_sizeY = 1 / _Ratio2Pixel_sizeY;

		if (EnableSnapping) FindClosestSnapPos(sizeYAsRatio);
	}

	public override void _Ready() // basically the node's start function
	{
		GetParent<Control>().Resized += OnParentResize;

		OnParentResize();

		ResetValues();

        CancelButton.Pressed += CancelButton_Pressed;
		SaveButton.Pressed += SaveButton_Pressed;
		DeleteButton.Pressed += DeleteButton_Pressed;
		
		ProgressInput.AddItem("Todo",       (int)TaskProgress.Todo);
		ProgressInput.AddItem("In Progress", (int)TaskProgress.InProgress);
		ProgressInput.AddItem("Done",        (int)TaskProgress.Done);
	}

	private void CancelButton_Pressed()
	{
		ResetValues();
    }

	private void DeleteButton_Pressed()
	{
		if (_editingTask == null) return;

		MainScene.Instance.Project.Tasks.Remove(_editingTask);
		MainScene.Instance.Project.TaskListUpdated.Invoke();

		ResetValues();
	}

	// Updates the day ranges based on what the month is 
	private void OnDue_MonthInput_Changed(float value)
	{
		Due_DayInput.MaxValue = TaskHelper.GetDaysInMonth((byte)Due_MonthInput.Value);
		GD.Print($"OnDue_MonthInput_Changed({value})");
    }
    private void OnStart_MonthInput_Changed(float value)
    {
        Start_DayInput.MaxValue = TaskHelper.GetDaysInMonth((byte)Start_MonthInput.Value);
        GD.Print($"OnStart_MonthInput_Changed({value})");

    }


    private void ResetValues()
	{
        ClosePanel();
        NameInput.Clear();
        DescriptionInput.Clear();

        DateOnly now = DateOnly.FromDateTime(DateTime.Now);

        Start_DayInput.Value = (now.Day);
        Start_MonthInput.Value = (now.Month);
        Start_YearInput.Value = (now.Year);

        DateOnly due = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));

        Due_YearInput.Value = (due.Year);
        Due_MonthInput.Value = (due.Month);
        Due_DayInput.Value = (due.Day);
        
        DeleteButton.Visible = false;
        _editingTask = null;
    }

	private void SaveButton_Pressed()
	{
		if (_editingTask != null)
		{
			_editingTask.Name = NameInput.Text;
			_editingTask.Description = DescriptionInput.Text;
			_editingTask.CreationDate = new DateTime((int)Start_YearInput.Value, (int)Start_MonthInput.Value, (int)Start_DayInput.Value);
			_editingTask.DueDate = new DateTime((int)Due_YearInput.Value, (int)Due_MonthInput.Value, (int)Due_DayInput.Value);
			_editingTask.Progress = (TaskProgress)ProgressInput.GetSelectedId();
		}
		else
		{
			var task = new Task
			{
				Name = NameInput.Text,
				Description = DescriptionInput.Text,
				DueDate = new DateTime((int)Due_YearInput.Value, (int)Due_MonthInput.Value, (int)Due_DayInput.Value),
				CreationDate = new DateTime((int)Start_YearInput.Value, (int)Start_MonthInput.Value, (int)Start_DayInput.Value),
				Progress = (TaskProgress)ProgressInput.GetSelectedId(),
			};

			MainScene.Instance.Project.Tasks.Add(task);
		}

		MainScene.Instance.Project.TaskListUpdated.Invoke();
		ResetValues();
	}

	public override void _Process(double delta) // runs every tick, its the main loop for general every frame processes
	{
		// checks if currently resizing the panel
		if (_isResizing)
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
				this.Position = _parentSize_Px - sizeDelta_Px;
			}
			else if (_targetPanelSize_Ratio <= ClosedPanelSizeRatio)
			{
				this.Visible = false;
			}
		}
	}
}
