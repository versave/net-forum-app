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
    public class EditPostModel : PageModel {
        private readonly PostContext _context;

        public EditPostModel(PostContext context) {
            _context = context;
        }

        [BindProperty]
        public Post PostItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            PostItem = await _context.Post.FirstOrDefaultAsync(m => m.Id == id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isNotUserEditable = PostItem.AuthorId != userId && !User.IsInRole("Admin");

            if (PostItem == null || isNotUserEditable) {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(PostItem).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                return NotFound();
            }

            return RedirectToPage("ViewPost", new { id = PostItem.Id });
        }
    }
}
