namespace TradeSphereECommerceApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TradeSphereECommerceApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TradeSphereECommerceApp.Models.TradeSphereDBModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TradeSphereDBModel context)
        {
            //context.Managers.AddOrUpdate(b => b.ID, new Manager() { ID = 1, Name = "Trade", Surname = "Sphere", Mail = "tradesphere@gmail.com", Password = "1234567", UserName = "TradeSphere", CreationTime = DateTime.Now, LastLoginTime = DateTime.Now });
        }
    }

}

