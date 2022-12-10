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
    
    public class AddPostModel : PageModel {
        private readonly PostContext _context;

        public AddPostModel(PostContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public Post PostItem { get; set; }

        public async Task<IActionResult> OnPostAsync() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            DateTime today = DateTime.Now;

            PostItem.AuthorId = userId;
            PostItem.AuthorName = userName;
            PostItem.Date = today;

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Post.Add(PostItem);
            await _context.SaveChangesAsync();

            TempData["Success"] = "The item has been added!";

            return RedirectToPage("Index");
        }
    }
}
