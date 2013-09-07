namespace Security.Interfaces.Commands
{
    using System;

    public class ActivateUserByIdCommand
    {
        public Guid Id { get; set; }

        public ActivateUserByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
