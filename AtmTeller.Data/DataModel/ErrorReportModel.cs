using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.DataModel
{
    [Serializable()]
    public class ErrorReportModel : OrderModel
    {
        /// <summary>
        /// Gets or sets date of birth
        /// </summary>
        
        public DateTime ErrorTime { get; set; }

        /// <summary>
        /// Gets or sets date of birth
        /// </summary>
        public DateTime ErrorTimeEnd { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string StateCurrent { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string ActionsTaken { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string StateAfter { get; set; }


    }
}
