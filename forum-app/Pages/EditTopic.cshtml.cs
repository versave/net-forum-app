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
using Microsoft.EntityFrameworkCore;

namespace forum_app.Pages {
    [Authorize]
    public class EditTopicModel : PageModel {
        private readonly TopicContext _context;

        public EditTopicModel(TopicContext context) {
            _context = context;
        }

        [BindProperty]
        public Topic TopicItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            TopicItem = await _context.Topic.FirstOrDefaultAsync(m => m.Id == id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isNotUserEditable = TopicItem.AuthorId != userId && !User.IsInRole("Admin");

            if (TopicItem == null || isNotUserEditable) {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(TopicItem).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}
