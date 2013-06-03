namespace FluentMigrator.InProc
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using FluentMigrator.Runner;
    using FluentMigrator.Runner.Announcers;

    public class MigratorContext
    {
        public MigratorContext()
        {
            // Default values
            Announcer = new ConsoleAnnouncer();
            Timeout = 30;
        }

        public Assembly MigrationsAssembly { get; set; }

        internal IAnnouncer Announcer { get; set; }

        public bool PreviewOnly { get; set; }

        public string Database { get; set; }

        public string Connection { get; set; }

        public int Timeout { get; set; }

        public string Profile { get; set; }
    }
}
