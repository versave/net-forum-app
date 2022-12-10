using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using forum_app.Infrastructure;
using forum_app.Model;

namespace forum_app.Controllers {
    [Authorize]
    public class DeleteTodoModel : PageModel {
        private readonly TodoContext _context;

        public DeleteTodoModel(TodoContext context) {
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

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            TodoList = await _context.todoList.FindAsync(id);

            if (TodoList != null) {
                _context.todoList.Remove(TodoList);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
