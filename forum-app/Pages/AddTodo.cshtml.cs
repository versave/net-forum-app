using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using forum_app.Infrastructure;
using forum_app.Model;

namespace forum_app.Controllers {
    [Authorize]
    public class AddTodoModel : PageModel {
        private readonly TodoContext _context;

        public AddTodoModel(TodoContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public TodoList TodoList { get; set; }

        public async Task<IActionResult> OnPostAsync() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            TodoList.UserId = userId;
            TodoList.UserEmail = userEmail;

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.todoList.Add(TodoList);
            await _context.SaveChangesAsync();

            TempData["Success"] = "The item has been added!";

            return RedirectToPage("Index");
        }
    }
}
