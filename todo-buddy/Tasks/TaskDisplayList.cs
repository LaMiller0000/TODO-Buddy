using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class TaskDisplayList : ScrollContainer
{
    [Export] public VBoxContainer TaskListContainer;
    [ExportToolButton("Refresh List")] public Callable RefreshButton => new Callable(this, nameof(Refresh));
    [ExportToolButton("Clear List")] public Callable ClearButton => new Callable(this, nameof(ClearList));

    [Export]
    public PackedScene _TaskDisplay;


    public override void _Ready()
    {
        
        Refresh();

        MainScene.Instance.Project.TaskListUpdated += Refresh;
    }
    public void Refresh()
    {
        ClearList();
        GD.Print($"Task Length: {MainScene.Instance.Project.Tasks.Count}");
        foreach (Task task in MainScene.Instance.Project.Tasks)
        {
            GD.Print($"Adding task: {task.Name}");
            TaskListElement taskListElement = _TaskDisplay.Instantiate<TaskListElement>();
            taskListElement.Task = task;
            taskListElement.Refresh();
            TaskListContainer.AddChild(taskListElement);
        }
    }
    public void ClearList()
    {
        foreach (var child in TaskListContainer.GetChildren())
        {
            TaskListContainer.RemoveChild(child);
        }
    }
}
