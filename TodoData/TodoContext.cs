using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoData.Models;

namespace TodoData
{
    public class TodoContext: DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options): base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
