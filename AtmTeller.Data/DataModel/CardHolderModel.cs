using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.DataModel
{
    public class CardHolderModel
    {
        /// <summary>
        /// Gets or sets id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets Branch
        /// </summary>
        public string CardIssuer { get; set; }

        /// <summary>
        /// Gets or sets Branch
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Gets or sets contact number
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets card valid to dd/yy
        /// </summary>
        public string CardValidTo { get; set; }

        /// <summary>
        /// Gets or sets email adress
        /// </summary>
        public DateTime RetentionDate { get; set; }

        /// <summary>
        /// Gets or sets Branch
        /// </summary>
        public string RetentionReason { get; set; }

        /// <summary>
        /// Gets or sets email adress
        /// </summary>
        public DateTime RetrievalDate { get; set; }

        /// <summary>
        /// Gets or sets the branch were client pick up the card ---- mogu stavit i enum 
        /// </summary>
        public string RetrievalBranch { get; set; }

        /// <summary>
        /// Gets or sets town  
        /// </summary>
        public string RetrievalLocation { get; set; }
    }
}
