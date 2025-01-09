using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;

namespace TradeSphereECommerceApp.Models
{
    public partial class TradeSphereDBModel : DbContext
    {
        public TradeSphereDBModel()
            : base("name=TradeSphereDBModel")
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
