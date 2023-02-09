using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        //private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) // RoleManager<IdentityRole> roleManager
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            //this.roleManager = roleManager;
        }



        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var user = new ApplicationUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FullName = RModel.FullName,
                    Gender = RModel.Gender,
                    MobileNo = RModel.MobileNo,
                    DelAddress = RModel.DelAddress,
                    CreditCard = protector.Protect(RModel.CreditCard)
                };

                //Create the Admin role if NOT exist
                //IdentityRole role = await roleManager.FindByIdAsync("Admin");
                //if (role == null)
                //{
                // IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Admin"));
                // if (!result2.Succeeded)
                //{
                //   ModelState.AddModelError("", "Create role admin failed");
                // }
                //}

                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    //Add users to Admin Role
                    //result = await userManager.AddToRoleAsync(user, "Admin");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }







    }
}
