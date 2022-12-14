using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using forum_app.Infrastructure;
using forum_app.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace forum_app.Pages {
    public class DeletePostModel : PageModel {
        private readonly PostContext _context;

        public DeletePostModel(PostContext context) {
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

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            PostItem = await _context.Post.FindAsync(id);

            if (PostItem != null) {
                _context.Post.Remove(PostItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
