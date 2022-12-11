using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using forum_app.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace forum_app.Pages {
    [Authorize(Roles = "Admin")]
    public class AdminActionsModel : PageModel {
        private readonly RoleManager<IdentityRole> roleManager;
        public readonly UserManager<IdentityUser> userManager;
        private readonly AuthDbContext _usersContext;

        public AdminActionsModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, AuthDbContext usersContext) {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this._usersContext = usersContext;
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
