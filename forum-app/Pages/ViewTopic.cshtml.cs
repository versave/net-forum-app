using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forum_app.Infrastructure;
using forum_app.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace forum_app.Pages {
    public class ViewTopicModel : PageModel {
        private readonly PostContext _context;

        public ViewTopicModel(PostContext context) {
            _context = context;
        }

        public IList<Post> PostList { get; set; }
        public int? topicId;

        public async Task OnGetAsync(int? id) {
            PostList = await _context.Post.Where(post => post.TopicId == id).ToListAsync();
            this.topicId = id;
        }
    }
}
