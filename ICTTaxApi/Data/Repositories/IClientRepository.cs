using ICTTaxApi.Data.Entities;

namespace ICTTaxApi.Data.Repositories
{
    public interface IClientRepository
    {
        Task<bool> Add(Client client);
        Task<Client> GetByName(string clientame);
        Task<bool> Exist(string clientame);
        Task Complete();
    }
}

