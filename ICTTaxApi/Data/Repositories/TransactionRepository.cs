using ICTTaxApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICTTaxApi.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ICTTaxDbContext context;

        public TransactionRepository(ICTTaxDbContext context)
        {
            this.context = context;
        }
        

        public async Task<List<Transaction>> Get()
        {
            return await context.Transactions
                .Include(transaction => transaction.TaxDocument)
                .Include(transaction => transaction.Client).ToListAsync();
        }

        public async Task<List<Transaction>> GetById(int clientId)
        {
            return await context.Transactions
                .Include(transaction => transaction.TaxDocument)
                .Where(transaction => transaction.ClientId.Equals(clientId))
                .ToListAsync();
        }

        public async Task Add(List<Transaction> transactions, string filename)
        {
            var amounts = transactions.Select(x => x.Amount);
            var newTaxDocument = new TaxDocument()
            {
                UploadedDate = DateTime.Now,
                Total = amounts.Sum(amount => amount),
                FileName = filename
            };
            newTaxDocument.Transactions.AddRange(transactions);

            context.Add(newTaxDocument);
            await Complete();
        }

        public async Task<int> GetTransactionCount()
        {
            return await context.Transactions.CountAsync();
        }

        public async Task<decimal> GetTotalTaxes()
        {
            var documents = await context.TaxDocuments.Select(taxDocument => taxDocument.Total).ToListAsync();

            return documents.Sum(total => total);   
        }

        public async Task<int> GetTotalFiles()
        {
            return await context.TaxDocuments.CountAsync();
        }

        private async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}
