namespace Security.Interfaces.Queries
{
    public class CheckUserCredentialsQuery
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
