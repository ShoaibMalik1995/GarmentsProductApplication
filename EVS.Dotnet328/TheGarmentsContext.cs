using EVS.Dotnet328.GarmentsShop;
using EVS.Dotnet328.UsersMgt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVS.Dotnet328
{
    public class TheGarmentsContext : DbContext 
    {
        public TheGarmentsContext()  : base("ConStr")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Product>()
                .HasMany<Color>(p => p.ColorsOffered)
                .WithMany();

            modelBuilder.Entity<Product>()
                .HasMany<Size>(p => p.SizesOffered)
                .WithMany();

            
            //modelBuilder.Entity<User>()
            //    .HasRequired<Address>(u => u.Address)
            //    .WithRequiredDependent();
            
        }


        public DbSet<Country> Countries { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Role>  Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<Fabric> Fabrics { get; set; }

        public DbSet<Product> Products { get; set; }


    }
}
