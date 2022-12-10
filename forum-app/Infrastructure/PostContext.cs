using forum_app.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forum_app.Infrastructure {
    public class PostContext : DbContext {
        public PostContext(DbContextOptions<PostContext> options) : base(options) { }

        public DbSet<Post> Post { get; set; }
    }
}
