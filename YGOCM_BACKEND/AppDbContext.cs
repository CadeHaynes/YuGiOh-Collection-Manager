using Microsoft.EntityFrameworkCore;

using YGOCM_BACKEND.Entities;

namespace YGOCM_BACKEND
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Entity Sets
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CollectionEntry> CollectionEntries { get; set; }

        // Create relations in the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CollectionEntry has one User, with Users having many CollectionEntries
            modelBuilder.Entity<CollectionEntry>()
                .HasOne(c => c.User)
                .WithMany(u => u.Collection)
                .HasForeignKey(c => c.UserId);

            // CollectionEntry has one Card, and Cards can be referenced by many CollectionEntries
            modelBuilder.Entity<CollectionEntry>()
                .HasOne(c => c.Card)
                .WithMany()
                .HasForeignKey(c => c.CardId);

            // CollectionEntry has a unique CardId and UserId
            modelBuilder.Entity<CollectionEntry>()
                .HasIndex(c => new { c.CardId, c.UserId })
                .IsUnique();

            // CollectionEntry requires a valid Quanity
            modelBuilder.Entity<CollectionEntry>()
                .Property(c => c.Quantity)
                .IsRequired();
        }
    }
}
