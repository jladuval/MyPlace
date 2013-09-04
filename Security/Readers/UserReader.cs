namespace Security.Readers
{
    using System.Linq;

    using Base.CQRS.Query.Attributes;

    using NHibernate;
    using NHibernate.Linq;

    using Domain;
    using Interfaces.Presentation;
    using Interfaces.Queries;
    using Services;

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
                               Roles = user.Roles.Select(x => x.Name).ToList(),
                               HasDetails = user.FirstName != null
                           }
                       : null;
        }
        
        public bool UserExists(string email) 
        {
            return _session.Query<User>().FirstOrDefault(x => x.Email == email) != null;
        }
    }
}
