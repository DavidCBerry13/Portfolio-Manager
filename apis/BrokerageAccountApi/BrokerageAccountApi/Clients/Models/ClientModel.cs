using AutoMapper;
using BrokerageAccountApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerageAccountApi.Clients
{

    /// <summary>
    /// Represents a client who has one or more investment accounts at the firm
    /// </summary>
    public class ClientModel
    {
        /// <summary>
        /// Creates a new ClientModel object
        /// </summary>
        public ClientModel()
        {
            Accounts = new List<ClientAccountModel>();
        }

        /// <summary>
        /// The unique id number assigned to this client
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// First name of the client
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// Last name of the client
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// Street address of the client
        /// </summary>
        public String StreetAddress { get; set; }

        /// <summary>
        /// City of the client
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// State of the address of the client
        /// </summary>
        public String StateCode { get; set; }

        /// <summary>
        /// Zip code for this clients address
        /// </summary>
        public String ZipCode { get; set; }

        /// <summary>
        /// List of investment accounts for this client
        /// </summary>
        public List<ClientAccountModel> Accounts { get; set; }

        /// <summary>
        /// Model object to represent an investement account owned by this client
        /// </summary>
        public class ClientAccountModel
        {
            /// <summary>
            /// Account number for this account
            /// </summary>
            public String AccountNumber { get; set; }

            /// <summary>
            /// Descriptive name of this account
            /// </summary>
            public String AccountName { get; set; }

            /// <summary>
            /// Status of this account
            /// </summary>
            public String AccountStatus { get; set; }

            /// <summary>
            /// Date this account was opened
            /// </summary>
            public String OpenDate { get; set; }

            /// <summary>
            /// Date this account was closed (null for open accounts)
            /// </summary>
            public String CloseDate { get; set; }

            /// <summary>
            /// Current value of assets in this account
            /// </summary>
            public decimal AccountBalance { get; set; }
        }

    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class ClientModelAutomapperProfile : Profile
    {

        public ClientModelAutomapperProfile()
        {
            CreateMap<Client, ClientModel>()
                .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.State.StateCode));

            CreateMap<InvestmentAccount, ClientModel.ClientAccountModel>()
                .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src => src.AccountStatus.AccountStatusName))
                .ForMember(dest => dest.OpenDate, opt => opt.MapFrom(src => src.OpenDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.CloseDate, opt => opt.MapFrom(src => src.CloseDate.HasValue ? src.CloseDate.Value.ToString("yyyy-MM-dd") : String.Empty))
                .ForMember(dest => dest.AccountBalance, opt => opt.MapFrom(src => src.CurrentValue));
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
