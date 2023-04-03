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

        public async Task<bool> AddTransactions(List<TransactionCreationDTO> transactionsDTO,string filename)
        {
            var clientDTOList = transactionsDTO.Select(transation=> transation.ClientName).ToList();
            await clientRepository.AddRange(clientDTOList);

            var dbClients = await clientRepository.GetClients(clientDTOList);

            foreach (var transaction in transactionsDTO)
            {
                transaction.ClientId =
                    dbClients.Where(clientdb => clientdb.ClientName.Equals(transaction.ClientName))
                    .Select(clientdb => clientdb.Id)
                    .FirstOrDefault();
            }

            var transactionList = mapper.Map<List<Transaction>>(transactionsDTO);

            await transactionRepository.Add(transactionList, filename);

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
                TotalClients = await clientRepository.Count(),
                TotalTransactions = await transactionRepository.GetTransactionCount(),
                TotalAmount = await transactionRepository.GetTotalTaxes(),
                FileCount = await transactionRepository.GetTotalFiles()
            };
        }

        public async Task<List<TransactionDTO>> GetTransactions(int pageNumber, int pageSize)
        {
            var transactions =  await transactionRepository.Get(pageNumber, pageSize);

            return mapper.Map<List<TransactionDTO>>(transactions);

        }
    }
}
