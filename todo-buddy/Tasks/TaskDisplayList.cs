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
        ClearList();
        Refresh();
    }
    public void Refresh()
    {
        GD.Print($"Task Length: {TaskHelper.DebugTasks.Count}");
        foreach (Task task in TaskHelper.DebugTasks)
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
