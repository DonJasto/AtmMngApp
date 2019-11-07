using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.DataModel
{
    [Serializable()]
    public class AtmComponentModel
    {
        /// <summary>
        /// Gets or sets aatm component serial number
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets member name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets member name
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets Atm Status
        /// </summary>
        public string Status { get; set; }
    }
}
