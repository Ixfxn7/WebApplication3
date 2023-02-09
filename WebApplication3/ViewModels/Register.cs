using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {

        [Required, MinLength(10), MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be valid email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$", ErrorMessage = "Password must have at least 12 characters, including lower-case, upper-case, numbers, and special characters")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        [Required, MinLength(2), MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Your FullName should not have any special characters")]
        public string FullName { get; set; }

        [Required, MinLength(2), MaxLength(500)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter a valid credit card number")]
        public string CreditCard { get; set; }

        [Required, MaxLength(1)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string DelAddress { get; set; }

        [Required]
        public string Token { get; set; }


    }
}
