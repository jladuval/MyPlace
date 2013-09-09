namespace Accounts.Domain
{
    using Base.DDD.Domain;

    public class Comment : Entity
    {
        public string Text { get; set; }

        public virtual User User { get; set; }
    }
}
