using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using forum_app.Infrastructure;
using forum_app.Model;

namespace forum_app.Controllers {
    [Authorize]
    public class EditTodoModel : PageModel {
        private readonly TodoContext _context;

        public EditTodoModel(TodoContext context) {
            _context = context;
        }

        [BindProperty]
        public TodoList TodoList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            TodoList = await _context.todoList.FirstOrDefaultAsync(m => m.Id == id);

            if (TodoList == null) {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            TodoList.UserId = userId;
            TodoList.UserEmail = userEmail;

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(TodoList).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!TodoListExists(TodoList.Id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            TempData["Success"] = "The item has been updated!";

            return RedirectToPage("./Index");
        }

        private bool TodoListExists(int id) {
            return _context.todoList.Any(e => e.Id == id);
        }
    }
}
