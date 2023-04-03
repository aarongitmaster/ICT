﻿using ICTTaxApi.Data.Entities;

namespace ICTTaxApi.Data.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> Get(int pageNumber, int pageSize, string sortValue);
        Task Add(List<Transaction> transactions, string filename);
        Task<List<Transaction>> GetById(int clientId, string sortValue);
        Task<int> GetTransactionCount();
        Task<decimal> GetTotalTaxes();
        Task<int> GetTotalFiles();
    }
}
