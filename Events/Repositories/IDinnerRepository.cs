namespace Events.Repositories
{
    using Events.Domain;

    public interface IDinnerRepository
    {
        void Save(Dinner dinner);
    }
}
