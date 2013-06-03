namespace Infrastructure.NHibernate
{
    using System.Diagnostics;

    using global::NHibernate;

    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override global::NHibernate.SqlCommand.SqlString OnPrepareStatement(global::NHibernate.SqlCommand.SqlString sql)
        {
            Trace.WriteLine(sql.ToString());
            return sql;
        }
    }
}
