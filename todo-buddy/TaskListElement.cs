using Godot;
using System;

public partial class TaskListElement : Panel
{
    public Task Task;

    // this is a child control, its responsible for resizing the text and other controls based on the size of the element
    // its also seperate so that [tool] can be used so it can run in the editor without this needing it too
    private ListElementOrganizer _organizer;

    //private Label _taskNameLabel => _organizer.TaskNameLabel;
    //private Button _expand_Button => _organizer.Expand_Button;

    // flag to hold if the element is expanded or not
    private bool _expanded = false;

    public override void _Ready()
    {
        Task = TaskHelper.DebugTask_1;

        _organizer = GetChild<ListElementOrganizer>(0);

        _organizer.OnResizeEvent += OnResize;


        if (Task == null) return;
        _organizer.TaskNameLabel.Text = Task.Name;

    }

    public void OnResize(object sender, EventArgs e)
    {
        // this sets size values based on the size of the control, it is used to make the text bigger when there is more space, and smaller when there is less space
        switch ((this.Size.X, this.Size.Y))
        {
            //TODO: add more sizes 
            case ( > 575, > 165):
                _organizer.DueDate_Label.Text = $"{TaskHelper.GetDateSmall(Task.DueDate.Value)}, {Task.DueDate.Value.Year}";
                _organizer.TaskNameLabel.LabelSettings.FontSize = 75;
                break;
            default:
                _organizer.DueDate_Label.Text = TaskHelper.GetDateSmall(Task.DueDate.Value);
                break;
        }
    }
    public void OnExpandButtonPressed()
    {
        _expanded = !_expanded;
        //_organizer.TaskNameLabel.Text = _testValue ? "Pressed!" : Task.Name;
        CustomMinimumSize = _expanded ? new Vector2(450, 400) : new Vector2(450, 130);
    }
}
