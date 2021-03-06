﻿namespace Web.Models.DinnerList
{
    using System;

    public class DinnerListItemModel
    {
        public Guid Id { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }

        public bool DryDinner { get; set; }

        public string Date { get; set; }

        public int Distance { get; set; }
    }
}