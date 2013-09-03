﻿namespace Web.Models.Membership
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
	{
        [EmailAddress]
        [Required]
		public string Email { get; set; }

        [Required]
	    public string Password { get; set; }

	    public bool RememberMe { get; set; }
	}
}
