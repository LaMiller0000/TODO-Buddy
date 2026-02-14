using Godot;
using System;

//[Tool] //TODO: Remove this when not needed, it is only really nessary when adding elements by hand, but when everything is done procedurally it shouldn't be needed
public partial class ListElementOrganizer : MarginContainer
{
    private TaskListElement _parent;

    // only really used to give values in the editor, not useful during runtime
    private Task _debug_task = TaskHelper.DebugTask_1;

    public event EventHandler OnResizeEvent;

    [Export] public Label TaskNameLabel;
    [Export] public Label DueDate_Label;
    [Export] public Button Expand_Button;
    [Export] public VBoxContainer VBox_DateStatus;

    public DateTime Debug_DueDate = new DateTime(new DateOnly(2026, 12, 12), new TimeOnly(23, 59));


    public override void _Ready()
    {
        _parent = GetParent<TaskListElement>();
    }

    // this is called by the control when its resized
    public void OnResize()
    {
        //// if the method is not called in the editor than it skipps the rest  
        //if (!Engine.IsEditorHint()) { OnResizeEvent?.Invoke(this, EventArgs.Empty); return; }

        // this sets size values based on the size of the control, it is used to make the text bigger when there is more space, and smaller when there is less space
        switch ((this.Size.X, this.Size.Y))
        {
            case ( > 575, > 165):
                DueDate_Label.LabelSettings.FontSize = 24;
                DueDate_Label.Text = $"{TaskHelper.GetDateSmall(_debug_task.DueDate.Value)}, {_debug_task.DueDate.Value.Year}";

                TaskNameLabel.LabelSettings.FontSize = 75;
                break;
            default:
                DueDate_Label.Text = TaskHelper.GetDateSmall(_debug_task.DueDate.Value);
                DueDate_Label.LabelSettings.FontSize = 24;

                //VBox_DateStatus.Size = new Vector2(VBox_DateStatus.Size.X, 50);

                TaskNameLabel.LabelSettings.FontSize = 55;
                break;
        }

        OnResizeEvent?.Invoke(this, EventArgs.Empty);
    }


}
