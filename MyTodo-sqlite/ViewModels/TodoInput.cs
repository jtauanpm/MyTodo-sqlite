using System.ComponentModel.DataAnnotations;

namespace MyTodo_sqlite.ViewModels;

public class TodoInput
{
    public TodoInput(string title)
    {
        Title = title;
    }

    [Required]
    public string Title { get; set; }
}