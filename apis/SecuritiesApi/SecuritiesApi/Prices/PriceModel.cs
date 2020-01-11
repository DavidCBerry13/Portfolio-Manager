using AutoMapper;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Prices
{

    /// <summary>
    /// Represents the closing price of a security for a given day
    /// </summary>
    public class PriceModel
    {
        /// <summary>
        /// The ticker symbol of the security (e.g. "MSFT")
        /// </summary>
        public String Ticker { get; set; }

        /// <summary>
        /// The name of the security (e.g. "Microsoft Corporation")
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public String SecurityType { get; set; }

        /// <summary>
        /// The date this closing price and associated information is for in YYYY-MM-DD format
        /// </summary>
        public String TradeDate { get; set; }

        /// <summary>
        /// The opening price of the security on this day.  This field is sometimes null due to missing
        /// data from the market data provider
        /// </summary>
        public decimal? OpeningPrice { get; set; }

        public decimal? ClosingPrice { get; set; }

        public decimal? DailyHigh { get; set; }

        public decimal? DailyLow { get; set; }

        /// <summary>
        /// The number of shares traded of this security on this day
        /// </summary>
        public long? Volume { get; set; }

        /// <summary>
        /// The daily change in dollars of this security
        /// </summary>
        public decimal? Change { get; set; }

        /// <summary>
        /// The daily percentage change for this security
        /// </summary>
        public decimal? ChangePercent { get; set; }

    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class PriceModelAutomapperProfile : Profile
    {
        public PriceModelAutomapperProfile()
        {
            CreateMap<SecurityPrice, PriceModel>()
                .ForMember(
                    dest => dest.TradeDate,
                    opt => opt.MapFrom(src => src.TradeDate.ToString("yyyy-MM-dd"))
                );
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member



}
