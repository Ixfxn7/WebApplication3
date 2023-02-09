using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
    [Authorize]
    public class userInfomationModel : PageModel
    {

        public ApplicationUser currentUser { get; set; }

        private readonly IConfiguration _configuration;

        private readonly AuthDbContext _context;

        private readonly IWebHostEnvironment _environment;

        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        public userInfomationModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AuthDbContext context, IWebHostEnvironment environment, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            _environment = environment;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string email = HttpContext.Session.GetString("Email");

            System.Security.Claims.ClaimsPrincipal currentUser1 = this.User;
            var user = await userManager.GetUserAsync(User);

            WebApplication3.Model.ApplicationUser? current = await userManager.FindByNameAsync(user.UserName);

            if (current == null)
            {
                TempData["FlashMessage.Type"] = "error";
                TempData["FlashMessage.Text"] = "User was not found in database";
                return RedirectToPage("../index");
            }
            // setting up data protector 
            var dataProtect = DataProtectionProvider.Create("encrypt");
            var protect = dataProtect.CreateProtector("creditcard");


            // decrypting credit card number
            currentUser = current;
            currentUser.CreditCard = protect.Unprotect(user.CreditCard);
            return Page();
        }
    }
}
