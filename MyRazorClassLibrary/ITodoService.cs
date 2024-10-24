using Refit;

namespace MyRazorClassLibrary
{
    public interface ITodoService
    {
        [Get("/api/todo")]
        Task<List<TodoItem>> GetAll();

        [Get("/api/todo/{id}")]
        Task<TodoItem> GetById(int id);

        [Post("/api/todo")]
        Task<TodoItem> Create([Body] TodoItem todoItem);

        [Put("/api/todo/{id}")]
        Task Update(int id, [Body] TodoItem todoItem);

        [Delete("/api/todo/{id}")]
        Task Delete(int id);
    }

    public class TodoItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; }
    }
}