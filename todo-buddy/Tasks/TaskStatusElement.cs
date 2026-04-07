using Godot;
using System;

[Tool]
public partial class TaskStatusElement : Control
{
    [Export] public TabContainer _tabContainer; 
    [Export] public OptionButton _optionButton;

    public Task Task;

    public void UpdateStatus()
    {
        _optionButton.Select((int)Task.Progress);
        _tabContainer.CurrentTab = (int)Task.Progress;
    }

    public void OnSelectionChange(int index)
    {
        Task.Progress = (TaskProgress)index;
        UpdateStatus();
    }
}
