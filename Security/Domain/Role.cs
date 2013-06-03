namespace Security.Domain
{
    using System.Collections.Generic;

    using Base.DDD.Domain;

    public class Role : Entity
    {
        public string Name { get; set; }

        public IList<User> Users { get; set; }
    }
}
