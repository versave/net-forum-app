using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using forum_app.Infrastructure;
using forum_app.Model;

namespace forum_app.Controllers {
    public class IndexModel : PageModel {
        private readonly PostContext _context;

        public IndexModel(PostContext context) {
            _context = context;
        }

        public IList<Post> PostList { get;set; }

        public async Task OnGetAsync() {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // TodoList = await _context.todoList.Where(todo => todo.UserId == userId).ToListAsync();
            PostList = await _context.Post.ToListAsync();
        }
    }
}
