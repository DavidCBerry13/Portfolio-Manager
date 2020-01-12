using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.Domain
{

    /// <summary>
    /// Represents a financial instrument like a stock, bond, mutual fund or cash
    /// </summary>
    public class Security
    {

        protected internal Security()
        {

        }


        protected internal Security(string ticker, string name, string securityType, DateTime firstTradeDate, DateTime lastTradeDate)
        {
            Ticker = ticker;
            Name = name;
            SecurityType = securityType;
            FirstTradeDate = firstTradeDate;
            LastTradeDate = lastTradeDate;
        }


        /// <summary>
        /// Gets/Sets the ticker symbol of the security
        /// </summary>
        public String Ticker { get; protected internal set; }

        /// <summary>
        /// Gets/Sets the name of the security, that is the name of the stock, mutual fund, etc.
        /// </summary>
        public String Name { get; protected internal set; }

        /// <summary>
        /// Gets a code that describes the type of the security (Stock, Mutual Fund, Cash)
        /// </summary>
        public String SecurityType { get; protected internal set; }

        /// <summary>
        /// Gets the first date the security has data in the system for
        /// </summary>
        public DateTime FirstTradeDate { get; protected internal set; }

        /// <summary>
        /// Gets the last date the security has data in the system for
        /// </summary>
        public DateTime LastTradeDate { get; protected internal set; }

    }
}
