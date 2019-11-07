using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmTeller.Data.DataModel
{
    [Serializable()]
    public class MalfunctionReportModel : OrderModel
    {
        private TechnicianModel _technicianModel = new TechnicianModel();
        public TechnicianModel Technician
        {
            get { return _technicianModel; }
            set { _technicianModel = value; }
        }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string StateCurrent { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string ActionsTaken { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string StateAfter { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string SpareMaterial { get; set; }

        /// <summary>
        /// Gets or sets Error time hours minutes sec 
        /// </summary>
        public DateTime MalfunctionTime { get; set; }

        /// <summary>
        /// Gets or sets Error time hours minutes sec 
        /// </summary>
        public DateTime MalfunctionTimeEnd { get; set; }

        /// <summary>
        /// Gets or sets Error time hours minutes sec 
        /// </summary>
        //public TechnicianModel Technician { get; set; }
    }
}
