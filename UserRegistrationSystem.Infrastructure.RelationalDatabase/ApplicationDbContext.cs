using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using UserRegistrationSystem.Infrastructure.RelationalDatabase.DBEntities;

namespace UserRegistrationSystem.Infrastructure.RelationalDatabase
{

    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().Property(p => p.PersonalId).HasMaxLength(11);
            modelBuilder.Entity<Address>().HasKey(k => k.Id).Metadata.IsPrimaryKey();
            modelBuilder.Entity<Address>().HasOne<User>().WithOne(a => a.Address).HasForeignKey<Address>(a => a.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Address> Addresses { get; set; }
    }
}