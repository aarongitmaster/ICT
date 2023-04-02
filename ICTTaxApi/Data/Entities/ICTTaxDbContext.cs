using Microsoft.EntityFrameworkCore;

namespace ICTTaxApi.Data.Entities
{
    public class ICTTaxDbContext : DbContext
    {
        public ICTTaxDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TaxDocument> TaxDocuments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<TaxDocument>()
                .HasMany(taxDocument => taxDocument.Transactions)
                .WithOne(transaction => transaction.TaxDocument)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }

    }
}
