using forum_app.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forum_app.Infrastructure {
    public class TopicContext: DbContext {
        public TopicContext(DbContextOptions<TopicContext> options) : base(options) { }

        public DbSet<Topic> Topic { get; set; }
    }
}
