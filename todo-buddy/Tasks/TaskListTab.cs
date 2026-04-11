using Godot;
using System;

public partial class TaskListTab : Control
{
	[Export] public CreateTaskPanel CreateTaskPanel;
	[Export] public TaskDisplayList TaskDisplayList;

    public override void _Ready()
	{
		CreateTaskPanel.ClosePanel();
	}
	public void OnFilterChanged(int index)
	{
		TaskDisplayList.SortOptions filterOptions = (TaskDisplayList.SortOptions)index;

        TaskDisplayList.SortOption = filterOptions;

        TaskDisplayList.Refresh();
	}
	public void OnShowCompletedTasks_Pressed()
	{
		throw new NotImplementedException();
	}

    public void OnCreateButtonPressed()
	{
		// pull up for creating new tasks
		CreateTaskPanel.OpenPanel();
	}

	
}
