using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AtmTeller.Data.DataModel;

namespace AtmTeller.Data.DataAccess
{
    /// <summary>
    /// Interface IAtmMngAccess
    /// </summary>
    interface IAtmAccess
    {
        int AddAtm(AtmModel atm);

        bool AddAtmComponent(AtmModel atm, AtmComponentModel atmComponent);

        /// <summary>
        /// Method to get all atms
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllAtms();

        List<AtmModel> GetAllAtmsList();

        DataTable GetComponentByAtmId(int atmId);

        bool UpdateAtmComponent(AtmModel atm , AtmComponentModel atmComponentModel);

        AtmModel GetAtmData(int atmId);

        bool UpdateAtm(AtmModel atm);

        bool DeleteAtm(int atmId);

        AtmModel GetAtmByOrderId(int orderId);
    }
}
