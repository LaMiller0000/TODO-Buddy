using Godot;
using System;

public partial class ButtonTest : Button
{
    public void OnButtonPressed()
    {
        this.Text = "Button Pressed!";
    }
}
