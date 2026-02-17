using Godot;
using System;

[Tool]
public partial class TaskStatusElement : Control
{
    [Export] public TaskProgress Progress 
    { 
        get => _progress;
        set
        {
            _progress = value;
            UpdateStatus();
        }
    }

    //private TabContainer _tabContainer = GetChild<TabContainer>(0);

    private TaskProgress _progress = TaskProgress.Todo;

    private void UpdateStatus()
    {
        switch (Progress)
        {
            case TaskProgress.Todo:
                GetChild<TabContainer>(0).CurrentTab = 0;
                break;
            case TaskProgress.InProgress:
                GetChild<TabContainer>(0).CurrentTab = 1;
                break;
            case TaskProgress.Done:
                GetChild<TabContainer>(0).CurrentTab = 2;
                break;
        }
    }
}
