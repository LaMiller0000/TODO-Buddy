using Godot;
using System;
using TODOBuddy.Tasks;

public partial class DebugScreen : ColorRect
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	public void OnResetButtonPressed()
	{
        MainScene.Instance.Project.Tasks = TaskHelper.DebugTasks;
        MainScene.Instance.Project.TaskListUpdated.Invoke();
    }
	public void OnClearButtonPressed()
	{
        MainScene.Instance.Project.Tasks = new();
        MainScene.Instance.Project.TaskListUpdated.Invoke();

    }
    public void OnLoadButtonPressed()
	{
        //MainScene.Instance.Project = Project.LoadFromFile();
        MainScene.Instance.Project.LoadTasksFromFile();
    }
    public void OnSaveButtonPressed()
	{
        MainScene.Instance.Project.SaveToFile();

    }


}
