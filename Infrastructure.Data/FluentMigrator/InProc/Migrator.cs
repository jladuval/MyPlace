namespace FluentMigrator.InProc
{
    using System.Reflection;

    using FluentMigrator.Runner;
    using FluentMigrator.Runner.Initialization;
    using FluentMigrator.Runner.Processors;

    public class Migrator
    {
        private readonly IMigrationRunner _migrationRunner;

        public Migrator(MigratorContext context)
        {
            // Initialize runner context and migration processor
            var runnerContext = BuildRunnerContext(context.Announcer, context.Profile);
            var processor = BuildMigrationProcessor(context.Database, context.Connection, context.Timeout, context.Announcer, context.PreviewOnly);

            // Initialize migration runner
            var runner = BuildMigrationRunner(context.MigrationsAssembly, runnerContext, processor);
            _migrationRunner = runner;
        }

        private static RunnerContext BuildRunnerContext(IAnnouncer announcer, string profile)
        {
            var runnerContext = new RunnerContext(announcer) { Profile = profile };
            return runnerContext;
        }

        private static MigrationRunner BuildMigrationRunner(Assembly migrationAssembly, IRunnerContext runnerContext, IMigrationProcessor processor)
        {
            var migrationRunner = new MigrationRunner(migrationAssembly, runnerContext, processor);
            return migrationRunner;
        }

        private static IMigrationProcessor BuildMigrationProcessor(string database, string connection, int timeout, IAnnouncer announcer, bool previewOnly)
        {
            var processorFactoryProvider = new MigrationProcessorFactoryProvider();
            var processorFactory = processorFactoryProvider.GetFactory(database);
            
            var processorOptions = new ProcessorOptions
            {
                Timeout = timeout,
                PreviewOnly = previewOnly
            };

            var processor = processorFactory.Create(connection, announcer, processorOptions);
            return processor;
        }

        public void MigrateUp()
        {
            _migrationRunner.MigrateUp();
        }

        public void MigrateUp(long version)
        {
            _migrationRunner.MigrateUp(version);
        }

        public void Rollback(int steps)
        {
            _migrationRunner.Rollback(steps);
        }

        public void RollbackToVersion(long version)
        {
            _migrationRunner.RollbackToVersion(version);
        }

        public void RollbackAll()
        {
            _migrationRunner.RollbackToVersion(0);
        }

        public void MigrateDown(long version)
        {
            _migrationRunner.MigrateDown(version);
        }
    }
}
