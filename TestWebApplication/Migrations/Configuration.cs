namespace TestWebApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TestWebApplication.Models.DateIntervalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TestWebApplication.Models.DateIntervalContext";
        }

        protected override void Seed(TestWebApplication.Models.DateIntervalContext context)
        {

        }
    }
}
