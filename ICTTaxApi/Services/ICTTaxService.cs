using AutoMapper;
using ICTTaxApi.Data.Entities;
using ICTTaxApi.Data.Repositories;
using ICTTaxApi.DTOs;

namespace ICTTaxApi.Services
{
    public class ICTTaxService: IICTTaxService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;

        public ICTTaxService(ITransactionRepository transactionRepository,
            IClientRepository clientRepository, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AddTransactions(List<TransactionDTO> transactionsDTO,string filename)
        {
            var updateClients = false;

            foreach(var transactionDTO in transactionsDTO)
            {
                var clientExists = await clientRepository.Exist(transactionDTO.ClientName);

                if (!clientExists)
                {
                    await clientRepository.Add(new Client()
                    {
                        ClientName = transactionDTO.ClientName,
                        CreateDate = DateTime.Now
                    });
                    updateClients = true;
                }
            }

            if (updateClients)
                await clientRepository.Complete();

            var transactionList = mapper.Map<List<Transaction>>(transactionsDTO);

            transactionRepository.Add(transactionList, filename);

            return true;
        }

        public async  Task<List<TransactionDTO>> GetClientTransactions(string clientname)
        {
            var client = await clientRepository.GetByName(clientname);

            if (client == null)
                return null;

            var transactions = await transactionRepository.GetById(client.Id);

            return mapper.Map<List<TransactionDTO>>(transactions);
        }

        public async  Task<TransactionSummaryDTO> GetSummary()
        {
            return new TransactionSummaryDTO()
            {
                TotalTransactions = await transactionRepository.GetTransactionCount(),
                TotalAmount = await transactionRepository.GetTotalTaxes(),
                FileCount = await transactionRepository.GetTotalFiles()
            };
        }

        public async Task<List<TransactionDTO>> GetTransactions()
        {
            var transactions =  await transactionRepository.Get();

            return mapper.Map<List<TransactionDTO>>(transactions);

        }
    }
}
