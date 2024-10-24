using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static readonly List<TodoItem> _todos = new();
        private static int _nextId = 1;

        // GET: api/todo
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Simulating an async operation
            await Task.Yield(); // Task.Yield ensures method remains async
            return Ok(_todos);
        }

        // GET: api/todo/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Yield(); // Simulate async

            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItem todoItem)
        {
            await Task.Yield(); // Simulate async

            if (todoItem == null)
            {
                return BadRequest("Todo item cannot be null.");
            }

            todoItem.Id = _nextId++;
            _todos.Add(todoItem);

            return CreatedAtAction(nameof(GetById), new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoItem todoItem)
        {
            await Task.Yield(); // Simulate async

            if (todoItem == null)
            {
                return BadRequest("Todo item cannot be null.");
            }

            var existingTodo = _todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo == null)
            {
                return NotFound();
            }

            existingTodo.Title = todoItem.Title;
            existingTodo.IsCompleted = todoItem.IsCompleted;

            return NoContent();
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Yield(); // Simulate async

            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _todos.Remove(todo);
            return NoContent();
        }
    }

    // Todo item model
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
