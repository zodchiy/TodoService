using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoBI;
using TodoBI.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _service;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoService service, ILogger<TodoItemsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _service.GetById(id);

            if (todoItem == null)
            {
                _logger.LogError($"GetTodoItem: Не найдена запись с id={id}.");
                return NotFound();
            }

            return todoItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                _logger.LogError($"UpdateTodoItem: TodoItemDTO.id={todoItemDTO.Id} не равен id={id}.");
                return BadRequest();
            }
            var todoItem = await _service.GetById(id);
            if (todoItem == null)
            {
                _logger.LogError($"UpdateTodoItem: Не найдена запись с id={id}.");
                return NotFound();
            }
            try
            {
                await _service.Update(todoItemDTO);
            }
            catch (DbUpdateConcurrencyException) when (_service.GetById(id) == null)
            {
                _logger.LogError($"UpdateTodoItem: DbUpdateConcurrencyException с id={id}.");
                return NotFound();
            }
            catch (Exception ex)
            { 
                _logger.LogError($"UpdateTodoItem: Ошибка: {ex.Message}");
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            TodoItemDTO newItem = null;
            try
            {
                newItem = await _service.Add(todoItemDTO);
            }
            catch(Exception ex)
            {
                _logger.LogError($"CreateTodoItem: Ошибка: {ex.Message}");
                return BadRequest();
            }

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = newItem.Id },
                newItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _service.GetById(id);

            if (todoItem == null)
            {
                _logger.LogError($"DeleteTodoItem: Не найдена запись с id={id}.");
                return NotFound();
            }
            try
            {
                await _service.Delete(todoItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteTodoItem: Ошибка: {ex.Message}");
                return BadRequest();
            }
            return NoContent();
        }    
    }
}
