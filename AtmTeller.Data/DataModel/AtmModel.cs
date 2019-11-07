using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.DataModel
{
    [Serializable()]
    public class AtmModel
    {
        /// <summary>
        /// Gets or sets Atm ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Atm name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Atm Serial Number
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets Atm Model
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets Atm Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets Atm Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets Atm Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets Atm Status
        /// </summary>
        public int AccountingNumber { get; set; }


        /// <summary>
        /// Gets or sets AtmComponent
        /// </summary>
        
        public List<AtmComponentModel> components = new List<AtmComponentModel>();
        //public AtmComponentModel AtmComponent { get; set; }



    }
}
