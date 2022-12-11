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
    public class DeleteTopicModel : PageModel {
        private readonly TopicContext _topicContext;
        private readonly PostContext _postContext;

        public DeleteTopicModel(TopicContext topicContext, PostContext postContext) {
            _topicContext = topicContext;
            _postContext = postContext;
        }

        [BindProperty]
        public Topic TopicItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            TopicItem = await _topicContext.Topic.FirstOrDefaultAsync(m => m.Id == id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isNotUserEditable = TopicItem.AuthorId != userId && !User.IsInRole("Admin");


            if (TopicItem == null || isNotUserEditable) {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            TopicItem = await _topicContext.Topic.FindAsync(id);

            if (TopicItem != null) {
                _postContext.Post.RemoveRange(_postContext.Post.Where(post => post.TopicId == TopicItem.Id));
                await _postContext.SaveChangesAsync();


                _topicContext.Topic.Remove(TopicItem);
                await _topicContext.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
