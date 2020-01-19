using AutoMapper;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Securities
{
    public class SecurityModel
    {

        /// <summary>
        /// Gets/Sets the ticker symbol of the security
        /// </summary>
        public String Ticker { get; set; }

        /// <summary>
        /// Gets/Sets the name of the security, that is the name of the stock, mutual fund, etc.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets a code that describes the type of the security (Stock, Mutual Fund, Cash)
        /// </summary>
        public String SecurityType { get; set; }

        /// <summary>
        /// Gets the first date the security has data in the system for in YYYY-MM-DD format
        /// </summary>
        public String FirstTradeDate { get; set; }

        /// <summary>
        /// Gets the last date the security has data in the system for in YYYY-MM-DD format
        /// </summary>
        public String LastTradeDate { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class SecurityModelAutomapperProfile : Profile
    {
        public SecurityModelAutomapperProfile()
        {
            CreateMap<Security, SecurityModel>()
                .ForMember(
                    dest => dest.FirstTradeDate,
                    opt => opt.MapFrom(src => src.FirstTradeDate.ToString("yyyy-MM-dd"))
                )
                .ForMember(
                    dest => dest.LastTradeDate,
                    opt => opt.MapFrom(src => src.LastTradeDate.ToString("yyyy-MM-dd"))
                );
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
