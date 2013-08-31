namespace Web.Models.Dinner
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDinnerModel
    {
        [Required]
        [Display(Name = "Starter")]
        public string Starter { get; set; }

        [Required]
        [Display(Name = "Main")]
        public string Main { get; set; }

        [Required]
        [Display(Name = "Dessert")]
        public string Dessert { get; set; }

        [Display(Name = "Dry Night? (Alcohol free)")]
        public bool DryDinner { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(160, ErrorMessage = "Please enter less than 160 characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date and Time")]
        public string Date { get; set; }

        public string CurrentLocation { get; set; }
    }
}