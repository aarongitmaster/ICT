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
        public string storeContainerURL { get; set; }

        public ICTTaxService(IConfiguration config,
            ITransactionRepository transactionRepository,
            IClientRepository clientRepository, 
            IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.clientRepository = clientRepository;
            this.mapper = mapper;
            storeContainerURL = config.GetValue<string>("StorageContainerURL");
        }

        public async Task<string> AddTransactions(List<TransactionCreationDTO> transactionsDTO,string filename)
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

           return  await transactionRepository.Add(transactionList, filename);

        }

        public async  Task<List<TransactionDTO>> GetClientTransactions(string clientname, string sortValue)
        {
            var client = await clientRepository.GetByName(clientname);

            if (client == null)
                return null;

            var transactions = await transactionRepository.GetById(client.Id,sortValue);

            var transactionList= mapper.Map<List<TransactionDTO>>(transactions);

            foreach (var transaction in transactionList)
            {
                var fileName = transaction.FileName;
                transaction.FileUrl= $"{storeContainerURL}{fileName}";
            }

            return transactionList;
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

        public async Task<List<TransactionDTO>> GetTransactions(int pageNumber, int pageSize, string sortValue)
        {
            var transactions =  await transactionRepository.Get(pageNumber, pageSize, sortValue);

            var transactionList = mapper.Map<List<TransactionDTO>>(transactions);
            foreach (var transaction in transactionList)
            {
                var fileName = transaction.FileName;
                transaction.FileUrl = $"{storeContainerURL}{fileName}";
            }

            return transactionList;
        }
    }
}
