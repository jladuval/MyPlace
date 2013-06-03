using Moq;

using Security.Domain;
using Security.Interfaces.Commands;
using Xunit;

namespace Security.Tests.Application.HandlerTests
{
    public class SignupUserCommandHandlerTest
    {
        //readonly SignUpUserCommandHandler _handler = new SignUpUserCommandHandler();

        public SignupUserCommandHandlerTest()
        {
            //_handler.UserRepository = new Mock<IUserRepository>().SetupAllProperties().Object;
        }

        [Fact]
        public void SignupCorrect()
        {
            //Assert.DoesNotThrow(() => _handler.Handle(new SignUpUserCommand("a@a.c", "password")));
        }
    }
}
