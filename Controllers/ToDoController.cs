using ApiProject.Data;
using ApiProject.Models;
using ApiProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ToDoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var todos = await context
                .Todos
                .AsNoTracking()
                .ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var todo = await context
                .Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            //condicional se não encontrar o item
            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost("todos")]
        public async Task<IActionResult> PostAsync(
            [FromServices]  AppDbContext context, 
            [FromBody]      CreateToDoViewModel model) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = new Todo
            {
                DateTime = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created($"v1/todos/{todo.Id}", todo);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateToDoViewModel model,
            [FromRoute] int id) 
        { 
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

            if(todo == null)
            {
                NotFound();
            }

            try
            {
                todo.Title = model.Title;

                context.Todos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("todos/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
            {
                var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

                if (todo == null)
                    return NotFound(new { message = "item não encontrado" });

                context.Todos.Remove(todo);
                await context.SaveChangesAsync();

                return Ok(new { message = "item removido com sucesso" });
            }
        }
}