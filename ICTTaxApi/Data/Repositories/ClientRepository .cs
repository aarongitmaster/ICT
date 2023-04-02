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

        public async  Task<bool> Add(Client client)
        {
            await context.Clients.AddAsync(client);
            return true;
        }

        public async Task<Client> GetByName(string clientname)
        {
            return await context.Clients.FirstOrDefaultAsync(client => client.ClientName.Equals(clientname));
        }

        public async Task<bool> Exist(string clientname)
        {
            return await context.Clients.AnyAsync(client => client.ClientName.Equals(clientname));
        }

        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}

