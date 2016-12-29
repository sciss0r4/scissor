namespace DatabaseModels.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseModels.TextDataManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DatabaseModels.TextDataManagerContext context)
        {
            context.FileRepositories.AddOrUpdate(p => p.Id,
            new FileRepository { Id = 1, Path = @"C:\Repositories\A", Label = "Repository A", SpaceInKbytes = 102400000, SpaceLeft = 102400000 },
            new FileRepository { Id = 2, Path = @"C:\Repositories\B", Label = "Repository B", SpaceInKbytes = 51200000, SpaceLeft = 51200000 },
            new FileRepository { Id = 3, Path = @"C:\Repositories\C", Label = "Repository C", SpaceInKbytes = 512000000, SpaceLeft = 512000000 });

            context.IndexRepositories.AddOrUpdate(p => p.Id,
            new IndexRepository { Id = 1, Path = @"C:\Repositories\Index" });
        }
    }
}