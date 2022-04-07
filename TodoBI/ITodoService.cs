using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoBI.Models;
using TodoData.Models;

namespace TodoBI
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDTO>> GetAll();
        Task<TodoItemDTO> GetById(long id);
        Task<TodoItemDTO> Add(TodoItemDTO item);
        Task<int> Update(TodoItemDTO item);
        Task<int> Delete(TodoItemDTO item);
    }
}
