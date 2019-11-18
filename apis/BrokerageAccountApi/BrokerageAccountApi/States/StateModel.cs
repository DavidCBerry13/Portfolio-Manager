using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerageAccountApi.States
{
    /// <summary>
    /// Represents a US State or territory
    /// </summary>
    public class StateModel
    {

        /// <summary>
        /// The two character USPS abbreviation for this state or territory
        /// </summary>
        public String StateCode { get; set; }

        /// <summary>
        /// The name of this US state or territory
        /// </summary>
        public String StateName { get; set; }


    }
}
