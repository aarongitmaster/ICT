using ICTTaxApi.Data.Repositories;
using ICTTaxApi.DTOs;
using System.Transactions;

namespace ICTTaxApi.Services
{
    public interface IICTTaxService
    {
        Task<List<TransactionDTO>> GetTransactions(int pageNumber, int pageSize);
        Task<bool> AddTransactions(List<TransactionCreationDTO> transactionsDTO, string filename);
        Task<List<TransactionDTO>> GetClientTransactions(string id);
        Task<TransactionSummaryDTO> GetSummary();
    }
}
