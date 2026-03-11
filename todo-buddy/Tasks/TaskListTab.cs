using Godot;
using System;

public partial class TaskListTab : Control
{
    [Export] public CreateTaskPanel CreateTaskPanel;
	public override void _Ready()
	{
        CreateTaskPanel.ClosePanel();
	}
	public void OnFilterChanged(int index)
	{
        // pass this directly to the script handling TaskDisplayList
        throw new NotImplementedException("tried to run OnFilterChanged() which is not implemented yet");
	}
    public void OnCreateButtonPressed()
    {
        // pull up for creating new tasks

        CreateTaskPanel.OpenPanel();
        //throw new NotImplementedException("tried to run OnCreateButtonPressed() which is not implemented yet");
    }
}
