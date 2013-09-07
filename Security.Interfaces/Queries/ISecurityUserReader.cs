﻿using Security.Interfaces.Presentation;

namespace Security.Interfaces.Queries
{
    public interface ISecurityUserReader
    {
        CheckUserCredentialsDto CheckUserCredentials(CheckUserCredentialsQuery query);

        bool UserExists(string email);
        CheckUserCredentialsDto GetUserFromToken(string token);
    }
}
