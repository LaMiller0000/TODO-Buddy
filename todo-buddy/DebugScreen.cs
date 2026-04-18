using Godot;
using System;
using System.Linq;
using TODOBuddy.Tasks;
using Newtonsoft.Json;

public partial class DebugScreen : ColorRect
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}
    public static T DeepClone<T>(T obj)
    {
        // Serializes to JSON string, then deserializes to a brand new object instance
        string json = JsonConvert.SerializeObject(obj);
        return JsonConvert.DeserializeObject<T>(json);
    }
    public void OnResetButtonPressed()
	{
        MainScene.Instance.Project.Tasks = TaskHelper.DebugTasks.Select(item => DeepClone<Task>(item)).ToList(); // TaskHelper.DebugTasks.ToList();

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
