﻿using Castle.Components.DictionaryAdapter;

namespace Web.Migrations
{
    using Base.Infrastructure.Attributes;

    using FluentMigrator.InProc;

    using Infrastructure.Configuration;

    [Component(ComponentLifestyle.Singleton)]
    public class Migrator
    {
        private readonly IPersistenceSettings _settings;

        public Migrator(IPersistenceSettings settings)
        {
            _settings = settings;
        }

        public void MigrateUp()
        {
            var migratorContext = new MigratorContext()
            {
                Connection = _settings.ConnectionString,

                Database = "sqlserver2008",
                MigrationsAssembly = typeof(MigratorContext).Assembly,
            };
            var migrator = new FluentMigrator.InProc.Migrator(migratorContext);
            migrator.MigrateUp();
        }
    }
}