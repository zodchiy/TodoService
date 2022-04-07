using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBI.Models;
using TodoData;
using TodoData.Models;

namespace TodoBI
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepo _repo;
        public TodoService(ITodoRepo repo)
        {
            _repo = repo;
        }

        public async Task<TodoItemDTO> Add(TodoItemDTO item)
        {
            var newitem = DTOtoItem(item);
            var res = await _repo.Add(newitem);
            return ItemToDTO(res);
        }

        public async Task<int> Delete(TodoItemDTO item)
        {
            var _item = DTOtoItem(item);
            return await _repo.Delete(_item);
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAll()
        {
            var all = await _repo.GetAll();
            return all.Select(x => ItemToDTO(x)).ToList();
        }

        public async Task<TodoItemDTO> GetById(long id)
        {
            var res = await _repo.GetById(id);
            return ItemToDTO(res);
        }

        public async Task<int> Update(TodoItemDTO item)
        {
            var _item = DTOtoItem(item);
            return await _repo.Delete(_item);
        }

        #region converters
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
           new TodoItemDTO
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
               IsComplete = todoItem.IsComplete
           };
        private static TodoItem DTOtoItem(TodoItemDTO dto) =>
          new TodoItem
          {
              Id = dto.Id,
              Name = dto.Name,
              IsComplete = dto.IsComplete
          };
        #endregion
    }
}
