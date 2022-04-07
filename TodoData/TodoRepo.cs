using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoData.Models;

namespace TodoData
{
    public class TodoRepo : ITodoRepo
    {
        private readonly TodoContext _context;
        public TodoRepo(TodoContext context)
        {
            _context = context;
        }
        public async Task<TodoItem> Add(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<int> Delete(TodoItem item)
        {
            _context.TodoItems.Remove(item);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetById(long id)
        {
            return await _context.TodoItems.Where(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> Update(TodoItem newItem)
        {
            var oldItem = await _context.TodoItems.FindAsync(newItem.Id);
            oldItem.IsComplete = newItem.IsComplete;
            oldItem.Name = newItem.Name;
            return await _context.SaveChangesAsync();
        }
    }
}
