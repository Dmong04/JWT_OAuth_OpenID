using DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRAESTRUCTURE.Context
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseOpenIddict<Guid>();
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(u => u.Id);
                u.HasOne(u => u.Token)
                .WithOne(t => t.User)
                .HasForeignKey<Token>(t => t.User_id)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Token>(t =>
            {
                t.HasKey(t => t.User_id);
            });

            modelBuilder.Entity<Country>(c =>
            {
                c.HasKey(c => c.Country_id);

                c.HasMany(c => c.Users)
                .WithOne(u => u.Country)
                .HasForeignKey(u => u.Country_id)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
