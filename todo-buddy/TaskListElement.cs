using Godot;
using System;

[Tool] //TODO: Maybe Remove this when not needed, it is only really nessary when adding elements by hand, but when everything is done procedurally it shouldn't be needed
public partial class TaskListElement : Panel
{
    public Task Task = TaskHelper.DebugTask_1;

    [ExportToolButton("Refresh Control Labels")] public Callable RefreshButton => new Callable(this, nameof(Refresh));
    [ExportToolButton("Expand")] public Callable ExpandButton => new Callable(this, nameof(OnExpandButtonPressed));

    [Export] public Label TaskName_Label;
    [Export] public Label Description_Label;
    [Export] public Label DueDate_Label;
    [Export] public Button Expand_Button;
    [Export] public VBoxContainer VBox_DateStatus;
    [Export] public TaskStatusElement Status_Element;

    // flag to hold if the element is expanded or not
    private bool _expanded = false;
    
    public override void _Ready()
    {
        if (Task == null) Task = TaskHelper.DebugTask_1;
        Refresh();
    }

    
    public void Refresh()
    {
        TaskName_Label.Text = Task.Name;
        Description_Label.Text = Task.Description;
        Status_Element.Progress = Task.Progress;

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
    public void OnExpandButtonPressed()
    {
        _expanded = !_expanded;
        CustomMinimumSize = _expanded ? new Vector2(450, 400) : new Vector2(450, 130);
    }
}
