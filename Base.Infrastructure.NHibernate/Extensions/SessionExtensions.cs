namespace Infrastructure.NHibernate.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::NHibernate;
    using global::NHibernate.Engine;

    public static class SessionExtensions
    {
        public static IEnumerable<Guid> GetLocalKeys<T>(this ISession session)
        {
            var sessionImpl = session.GetSessionImplementation();
            var context = sessionImpl.PersistenceContext;

            var keys = from key in context.EntityEntries.Keys.OfType<T>()
                       let entry = (EntityEntry)context.EntityEntries[key]
                       select (Guid)entry.EntityKey.Identifier;

            return keys;
        }

        public static IEnumerable<T> GetLocalEntities<T>(this ISession session)
        {
            var sessionImpl = session.GetSessionImplementation();
            var context = sessionImpl.PersistenceContext;
            var entities = context.EntityEntries.Keys.OfType<T>().Select(key => key);

            return entities;
        }

        public static IEnumerable<T> GetLocalEntities<T>(this ISession session, Status status)
        {
            var sessionImpl = session.GetSessionImplementation();
            var context = sessionImpl.PersistenceContext;

            var entities = from key in context.EntityEntries.Keys.OfType<T>()
                       let entry = (EntityEntry)context.EntityEntries[key]
                       where entry.Status == status 
                       select key;

            return entities;
        }
    }
}