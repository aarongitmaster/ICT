using ICTTaxApi.Data.Entities;

namespace ICTTaxApi.Data.Repositories
{
    public interface IClientRepository
    {
        Task AddRange(List<string> clients);
        Task<List<Client>> GetClients(List<string> clients);
        Task<Client> GetByName(string clientame);
        Task<int> Count();
        Task Complete();
    }
}

