using POS.Models;

namespace POS.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<POS.DataAccess.NurTexDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(POS.DataAccess.NurTexDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (NurTexDbContext.GetContext().Set<User>().FirstOrDefault(u => u.Username == "admin") != null) return;
            NurTexDbContext.GetContext().Set<User>().AddOrUpdate(new User()
            {
                Username = "admin",
                Password = "12345",
                Name = "Admin",
                Role = "Admin"
            });
            NurTexDbContext.GetContext().SaveChanges();
        }
    }
}
