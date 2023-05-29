using Divisima.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Contexts
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }


        public DbSet<Slide> Slide { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Newscast> Newscasts { get; set; }
        public DbSet<Institutional> Institutional { get; set; }
        public DbSet<ProductPicture> ProductPicture { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<District> District { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(x => new { x.ProductID, x.CategoryID });

            modelBuilder.Entity<Product>().HasOne(x => x.Brand).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<District>().HasOne(x => x.City).WithMany(x => x.districts).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>().HasOne(x => x.City).WithMany(x => x.Orders).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderDetail>().HasOne(x => x.Product).WithMany(x => x.OrderDetails).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>().HasIndex(x => x.OrderNumber).IsUnique().HasDatabaseName("OrderNumberUnique");

            modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentID);

            modelBuilder.Entity<Admin>().HasData(new Admin { ID = 1, Name = "Ağacan", Surname = "Ergün", Password = "4c49a6720254293c040d06f1207d6796", UserName = "ağacan" });
        }
    }
}
