using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.DataModel
{
    [Serializable()]
    public class OrderModel
    {   
        private AtmModel _atmModel = new AtmModel();
        private AtmUserModel _atmUserModel = new AtmUserModel();
        /// <summary>
        /// Gets or sets order id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets order Type
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// Gets or sets date of order
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets order status --------- ovo može bit bool 
        /// </summary>
        public string StatusCompleted { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        
        public AtmModel ATM
        {
            get {return _atmModel ; }
            set { _atmModel = value; }
        }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        
        public AtmUserModel UserModel
        {
            get { return _atmUserModel; }
            set { _atmUserModel = value; }
        }
    }
}
