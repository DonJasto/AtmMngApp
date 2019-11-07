using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.Enums
{
    using System.ComponentModel;

    public enum OrderStatus
    {
        /// <summary>
        /// OrderType - Punjenje
        /// </summary>
        [Description("Izvršeno")]
        Punjenje = 1,

        /// <summary>
        /// OrderType - Greska
        /// </summary>
        [Description("Nije izvršeno")]
        Greska = 2

    }
}
