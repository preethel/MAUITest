using Microsoft.AspNetCore.Components;

namespace MyRazorClassLibrary;

public partial class TodoList
{
    private List<TodoItem> Todos = new();

    private string NewTodoTitle { get; set; } = string.Empty;

    private TodoItem? EditingTodo { get; set; }

    private string? AlertMessage { get; set; }

    private string AlertClass { get; set; } = "alert-success";

    [Inject]
    private ITodoService TodoService { get; set; } = null!;
    protected override async Task OnInitializedAsync()
    {
        await LoadTodos();
    }

    private async Task LoadTodos()
    {
        try
        {
            Todos = await TodoService.GetAll();
            StateHasChanged();
        }
        catch (Exception)
        {
            SetAlertMessage("Error loading todos.", "alert-danger");
        }
    }

    private async Task HandleSubmit()
    {
        var newTodo = new TodoItem { Title = NewTodoTitle, IsCompleted = false };

        try
        {
            var createdTodo = await TodoService.Create(newTodo);
            Todos.Add(createdTodo);
            NewTodoTitle = string.Empty;
            SetAlertMessage("Todo added successfully.", "alert-success");
        }
        catch (Exception)
        {
            SetAlertMessage("Error adding todo.", "alert-danger");
        }
    }

    private void EditTodo(TodoItem todo)
    {
        EditingTodo = new TodoItem
        {
            Id = todo.Id,
            Title = todo.Title,
            IsCompleted = todo.IsCompleted
        };
    }

    private void CancelEdit()
    {
        EditingTodo = null;
    }

    private async Task UpdateTodo(int id)
    {
        if (EditingTodo == null) return;

        try
        {
            await TodoService.Update(id, EditingTodo);
            var index = Todos.FindIndex(t => t.Id == id);
            if (index >= 0)
            {
                Todos[index] = EditingTodo;
            }
            EditingTodo = null;
            SetAlertMessage("Todo updated successfully.", "alert-success");
        }
        catch (Exception)
        {
            SetAlertMessage("Error updating todo.", "alert-danger");
        }
    }

    private async Task DeleteTodo(int id)
    {
        try
        {
            await TodoService.Delete(id);
            Todos.RemoveAll(t => t.Id == id);
            SetAlertMessage("Todo deleted successfully.", "alert-success");
        }
        catch (Exception)
        {
            SetAlertMessage("Error deleting todo.", "alert-danger");
        }
    }

    private void SetAlertMessage(string message, string alertClass)
    {
        AlertMessage = message;
        AlertClass = alertClass;
    }
}