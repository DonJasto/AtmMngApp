using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.DataModel
{
    [Serializable()]
    public class AtmUserModel
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
        /// Gets or sets email adress
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets Branch
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets Branch
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Gets or sets Type Manager or Teller
        /// </summary>
        public string UserType { get; set; }
    }
}
