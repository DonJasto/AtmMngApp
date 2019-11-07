using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AtmTeller.Data.DataAccess
{
    

    /// <summary>
    /// ConnectionAccess class
    /// </summary>
    public abstract class ConnectionAccess
    {
        /// <summary>
        /// Gets connection string
        /// </summary>
        protected string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["AtmMng"].ToString();
            }
        }
    }
}
