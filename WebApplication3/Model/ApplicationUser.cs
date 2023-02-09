using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace WebApplication3.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string CreditCard { get; set; }

        [Required, MaxLength(1)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string DelAddress { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public string AboutMe { get; set; }
    }
}
