using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

[Tool]
internal class TaskHelper
{
    #region Debug Tasks


    public static Task NullTask = new Task
    {
        Name = "Null Task",
        Description = "If you see this there has been an error."
    };

    //These are some filler tasks, mostly just used to fill in data while editing
    public static Task DebugTask_1 = new Task
    {
        Name = "Task 1",
        Description = "Description / notes 1    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna mauris, condimentum sit amet sem quis, scelerisque ornare sapien. Integer maximus congue nisl, sed egestas lorem hendrerit at. Donec vehicula ut orci id varius. Sed molestie ut nisi sit amet dictum. Quisque commodo maximus hendrerit.",
        DueDate = new DateTime(new DateOnly(2026, 12, 12), new TimeOnly(23, 59)),
        CreationDate = new DateTime(new DateOnly(2024, 6, 1), new TimeOnly(12, 0)),
        Progress = TaskProgress.Todo
    };

    public static List<Task> DebugTasks = new List<Task>
    {
        new Task {
            Name = "Task 1",
            Description = "Description / notes 1    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna mauris, condimentum sit amet sem quis, scelerisque ornare sapien. Integer maximus congue nisl, sed egestas lorem hendrerit at. Donec vehicula ut orci id varius. Sed molestie ut nisi sit amet dictum. Quisque commodo maximus hendrerit.",
            DueDate = new DateTime(new DateOnly(2026, 12, 12), new TimeOnly(23, 59)),
            CreationDate = new DateTime(new DateOnly(2024, 6, 1), new TimeOnly(12, 0)),
            Progress = TaskProgress.Todo
        },
        new Task {
            Name = "Task 2",
            Description = "Description / notes 2    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna mauris, condimentum sit amet sem quis, scelerisque ornare sapien. Integer maximus congue nisl, sed egestas lorem hendrerit at. Donec vehicula ut orci id varius. Sed molestie ut nisi sit amet dictum. Quisque commodo maximus hendrerit. Praesent aliquet tortor molestie, suscipit mi sit amet, sollicitudin eros. Proin a imperdiet neque. In id neque id justo tristique pellentesque sit amet vitae ante. Phasellus in mattis ante. Ut pellentesque elit sit amet mauris interdum, ac aliquam ex dictum. Vivamus id massa sed dolor mattis vestibulum at rutrum turpis.\r\n\r\nCurabitur vitae posuere nulla, ac iaculis ex. Aliquam tempor neque vel erat pretium convallis. Donec non turpis non velit venenatis ultricies. Nunc pharetra tempor condimentum. Donec leo justo, varius id pellentesque ac, laoreet a felis. Ut vulputate, elit a ornare gravida, ligula ante tempor massa, ac luctus orci justo nec elit. Nunc non lacinia ante, ut molestie ex. Vivamus sodales vulputate pellentesque. Pellentesque vitae lacinia mi, at tincidunt velit. Integer mattis nunc in nisi lacinia, eu pulvinar nunc tincidunt. Fusce laoreet purus et velit tincidunt volutpat. Proin et erat dolor. Morbi iaculis ut ipsum a hendrerit. Morbi et suscipit risus, sit amet vehicula ex. In nec augue eu ipsum rutrum tempus id sed turpis. Mauris vestibulum molestie erat vel dapibus.\r\n\r\nPellentesque molestie vitae neque vitae molestie. Nunc pretium enim non dui sodales viverra. Curabitur elit dui, rhoncus at hendrerit nec, molestie non ex. Donec nibh diam, ultricies semper sagittis non, rhoncus a velit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Pellentesque mauris massa, bibendum id accumsan quis, ornare et lorem. In ac volutpat justo. Aliquam condimentum arcu vitae ipsum iaculis, nec feugiat lorem accumsan. Vivamus dapibus lacus sed orci rutrum rutrum. Quisque commodo non tellus sit amet faucibus. Quisque eros enim, auctor ac malesuada in, vestibulum sed ligula.\r\n\r\nInteger tortor arcu, viverra non ipsum in, sodales blandit odio. Ut elementum ac diam ut fermentum. Vestibulum vitae orci nec risus sollicitudin commodo ut sit amet enim. Donec maximus placerat magna sed euismod. Nam erat sem, volutpat vitae urna id, blandit pharetra arcu. Donec scelerisque dapibus ligula ac dictum. Suspendisse malesuada pharetra pellentesque. Donec hendrerit nec nibh at hendrerit. Phasellus ac hendrerit sem. Vestibulum maximus ligula odio. Etiam vel congue mauris.\r\n\r\nSed vel placerat odio, nec convallis ex. Vestibulum venenatis erat vel tellus accumsan, eget ultricies lorem malesuada. Sed sollicitudin massa vitae euismod cursus. Etiam accumsan, nisl nec fringilla tincidunt, ante lectus ornare lectus, eget luctus quam nisi maximus felis. Nulla sollicitudin sapien a ornare tincidunt. Ut hendrerit sed mi eget sollicitudin. Sed eget ante id lacus tristique porttitor. Proin sed eleifend sapien. Proin fringilla ultrices risus sit amet porta.",
            DueDate = new DateTime(new DateOnly(2026, 6, 30), new TimeOnly(23, 59)),
            CreationDate = new DateTime(new DateOnly(2024, 6, 1), new TimeOnly(12, 0)),
            Progress = TaskProgress.InProgress
        },
        //ChatGPT generated filler tasks
        new Task {
            Name = "Buy groceries",
            Description = "Pick up a few items from the store. This task exists purely as filler content to test short descriptions and basic list rendering.",
            DueDate = new DateTime(new DateOnly(2026, 1, 10), new TimeOnly(17, 0)),
            CreationDate = new DateTime(new DateOnly(2024, 6, 5), new TimeOnly(10, 15)),
            Progress = TaskProgress.Todo
        },
        new Task {
            Name = "Schedule appointment",
            Description = "Call or book an appointment sometime this week. The exact details are intentionally vague. This description is medium-length to test text wrapping and spacing in the UI.",
            DueDate = new DateTime(new DateOnly(2026, 2, 3), new TimeOnly(12, 0)),
            CreationDate = new DateTime(new DateOnly(2024, 6, 6), new TimeOnly(9, 0)),
            Progress = TaskProgress.InProgress
        },
        new Task {
            Name = "Read documentation",
            Description = "Read through documentation or reference material.\r\n\r\nThis task includes line breaks, paragraphs, and slightly longer text so you can verify that multi-line descriptions, scrolling behavior, and padding behave correctly across different screen sizes.",
            DueDate = new DateTime(new DateOnly(2026, 4, 15), new TimeOnly(23, 59)),
            CreationDate = new DateTime(new DateOnly(2024, 6, 7), new TimeOnly(13, 30)),
            Progress = TaskProgress.Todo
        },
        new Task {
            Name = "Clean workspace",
            Description = "Organize desk, clean up cables, and remove unnecessary clutter. This task is intentionally boring and generic so that the UI design remains the focus rather than the content itself.",
            DueDate = new DateTime(new DateOnly(2025, 11, 30), new TimeOnly(18, 0)),
            CreationDate = new DateTime(new DateOnly(2024, 6, 8), new TimeOnly(15, 45)),
            Progress = TaskProgress.Done
        },
        new Task {
            Name = "General reminder",
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna mauris, condimentum sit amet sem quis, scelerisque ornare sapien. Integer maximus congue nisl, sed egestas lorem hendrerit at.\r\n\r\nDonec vehicula ut orci id varius. Sed molestie ut nisi sit amet dictum. Quisque commodo maximus hendrerit. Praesent aliquet tortor molestie, suscipit mi sit amet, sollicitudin eros.",
            DueDate = new DateTime(new DateOnly(2026, 8, 20), new TimeOnly(23, 59)),
            CreationDate = new DateTime(new DateOnly(2024, 6, 9), new TimeOnly(8, 0)),
            Progress = TaskProgress.InProgress
        }
    };
        
