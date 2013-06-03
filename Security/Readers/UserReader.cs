namespace Security.Readers
{
    using System;
    using System.Linq;

    using Base.CQRS.Query.Attributes;

    using NHibernate;
    using NHibernate.Linq;

    using Security.Domain;
    using Security.Interfaces.Application;
    using Security.Interfaces.Presentation;
    using Security.Interfaces.Queries;
    using Security.Services;

    [Reader]
    public class UserReader : ISecurityUserReader
    {
        private readonly ISession _session;

        private readonly ICryptoService _cryptoService;

        public UserReader(ISession session, ICryptoService cryptoService)
        {
            _session = session;
            _cryptoService = cryptoService;
        }

        public CheckUserCredentialsDto CheckUserCredentials(CheckUserCredentialsQuery query)
        {
            var user = _session.Query<User>().FirstOrDefault(x => x.Email == query.Email);
            return user != null
                   && _cryptoService.CheckPassword(user.Password, query.Password, user.Salt)
                       ? new CheckUserCredentialsDto
                           {
                               UserId = user.Id,
                               Roles = user.Roles.Select(x => (UserRoles)Enum.Parse(typeof(UserRoles), x.Name, true)).ToList()
                           }
                       : null;
        }
        
        public bool UserExists(UserExistsQuery query) 
        {
            return _session.Query<User>().FirstOrDefault(x => x.Email == query.Email) != null;
        }
    }
}
