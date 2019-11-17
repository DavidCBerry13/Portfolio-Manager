using AutoMapper;
using BrokerageAccountApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerageAccountApi.Clients
{
    public class ClientModel
    {

        public ClientModel()
        {
            Accounts = new List<ClientAccountModel>();
        }


        public int ClientId { get; set; }


        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String StreetAddress { get; set; }

        public String City { get; set; }

        public String StateCode { get; set; }

        public String ZipCode { get; set; }

        public List<ClientAccountModel> Accounts { get; set; }


        public class ClientAccountModel
        {
            public String AccountNumber { get; set; }

            public String AccountName { get; set; }
            public String AccountStatus { get; set; }

            public String OpenDate { get; set; }

            public String CloseDate { get; set; }

            public decimal AccountBalance { get; set; }
        }

    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class ClientModelAutomapperProfile : Profile
    {

        public ClientModelAutomapperProfile()
        {
            CreateMap<Client, ClientModel>();

            CreateMap<InvestmentAccount, ClientModel.ClientAccountModel>()
                .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src => src.AccountStatus.AccountStatusName))
                .ForMember(dest => dest.OpenDate, opt => opt.MapFrom(src => src.OpenDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.CloseDate, opt => opt.MapFrom(src => src.CloseDate.HasValue ? src.CloseDate.Value.ToString("yyyy-MM-dd") : String.Empty))
                .ForMember(dest => dest.AccountBalance, opt => opt.MapFrom(src => src.CurrentValue));
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
