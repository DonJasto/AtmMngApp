using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.DataModel
{
    [Serializable()]
    public class TechnicianModel
    {
        /// <summary>
        /// Gets or sets id of technician
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Surnam
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets contact number
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets Company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets email adress
        /// </summary>
        public string Email { get; set; }
    }
}
