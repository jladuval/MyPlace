namespace Web.Models.Membership
{
    using System.ComponentModel.DataAnnotations;

    public class SignupModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string HoneyPot { get; set; }
    }
}