    #endregion

    #region Date and Time Stuff
    // helpers for converting dates to strings
    // small shortens the month name, large uses the full month name 
    public static string GetDateSmall(DateTime dateTime) => GetDateSmall(DateOnly.FromDateTime(dateTime));
    public static string GetDateSmall(DateOnly date)
    {
        return $"{MonthNumToMonthNameSmall(date)} {date.Day}";
    }
    public static string GetDateOrNoneSmall(DateTime? dateTime) => GetDateOrNoneSmall(dateTime.HasValue ? DateOnly.FromDateTime(dateTime.Value) : null);
    public static string GetDateOrNoneSmall(DateOnly? date)
    {
        return date.HasValue ? GetDateSmall(date.Value) : "No Due Date";
    }

    public static string GetDateLarge(DateTime dateTime) => GetDateLarge(DateOnly.FromDateTime(dateTime));
    public static string GetDateLarge(DateOnly date)
    {
        return $"{MonthNumToMonthNameLarge(date)} {date.Day}, {date.Year}";
    }
    public static string GetDateOrNoneLarge(DateTime? dateTime) => GetDateOrNoneLarge(dateTime.HasValue ? DateOnly.FromDateTime(dateTime.Value) : null);
    public static string GetDateOrNoneLarge(DateOnly? date)
    {
        return date.HasValue ? GetDateLarge(date.Value) : "No Due Date";
    }

    public static string MonthNumToMonthNameSmall(DateTime dateTime) => MonthNumToMonthNameSmall(DateOnly.FromDateTime(dateTime));
    public static string MonthNumToMonthNameSmall(DateOnly date)
    {
        switch (date.Month)
        {
            case 1: return "Jan";
            case 2: return "Feb";
            case 3: return "Mar";
            case 4: return "Apr";
            case 5: return "May";
            case 6: return "Jun";
            case 7: return "Jul";
            case 8: return "Aug";
            case 9: return "Sep";
            case 10: return "Oct";
            case 11: return "Nov";
            case 12: return "Dec";

            // this should be impossible, but its here just in case
            default: return "ERR";
        }
    }

    public static string MonthNumToMonthNameLarge(DateOnly date)
    {
        switch (date.Month)
        {
            case 1: return "January";
            case 2: return "February";
            case 3: return "March";
            case 4: return "April";
            case 5: return "May";
            case 6: return "June";
            case 7: return "July";
            case 8: return "August";
            case 9: return "September";
            case 10: return "October";
            case 11: return "November";
            case 12: return "December";

            // this should be impossible, but its here just in case
            default: return "ERR";
        }
    }
    #endregion
}
