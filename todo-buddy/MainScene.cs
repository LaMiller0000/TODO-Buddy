using Godot;
using System;
using System.Collections.Generic;

public partial class MainScene : Control
{
    static public MainScene Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }


}
