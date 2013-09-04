﻿namespace Accounts.Domain
{
    using System;

    public class DinnerApplicant
    {
        public virtual User User { get; set; }

        public virtual Dinner Dinner { get; set; }

        public bool Accepted { get; set; }

        public bool Rejected { get; set; }

        private DinnerApplicant()
        {
        }

        public DinnerApplicant(User user, Dinner dinner)
        {
            User = user;
            Dinner = dinner;
            Accepted = false;
            Rejected = false;
        }

        public override bool Equals(object obj)
        {
            if (obj is DinnerApplicant)
            {
                var dinnerApplicant = (DinnerApplicant) obj;
                return dinnerApplicant.User.Id == User.Id && dinnerApplicant.Dinner.Id == Dinner.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
