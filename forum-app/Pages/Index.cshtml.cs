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
        private readonly TopicContext _context;

        public IndexModel(TopicContext context) {
            _context = context;
        }

        public IList<Topic> TopicList { get; set; }
        public string userId;

        public async Task OnGetAsync() {
            this.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TopicList = await _context.Topic.ToListAsync();
        }
    }
}
