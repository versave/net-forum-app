using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace forum_app.Pages {
    [Authorize(Roles = "Admin")]
    public class AdminActionsModel : PageModel {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminActionsModel(RoleManager<IdentityRole> roleManager) {
            this.roleManager = roleManager;
        }

        [BindProperty]
        public ProjectRole role { get; set; }

        public async Task<IActionResult> OnPostAsync() {
            var roleExists = await roleManager.RoleExistsAsync(role.RoleName);

            if (!roleExists) {
                var result = await roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }

            return Page();
        }
    }
}
