using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.DataModel
{
    public class RefillModel : OrderModel
    {

        /// <summary>
        /// Gets or sets number of bills to put in atm bill 100 
        /// </summary>
        public int RefillNewBill100 { get; set; }

        /// <summary>
        /// Gets or sets number of bills to put in atm bill 100 
        /// </summary>
        public int RefillNewBill200 { get; set; }

        /// <summary>
        /// Gets or sets number of bills returned from atm bill 100 from printed paper local counter
        /// </summary>
        public int ReturnLocalBill100 { get; set; }

        /// <summary>
        /// Gets or sets number of bills returned from atm bill 200 from printed paper local counter
        /// </summary>
        public int ReturnLocalBill200 { get; set; }

        /// <summary>
        /// Gets or sets number of bills returned from atm bill from printed 100 server counter
        /// </summary>
        public int ReturnServerBill100 { get; set; }

        /// <summary>
        /// Gets or sets number of bills returned from atm bill from printed 200 server counter
        /// </summary>
        public int ReturnServerBill200 { get; set; }

        /// <summary>
        /// Gets or sets number of bills counted in cash 100
        /// </summary>
        public int CashBill100 { get; set; }

        /// <summary>
        /// Gets or sets number of bills counted in cash 200
        /// </summary>
        public int CashBill200 { get; set; }

        /// <summary>
        /// FALI SCANNED DOC
        /// </summary>
        //public int CashBill200 { get; set; }

        /// <summary>
        /// Gets or sets date of birth
        /// </summary>
        public DateTime RefillTime { get; set; }

        /// <summary>
        /// Gets or sets date of birth
        /// </summary>
        public DateTime RefillTimeEnd { get; set; }
    }
}
