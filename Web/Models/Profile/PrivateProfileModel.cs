namespace Web.Models.Profile
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Enums;
    using DataAnnotationsExtensions;
    using Shared;

    public class PrivateProfileModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Suburb")]
        public string Suburb { get; set; }

        [Required]
        [Display(Name = "City / Province")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }
        
        public string ProfileImage { get; set; }

        public IList<ImageModel> ProfileImageUrls { get; set; }

        public string Description { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public Orientation Orientation { get; set; }

        public string Age { get; set; }

        public bool Friendship { get; set; }

        public bool Romance { get; set; }
    }
}