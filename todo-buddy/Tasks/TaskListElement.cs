using Godot;
using System;

[Tool] //TODO: Maybe Remove this when not needed, it is only really nessary when adding elements by hand, but when everything is done procedurally it shouldn't be needed
public partial class TaskListElement : Panel
{
    public Task Task = TaskHelper.DebugTask_1;

    [ExportToolButton("Refresh Control Labels")] public Callable RefreshButton => new Callable(this, nameof(Refresh));
    [ExportToolButton("Expand")] public Callable ExpandButton => new Callable(this, nameof(OnExpandButtonDown));

    // How long a click needs to be held to count as a long press in milliseconds.
    [Export] public ulong LongPressTimeInMS = 600;
    // The maximum distance the cursor can travel between Expand button down and up before it stops detecting it as a press.
    // To stop stuff like scrolling or swiping from counting as a press.
    [Export] public float PressCursorDistanceLockout = 30.0f;

    [Export] public Label TaskName_Label;
    [Export] public Label Description_Label;
    [Export] public Label DueDate_Label;
    [Export] public Button Expand_Button;
    [Export] public VBoxContainer VBox_DateStatus;
    [Export] public TaskStatusElement Status_Element;

    [Export] public StyleBox Late_Stylebox;
    [Export] public StyleBox Completed_Stylebox;
    
    public CreateTaskPanel CreateTaskPanel;


    // flag to hold if the element is expanded or not
    private bool _expanded = false;
    
    private ulong _buttonDownTime;
    private Vector2 _buttonDownCursorPos;

    public override void _Ready()
    {
        if (Task == null) Task = TaskHelper.DebugTask_1;
        TaskName_Label.LabelSettings = (LabelSettings)TaskName_Label.LabelSettings.Duplicate();

        Refresh();
    }

    
    public void Refresh()
    {
        TaskName_Label.Text = Task.Name;
        Description_Label.Text = Task.Description;
        Status_Element.Task = Task;
        Status_Element.UpdateStatus();


        //this.AddThemeStyleboxOverride("panel", Late_Stylebox);
        if (Task.DueDate.Value < DateTime.Now &&
            Task.Progress != TaskProgress.Completed)
        {
            if (!this.HasThemeStyleboxOverride("panel"))
            {
                this.AddThemeStyleboxOverride("panel", Late_Stylebox);
                GD.Print("-----------------------Add Theme Stylebox Override");
            }
        }
        else if (this.HasThemeStyleboxOverride("panel"))
        {
            GD.Print("Remove Theme Stylebox Override");
            this.RemoveThemeStyleboxOverride("panel");
        }

        if (Task.Progress == TaskProgress.Completed)
        {
            this.AddThemeStyleboxOverride("panel", Completed_Stylebox);
        }



        OnResize();
    }

    public void OnResize()
    {
        // this sets size values based on the size of the control, it is used to make the text bigger when there is more space, and smaller when there is less space
        switch ((this.Size.X, this.Size.Y))
        {
            //TODO: add more sizes 
            case ( > 575, > 165):
                TaskName_Label.LabelSettings.FontSize = 75;

                DueDate_Label.Text = Task.DueDate.HasValue ? $"{TaskHelper.GetDateSmall(Task.DueDate.Value)}, {Task.DueDate.Value.Year}" : "No Due Date";
                break;
            default:
                TaskName_Label.LabelSettings.FontSize = 55;

                DueDate_Label.Text = TaskHelper.GetDateOrNoneSmall(Task.DueDate);
                break;
        }
    }


    public void OnExpandButtonDown()
    {
        _buttonDownTime = Time.GetTicksMsec();
        _buttonDownCursorPos = GetGlobalMousePosition();
        
    }
    public void OnExpandButtonUp()
    {
        ulong pressDelta = Time.GetTicksMsec() - _buttonDownTime;
        float CursorPosDelta = GetGlobalMousePosition().DistanceTo(_buttonDownCursorPos);

        GD.Print($"CursorPosDelta: {CursorPosDelta}");

        if(CursorPosDelta < PressCursorDistanceLockout)
        {
            if ( pressDelta > LongPressTimeInMS)
            {
                LongPress();
            }
            else
            {
                ShortPress();
            }
        }
    }

    /// <summary>
    /// expands the element to show more details, or collapses it to show less details. This is triggered by a short press on the expand button
    /// </summary>
    public void ShortPress()
    {
        _expanded = !_expanded;
        CustomMinimumSize = _expanded ? new Vector2(450, 400) : new Vector2(450, 130);
    }

    /// <summary>
    /// this pulls up a menu for editing the task. This is triggered by a long press
    /// </summary>
    public void LongPress()
    {
        CreateTaskPanel.OpenPanelEdit(Task);
    }
}
