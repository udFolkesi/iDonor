using BloodDonationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationAPI.Data
{
    public class iDonorDbContext : DbContext
    {
        public iDonorDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blood> Blood { get; set; }
        public DbSet<BloodBank> BloodBank { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<DonationOperation> DonationOperation { get; set; }
        public DbSet<User> User { get; set; }

/*        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship
*//*            modelBuilder.Entity<BloodBank>()
                .HasMany(p => p.DonationOperations)
                .WithOne(c => c.BloodBank)
                .HasForeignKey(c => c.BloodBankID);*//*
        }*/
    }
}
