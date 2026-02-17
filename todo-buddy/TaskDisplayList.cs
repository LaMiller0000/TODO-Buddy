using Godot;
using System;
using System.Collections.Generic;

//[Tool]
public partial class TaskDisplayList : ScrollContainer
{
    [Export] public VBoxContainer TaskListContainer;

    //public List<Task> Tasks = TaskHelper.DebugTasks;

    private PackedScene _TaskDisplay;

    public override void _Ready()
    {
        foreach ( var child in TaskListContainer.GetChildren())
        {
            TaskListContainer.RemoveChild(child);
        }

        _TaskDisplay = ResourceLoader.Load<PackedScene>("res://task_list_element.tscn");

        foreach (var task in TaskHelper.DebugTasks)
        {
            GD.Print($"Adding task: ");

            TaskListElement taskListElement = _TaskDisplay.Instantiate<TaskListElement>();
            taskListElement.Task = task;
            TaskListContainer.AddChild(taskListElement);
        }
    }
}
