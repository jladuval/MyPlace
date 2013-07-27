namespace Accounts.Interfaces.Presentation.Dinner
{
    public class DinnerListItemDto
    {
        public string ProfileImageUrl { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public bool DryDinner { get; set; }

        public string Date { get; set; }

        public int Distance { get; set; }
    }
}
