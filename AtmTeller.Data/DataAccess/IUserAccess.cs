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
    interface IUserAccess
    {

        bool AddAtmUser(AtmUserModel user);

        bool AddTechnician(TechnicianModel technician);

        DataTable GetAllAtmUsers();

        DataTable GetAllTechnicians();
      
        List<AtmUserModel> GetAllUsers();

        AtmUserModel GetAtmUserData(int userId);

        TechnicianModel GetTechnicianData(int userId);

        List<TechnicianModel> GetAllTechniciansList();

        bool UpdateAtmUser(AtmUserModel user);

        bool UpdateTechnician(TechnicianModel technician);

        bool DeleteAtmUser(int userId);

        bool DeleteTechnician(int technicianId);


    }
}
