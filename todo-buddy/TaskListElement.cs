using Godot;
using System;

public partial class TaskListElement : Panel
{
    public Task Task;

    [Export] public Label TaskNameLabel;
    [Export] public Button Expand_Button;

    private bool _testValue = false;

    public override void _Ready()
    {
        Expand_Button.Pressed += OnExpandButtonPressed;

        base._Ready();
        if (Task == null) return;
        TaskNameLabel.Text = Task.Name;

    }
    public void OnExpandButtonPressed()
    {
        _testValue = !_testValue;
        TaskNameLabel.Text = _testValue ? "Pressed!" : "wowWOWw0w4";
    }
}
