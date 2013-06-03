namespace Web.Models.Membership
{
    using System.ComponentModel.DataAnnotations;

    public class SignupModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}