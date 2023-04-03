using ICTTaxApi.Data.Repositories;
using ICTTaxApi.DTOs;
using System.Transactions;

namespace ICTTaxApi.Services
{
    public interface IICTTaxService
    {
        Task<List<TransactionDTO>> GetTransactions();
        Task<bool> AddTransactions(List<TransactionCreationDTO> transactionsDTO, string filename);
        Task<List<TransactionDTO>> GetClientTransactions(string id);
        Task<TransactionSummaryDTO> GetSummary();
    }
}
