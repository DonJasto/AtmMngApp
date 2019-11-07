using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AtmTeller.Data.DataModel;

namespace AtmTeller.Data.BusinessService
{
    public interface IUserService
    {

        bool AddAtmUser(AtmUserModel user);

        bool AddTechnician(TechnicianModel technician);

        DataTable GetAllAtmUsers();

        List<AtmUserModel> GetAllUsers();

        DataTable GetAllTechnicians();

        AtmUserModel GetAtmUserData(int userId);

        TechnicianModel GetTechnicianData(int userId);

        List<TechnicianModel> GetAllTechniciansList();

        bool UpdateAtmUser(AtmUserModel user);

        bool UpdateTechnician(TechnicianModel technician);

        bool DeleteAtmUser(int userId);

        bool DeleteTechnician(int technicianId);




    }
}
