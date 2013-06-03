namespace Infrastructure.NHibernate.Repositories
{
    using System.Data;

    using global::NHibernate;
    using global::NHibernate.SqlCommand;
    using global::NHibernate.SqlTypes;

    public static class DbExtensions
    {
        public static IDbCommand GenerateCommand(this ISession session, CommandType commandType, string sql, params SqlType[] parameterTypes)
        {
            var connection = session.Connection;
            var implementor = session.GetSessionImplementation();
            var driver = implementor.Factory.ConnectionProvider.Driver;
            var command = driver.GenerateCommand(CommandType.Text, new SqlString(sql), parameterTypes);
            command.Connection = connection;
            return command;
        }

        public static void SetParameter(this IDbCommand command, int index, string name, object value)
        {
            var param = (IDataParameter)command.Parameters[index];
            param.ParameterName = name;
            param.Value = value;
        }
    }
}