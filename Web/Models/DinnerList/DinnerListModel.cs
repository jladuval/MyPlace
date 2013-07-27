namespace Web.Models.DinnerList
{
    using System.Collections.Generic;

    public class DinnerListModel
    {
        public int TotalResults { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public IList<DinnerListItemModel> Dinners { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }
    }
}