using Godot;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TODOBuddy.Tasks;
/// <summary>
/// Currently, this just holds the task list.
/// <para>The app currently only supports one project but this is set of for when / if that that is added in the future.</para>
/// </summary>
public class Project
{
    // This is so the json files have a version number
    public string SaveVersion = "0.1";
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();

    // TODO: add this when User is implemented
    //public List<User> Users { get; set; } = new List<User>();

    [JsonIgnore]
    public Action TaskListUpdated;

    public Project() { }

    #region Save Load Stuff

    // The user:// folder, on windows, is located here:
    // %appdata%\..\Roaming\Godot\app_userdata\TODOBuddy
    public bool SaveToFile(string path = "user://save.json")
    {
        string content = JsonConvert.SerializeObject(this, Formatting.Indented);

        using FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Write);

        return file.StoreString(content);
    }
    public bool LoadTasksFromFile(string path = "user://save.json")
    {
        using var file = FileAccess.Open("user://save.json", FileAccess.ModeFlags.Read);

        string content = file.GetAsText();

        var project = JsonConvert.DeserializeObject<Project>(content);

        Tasks = project.Tasks;

        TaskListUpdated.Invoke();

        return true;
    }
    public static Project LoadFromFile(string path = "user://save.json")
    {
        using var file = FileAccess.Open("user://save.json", FileAccess.ModeFlags.Read);

        string content = file.GetAsText();

        var project = JsonConvert.DeserializeObject<Project>(content);

        return project;
    }

    #endregion
}
