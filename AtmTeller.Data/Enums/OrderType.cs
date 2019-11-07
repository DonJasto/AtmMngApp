using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.Enums
{
    using System.ComponentModel;

    public enum OrderType
    {
        /// <summary>
        /// OrderType - Punjenje
        /// </summary>
        [Description("Punjenje")]
        Punjenje = 1,

        /// <summary>
        /// OrderType - Greska
        /// </summary>
        [Description("Greska")]
        Greska,

        /// <summary>
        /// OrderType - Kvar
        /// </summary>
        [Description("Kvar")]
        Kvar


    }
}
