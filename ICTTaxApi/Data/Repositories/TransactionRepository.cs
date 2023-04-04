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
        

        public async Task<List<Transaction>> Get(int pageNumber, int pageSize, string sortValue)
        {
            var transactionsDb = context.Transactions
                .Include(transaction => transaction.TaxDocument)
                .Include(transaction => transaction.Client);

            var resultList = new List<Transaction>();
            switch (sortValue)
            {
                default:
                case "date":
                    resultList = await transactionsDb.OrderBy(transaction => transaction.TransactionDate).ToListAsync();
                    break;
                case "filename":
                    resultList = await transactionsDb.OrderBy(transaction => transaction.TaxDocument.FileName).ToListAsync(); 
                    break;
                case "updated":
                    resultList = await transactionsDb.OrderBy(transaction => transaction.TaxDocument.UploadedDate).ToListAsync();
                    break;
            }

            resultList = resultList.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();


            return resultList;
        }

        public async Task<List<Transaction>> GetById(int clientId, string sortValue)
        {
            var transactionsDb = context.Transactions
                .Include(transaction => transaction.TaxDocument)
                .Include(transaction => transaction.Client)
                .Where(transaction => transaction.ClientId.Equals(clientId));
            var resultList = new List<Transaction>();

            switch (sortValue)
            {
                default:
                case "date": resultList= await transactionsDb.OrderBy(transaction => transaction.TransactionDate).ToListAsync(); break;
                case "filename": resultList = await transactionsDb.OrderBy(transaction => transaction.TaxDocument.FileName).ToListAsync(); break;
                case "updated": resultList = await transactionsDb.OrderBy(transaction => transaction.TaxDocument.UploadedDate).ToListAsync(); break;
            }

            return  resultList;
        }

        public async Task<string> Add(List<Transaction> transactions, string filename)
        {
            string currentFileName = string.Empty;

                var amounts = transactions.Select(x => x.Amount);
                var newTaxDocument = new TaxDocument()
                {
                    UploadedDate = DateTime.Now,
                    Total = amounts.Sum(amount => amount),
                    FileName = string.Format("{0}_{1}", DateTime.Now.ToFileTime(), filename)
                };

                if (transactions != null)
                {
                    var taxDocument = await context.AddAsync(newTaxDocument);
                    await Save();

                    foreach (var transaction in transactions)
                    {
                        transaction.TaxDocumentId = taxDocument.Entity.Id;
                    }

                    await context.Transactions.AddRangeAsync(transactions);

                    await Save();

                    currentFileName = newTaxDocument.FileName;
                }

            return currentFileName;
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

        private async Task Save()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
