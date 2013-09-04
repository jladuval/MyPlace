namespace Accounts.Domain
{
    using System;

    using Base.DDD.Domain;

    public class DinnerApplicant : Entity
    {
        public virtual User User { get; set; }

        public virtual Dinner Dinner { get; set; }

        public virtual User Partner { get; set; }

        public Guid? VerificationCode { get; set; }

        public bool Accepted { get; set; }

        public bool Rejected { get; set; }

        public DinnerApplicant()
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
