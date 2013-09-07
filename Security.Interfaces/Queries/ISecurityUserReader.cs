using Security.Interfaces.Presentation;

namespace Security.Interfaces.Queries
{
    using System;

    public interface ISecurityUserReader
    {
        CheckUserCredentialsDto CheckUserCredentials(CheckUserCredentialsQuery query);

        bool UserExists(string email);
        CheckUserCredentialsDto GetUserFromToken(string token);
        CheckUserCredentialsDto GetUserById(Guid userId);
    }
}
