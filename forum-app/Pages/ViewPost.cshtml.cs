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
    public class ViewPostModel : PageModel {
        private readonly PostContext _postContext;
        private readonly CommentContext _commentContext;

        public ViewPostModel(PostContext context, CommentContext commentContext) {
            _postContext = context;
            _commentContext = commentContext;
        }

        [BindProperty]
        public Post PostItem { get; set; }

        [BindProperty]
        public Comment AddComment { get; set; }

        public IList<Comment> CommentList { get; set; }
        public bool isUserPost = false;

        public async Task<IActionResult> OnGetAsync(int? id) {
            return await this.SetPostInfo(id);
        }

        public async Task<IActionResult>? OnPostAsync(int? id) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            DateTime today = DateTime.Now;

            if (AddComment.Content == null) {
                return await this.SetPostInfo(id);
            }

            AddComment.AuthorId = userId;
            AddComment.AuthorName = userName;
            AddComment.Date = today;
            AddComment.PostId = PostItem.Id;

            _commentContext.Comment.Add(AddComment);
            await _commentContext.SaveChangesAsync();

            return await this.SetPostInfo(id);
        }

        private async Task<IActionResult> SetPostInfo(int? id) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null) {
                return NotFound();
            }

            PostItem = await _postContext.Post.FirstOrDefaultAsync(m => m.Id == id);
            CommentList = await _commentContext.Comment.Where(comment => comment.PostId == PostItem.Id).ToListAsync();
            this.isUserPost = PostItem.AuthorId == userId || User.IsInRole("Admin");

            if (PostItem == null) {
                return NotFound();
            }

            return Page();
            
        }
    }
}
