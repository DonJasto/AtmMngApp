using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.Enums
{
    using System.ComponentModel;

    public enum WorkingStatus
    {
        /// <summary>
        /// OrderType - Punjenje
        /// </summary>
        [Description("U funkciji")]
        Punjenje = 1,

        /// <summary>
        /// OrderType - Greska
        /// </summary>
        [Description("Nije u funkciji")]
        Greska = 2


    }
}
