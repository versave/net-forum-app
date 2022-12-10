using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using forum_app.ViewModel;

namespace forum_app.Pages {
    public class RegisterModel : PageModel {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public Register Model { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync() {
            if (ModelState.IsValid) {
                var user = new IdentityUser() {
                    UserName = Model.Email,
                    Email = Model.Email,
                };

                var result = await userManager.CreateAsync(user, Model.Password);

                if (result.Succeeded) {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }

                foreach (var err in result.Errors) {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return Page();
        }
    }
}
