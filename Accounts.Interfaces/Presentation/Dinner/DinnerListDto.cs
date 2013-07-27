namespace Accounts.Interfaces.Presentation.Dinner
{
    using System.Collections.Generic;

    public class DinnerListDto
    {
        public int TotalResults { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public IList<DinnerListItemDto> Dinners { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }
    }
}
