using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTodo_sqlite.Data;
using MyTodo_sqlite.Models;
using MyTodo_sqlite.ViewModels;

namespace MyTodo_sqlite.Controllers;

[ApiController]
[Route("v1/todos")]
public class TodoController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromServices] AppDbContext context)
    {
        var todos = await context
            .Todos
            .AsNoTracking()
            .ToListAsync();

        return Ok(todos);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] AppDbContext context, [FromRoute] int id)
    {
        var todo = await context
            .Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] TodoInput todoInput)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var todo = new Todo
        {
            Date = DateTime.Now,
            Done = false,
            Title = todoInput.Title
        };

        try
        {
            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();
            return Created($"v1/todos/{todo.Id}", todo);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromServices] AppDbContext context,
        [FromBody] TodoInput todoInput, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var todo = await context.Todos.FirstOrDefaultAsync(t => t.Id == id);

        if (todo == null)
            return NotFound();

        try
        {
            todo.Title = todoInput.Title;
            context.Todos.Update(todo);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {
        var todo = await context.Todos.FirstOrDefaultAsync(t => t.Id == id);

        if (todo == null)
            return BadRequest();
        
        try
        {
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}