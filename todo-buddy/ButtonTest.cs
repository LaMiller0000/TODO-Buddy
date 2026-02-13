using Godot;
using System;

public partial class ButtonTest : Button
{
    private bool _buttonValue = false;

    public void OnButtonPressed()
    {
        _buttonValue = !_buttonValue;
        this.Text = _buttonValue ? "Button Pressed!" : "wOwoW0wow";
    }
}
