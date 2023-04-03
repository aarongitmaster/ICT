using AutoMapper;
using ICTTaxApi.Data.Entities;
using ICTTaxApi.DTOs;

namespace ICTTaxApi.Tools
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Transaction, TransactionDTO>()
                .ForMember(transaction => transaction.FileName, options => options.MapFrom(x => x.TaxDocument.FileName))
                .ForMember(transaction => transaction.UploadedDate, options => options.MapFrom(x => x.TaxDocument.UploadedDate.ToString("dd MMMM yyyy hh:mm tt")))
                .ForMember(transaction => transaction.ClientName, options => options.MapFrom(x => x.Client.ClientName))
                .ForMember(transaction => transaction.TransactionDate, options => options.MapFrom(x => x.TransactionDate.ToString("dd MMMM yyyy")));

           CreateMap<TransactionCreationDTO, Transaction>();
                //.ForMember(transaction => transaction.FileName, options => options.MapFrom(x => x.TaxDocument.FileName))
                //.ForMember(transaction => transaction.UploadedDate, options => options.MapFrom(x => x.TaxDocument.UploadedDate.ToString("dd Mmm yyyy hh:mmAM/PM")))
                //.ForMember(transaction => transaction.ClientName, options => options.MapFrom(x => x.Client.ClientName))
                //.ForMember(transaction => transaction.TransactionDate, options => options.MapFrom(x => x.TransactionDate.ToString("dd Mmm yyyy")))

        }

    }
}
