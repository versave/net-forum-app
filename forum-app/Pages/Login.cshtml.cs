using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using forum_app.ViewModel;
using System.Security.Claims;

namespace forum_app.Pages {
    public class LoginModel : PageModel {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public Login Model { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager) {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
            if (ModelState.IsValid) {
                var identityResult = await signInManager.PasswordSignInAsync(Model.UserName, Model.Password, Model.RememberMe, false);
                
                if (identityResult.Succeeded) {
                
                    return returnUrl == null || returnUrl == "/" ? RedirectToPage("Index") : RedirectToPage(returnUrl);
                }
            }

            ModelState.AddModelError("", "Incorrect credentials");
            return Page();
        }
    }
}
