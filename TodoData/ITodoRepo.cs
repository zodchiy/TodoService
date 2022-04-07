using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoData.Models;

namespace TodoData
{
    public interface ITodoRepo
    {
        Task<IEnumerable<TodoItem>> GetAll();
        Task<TodoItem> GetById(long id);
        Task<TodoItem> Add(TodoItem item);
        Task<int> Update(TodoItem item);
        Task<int> Delete(TodoItem item);
    }
}
