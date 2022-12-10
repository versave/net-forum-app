using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forum_app.Model;

namespace forum_app.Infrastructure {
    public class TodoContext: DbContext {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<TodoList> todoList { get; set; }
    }
}
