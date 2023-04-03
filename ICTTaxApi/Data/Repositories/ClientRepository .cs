using ICTTaxApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICTTaxApi.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ICTTaxDbContext context;

        public ClientRepository(ICTTaxDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Client>> GetClients(List<string> clients)
        {
            return await context.Clients.Where(client => clients.Contains(client.ClientName)).ToListAsync();
        }

        public async Task<Client> GetByName(string clientname)
        {
            return await context.Clients.FirstOrDefaultAsync(client => client.ClientName.Equals(clientname));
        }

        public async Task AddRange(List<string> clients)
        {
            var clientListDB = await context.Clients.Select(clientDB=>clientDB.ClientName).ToListAsync();
            var newClients = clients.Select(client => new Client{
                                                ClientName = client,
                                                CreateDate = DateTime.Now})
                                    .Where(client => !clientListDB.Contains(client.ClientName)).ToList();


            if (newClients.Count > 0)
            {
                await context.Clients.AddRangeAsync(newClients);
                await Save();
            }
        }

        public async Task<int> Count()
        {
            return await context.Clients.CountAsync();
        }

        public async Task Save()
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

