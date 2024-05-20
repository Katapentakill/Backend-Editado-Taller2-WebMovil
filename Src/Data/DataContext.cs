using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Gender> Genders {get; set;} = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<ProductType> ProductTypes { get; set; } = null!;

        public DbSet<Purchase> Purchases { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Role> Roles { get; set;} = null!;

        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}