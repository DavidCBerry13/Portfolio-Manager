using AutoMapper;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.TradeDates
{
    /// <summary>
    /// represents a trading date when US Markets were open and end of day pricing data is available for
    /// </summary>
    public class TradeDateModel
    {

        /// <summary>
        /// Gets the date of this TradeDate
        /// </summary>
        public String TradeDate { get; set; }

        /// <summary>
        /// Checks if this Trade Date was the last trading day of the month
        /// </summary>
        public bool IsMonthEnd { get; set; }

        /// <summary>
        /// Checks if this Trade Date was the last trading day of the quarter
        /// </summary>
        public bool IsQuarterEnd { get; set; }

        /// <summary>
        /// Checks if this Trade Date was the last trading day of the calendar year
        /// </summary>
        public bool IsYearEnd { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class TradeDateModelAutomapperProfile : Profile
    {
        public TradeDateModelAutomapperProfile()
        {
            CreateMap<TradeDate, TradeDateModel>()
                .ForMember(
                    dest => dest.TradeDate,
                    opt => opt.MapFrom(src => src.AsString())
                );
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
