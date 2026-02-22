using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Tool]
public class Task
{
    /// <summary>Holds the name of the task.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Holds the task's description.</summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>The due date of the task. Null if no due date is set.</summary>
    public DateTime? DueDate { get; set; } = null;
    /// <summary>The date the task was created</summary>
    public DateTime CreationDate { get; set; } = DateTime.Now;
    /// <summary>Holds if the Task is compleated</summary>
    public TaskProgress Progress { get; set; } = TaskProgress.Todo;

    /// <summary>Gets an immutable list of tasks this task depends on. null if no dependancys. Inverse of <seealso cref="Subtasks"/>.</summary>
    /// <remarks>Use <seealso cref="AddDependancy(Task)"/> and <see cref="RemoveDependancy(Task)"/> to edit list. </remarks>
    public ImmutableList<Task>? Dependancies { 
        get => _dependancies.ToImmutableList();
    }
    /// <summary>Gets an immutable list of the tasks that depend on this task. null if no subtasks. Inverse of <seealso cref="Dependancies"/>.</summary>
    /// <remarks>Use <see cref="AddSubtask(Task)"/> and <see cref="RemoveSubtask(Task)"/> to edit list.</remarks>
    public ImmutableList<Task>? Subtasks { 
        get => _subtasks.ToImmutableList();
    }

    // tasks that this task depends on. null if nothing
    private List<Task>? _dependancies = null;
    // things that depend on this task. null if nothing
    private List<Task>? _subtasks = null;

    /// <summary>
    /// Adds a dependency to the current task, establishing a bidirectional relationship between the tasks.
    /// </summary>
    /// <remarks>This method ensures that the specified task is added as a dependency only once. It also adds
    /// the current task as a subtask of the specified dependency, maintaining a consistent bidirectional link between
    /// tasks.</remarks>
    /// <param name="task">The task to add as a dependency. Cannot be <c>null</c>.</param>
    public void AddDependancy(Task task)
    {
        // initialize list (if needed)
        if (_dependancies == null) _dependancies = new List<Task>();

        // avoid duplicates
        if (!_dependancies.Contains(task)) _dependancies.Add(task);

        // ensure bidirectional link
        if (!task.Subtasks.Contains(this)) task.AddSubtask(this);

    }
    /// <summary></summary>
    /// <remarks></remarks>
    /// <param name="task">The task to remove as a dependency. Cannot be <c>null</c>.</param>
    public void RemoveDependancy(Task task)
    {
        // if no dependancies, nothing to remove
        if (_dependancies == null) return;
        // remove dependancy
        _dependancies.Remove(task);
        // remove bidirectional link
        if (task.Subtasks.Contains(this)) task.RemoveSubtask(this);
    }

    /// <summary></summary>
    /// <remarks></remarks>
    /// <param name="task">The task to add as a subTask. Cannot be <c>null</c>.</param>
    public void AddSubtask(Task task)
    {
        // initialize list (if needed)
        if (_subtasks == null) _subtasks = new List<Task>();

        // avoid duplicates
        if (!_subtasks.Contains(task)) _subtasks.Add(task);

        // ensure bidirectional link
        if (!task.Dependancies.Contains(this)) task.AddDependancy(this);
    }
    /// <summary></summary>
    /// <remarks></remarks>
    /// <param name="task">The subTask to be removed. Cannot be <c>null</c>.</param>
    public void RemoveSubtask(Task task)
    {
        // if no subtasks, nothing to remove
        if (_subtasks == null) return;
        // remove subtask
        _subtasks.Remove(task);
        // remove bidirectional link
        if (task.Dependancies.Contains(this)) task.RemoveDependancy(this);
    }

}

/// <summary>
/// Specifies the progress state of a task.
/// </summary>
/// <remarks>Use this enumeration to indicate whether a task is yet to be started, currently in progress, or
/// completed.</remarks>
public enum TaskProgress
{
    Todo = 0,
    InProgress = 1,
    Done = 2,
    Completed = Done,
}

