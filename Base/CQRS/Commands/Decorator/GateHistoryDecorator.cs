using Base.CQRS.Commands.Handler;

namespace Base.CQRS.Commands.Decorator
{
    internal class GateHistoryDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _inner;

        public GateHistoryDecorator(ICommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public void Handle(TCommand command)
        {
            _inner.Handle(command);
        }
    }
}
