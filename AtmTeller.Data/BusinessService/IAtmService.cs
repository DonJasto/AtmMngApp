using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AtmTeller.Data.DataModel;

namespace AtmTeller.Data.BusinessService
{
    public interface IAtmService
    {
        int AddAtm(AtmModel atm);

        bool AddAtmComponents(AtmModel atm);

        bool AddAtmComponent(AtmModel atm, AtmComponentModel atmComponent);

        DataTable GetAllAtms();

        List<AtmModel> GetAllAtmsList();

        AtmModel GetAtmData(int atmId);

        DataTable GetComponentByAtmId(int atmId);

        bool UpdateAtm(AtmModel atm);

        bool DeleteAtm(int atmId);

        bool UpdateAtmComponent(AtmModel atm , AtmComponentModel atmComponent);

        AtmModel GetAtmByOrderId(int orderId);
    }
}
