using Godot;
using System;
using System.Linq;
using TODOBuddy.Tasks;
using Newtonsoft.Json;

public partial class DebugScreen : ColorRect
{
    // this is used to refresh the list with out using Project.TaskListUpdated.Invoke() because that
    // automatically saves changes, this is good for normal use but not when debugging
    public TaskDisplayList _taskDisplayList;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        try
        {
            _taskDisplayList = MainScene.Instance.GetNode($"Container/TaskList_Tab/VBoxContainer/TaskDisplayList") as TaskDisplayList;
        }
        catch ( Exception ex )
        {
            GD.PrintErr($"DebugScreen Error: cannot find _taskDisplayList, (error msg: {ex})");
        }
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

        try
        {
            // this refreshes the list with out saving the changes to file
            _taskDisplayList.Refresh();
        }
        catch ( Exception ex )
        {
            // this saves changes to file
            MainScene.Instance.Project.TaskListUpdated.Invoke(); 
        }

    }
    public void OnClearButtonPressed()
	{
        MainScene.Instance.Project.Tasks = new();

        try
        {
            // this refreshes the list with out saving the changes to file
            _taskDisplayList.Refresh();
        }
        catch (Exception ex)
        {
            // this saves changes to file
            MainScene.Instance.Project.TaskListUpdated.Invoke();
        }

    }
    public void OnLoadButtonPressed()
	{
        MainScene.Instance.Project.LoadTasksFromFile();
    }
    public void OnSaveButtonPressed()
	{
        MainScene.Instance.Project.SaveToFile();

    }


}
