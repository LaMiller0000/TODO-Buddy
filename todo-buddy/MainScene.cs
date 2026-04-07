using Godot;
using System;
using System.Collections.Generic;
using TODOBuddy.Tasks;

public partial class MainScene : Control
{
	static public MainScene Instance { get; private set; }

	public Vector2 ViewportResolution { get => GetViewport().GetVisibleRect().Size; }

	// contains all the tasks(as well as other stuff
	public Project Project = new Project();

    public override void _EnterTree()
    {
		Instance = this;


        //Project.Tasks = TaskHelper.DebugTasks;
        //Project.SaveToFile();

        Project = Project.LoadFromFile();
        TaskHelper.DebugTasks = Project.Tasks;
    }


}
