using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class TaskDisplayList : ScrollContainer
{
    [Export] public VBoxContainer TaskListContainer;
    [ExportToolButton("Refresh List")] public Callable RefreshButton => new Callable(this, nameof(Refresh));
    [ExportToolButton("Clear List")] public Callable ClearButton => new Callable(this, nameof(ClearList));

    [Export] public bool ShowCompletedTasks = false;

    [Export] public PackedScene _TaskDisplay;

    [Export] public SortOptions SortOption = SortOptions.DueDate;

    public enum SortOptions
    {
        DueDate = 0,
        Priority = 1,
        CreationDate = 2,
        Alphabetiacally = 3,
    }

    public override void _Ready()
    {
        
        Refresh();

        if (!Engine.IsEditorHint()) MainScene.Instance.Project.TaskListUpdated += Refresh;
    }
    public void Refresh()
    {
        List<Task> taskList;
        if (Engine.IsEditorHint()) taskList = TaskHelper.DebugTasks;
        else taskList = MainScene.Instance.Project.Tasks;

        ClearList();
        GD.Print($"Task Length: {taskList.Count}");

        taskList = sortTaskList(taskList, SortOption);

        foreach (Task task in taskList)
        {
            if (!ShowCompletedTasks && task.Progress == TaskProgress.Completed)
            {
                GD.Print($"Not Adding compleated task: {task.Name}");
                continue;
            }

            GD.Print($"Adding task: {task.Name}");
            TaskListElement taskListElement = _TaskDisplay.Instantiate<TaskListElement>();
            taskListElement.Task = task;
            taskListElement.Refresh();
            TaskListContainer.AddChild(taskListElement);
        }
    }

    private List<Task> sortTaskList(List<Task> tasks, SortOptions sortOption)
    {
        List<Task> sortedTasks; // = new List<Task>();

        switch (sortOption)
        {
            default: //case SortOptions.DueDate:
                sortedTasks = tasks
                    .OrderBy(x => x.DueDate)
                    .ThenBy(x => x.Name)
                    .ToList();
                break;
            case SortOptions.Priority:
                sortedTasks = tasks
                    .OrderBy(x => x.Progress)
                    .ThenBy(x => x.DueDate)
                    .ThenBy(x => x.Name)
                    .ToList();
                break;
            case SortOptions.Alphabetiacally:
                sortedTasks = tasks
                    .OrderBy(x => x.Name)
                    .ToList();
                break;
            case SortOptions.CreationDate:
                sortedTasks = tasks
                    .OrderBy(x => x.CreationDate)
                    .ThenBy(x => x.Name)
                    .ToList();
                break;
        }

        return sortedTasks;
    }

    public void ClearList()
    {
        foreach (var child in TaskListContainer.GetChildren())
        {
            TaskListContainer.RemoveChild(child);
        }
    }
}
