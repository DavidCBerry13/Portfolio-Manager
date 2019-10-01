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
        /// Gets the first date the security has data in the system for
        /// </summary>
        public DateTime FirstTradeDate { get; set; }

        /// <summary>
        /// Gets the last date the secirity has data in the system for
        /// </summary>
        public DateTime LastTradeDate { get; set; }

    }
}
