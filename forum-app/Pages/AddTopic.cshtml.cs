using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using forum_app.Infrastructure;
using forum_app.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace forum_app.Pages {
    [Authorize]
    public class AddTopicModel : PageModel {
        private readonly TopicContext _context;

        public AddTopicModel(TopicContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public Topic TopicItem { get; set; }

        public async Task<IActionResult> OnPostAsync() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            DateTime today = DateTime.Now;

            TopicItem.AuthorId = userId;
            TopicItem.AuthorName = userName;
            TopicItem.Date = today;

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Topic.Add(TopicItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
