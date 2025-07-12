namespace MyServer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyServer.GameServerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//需要在App.config中配置
        }

        protected override void Seed(MyServer.GameServerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
