namespace Base.CQRS.Commands
{
    public interface IGate
    {
        void Dispatch<T>(T command);
    }
}
