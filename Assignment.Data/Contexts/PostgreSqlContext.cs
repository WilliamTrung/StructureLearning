using Assignment.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Contexts
{
    public class PostgreSqlContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasKey(c => c.Id);
            builder.Entity<Category>().HasMany(c => c.Products).WithOne(e => e.Category);
            builder.Entity<Category>().HasOne(c => c.Parent).WithMany(e => e.Children).HasForeignKey(c => c.ParentId);

            builder.Entity<Product>().HasKey(c => c.Id);
            builder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products);
        }
    }
}
