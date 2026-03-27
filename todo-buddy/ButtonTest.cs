using Godot;
using System;

public partial class ButtonTest : Button
{
    private int _buttonCurrentValue = 0;

    public override void _Ready()
    {
        this.Pressed += OnButtonPressed;
        OnButtonPressed();
    }

    public void OnButtonPressed()
    {
        _buttonCurrentValue++;

        //GD.Print($"_buttonValue: {_buttonCurrentValue}");

        switch (_buttonCurrentValue - 1)
        {
            case 0: this.Text = "This is a button"; break;
            case 1: this.Text = "Dear god"; break;
            case 2: this.Text = "Theres more"; break;
            case 3: this.Text = "noooo"; break;
            default: _buttonCurrentValue = 0; OnButtonPressed();  break;
        }
    }
}
