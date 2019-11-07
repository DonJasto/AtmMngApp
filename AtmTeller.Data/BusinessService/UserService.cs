using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmTeller.Data.DataModel;
using AtmTeller.Data.DataAccess;
using System.Data;

namespace AtmTeller.Data.BusinessService
{
    public class UserService : IUserService
    {
        /// <summary>
        /// interface of UserAccess
        /// </summary>
        private IUserAccess userAccess;

        /// <summary>
        /// Initializes a new instance of the UserService class
        /// </summary>
        public UserService()
        {
            this.userAccess = new UserAccess();
        }

        public bool AddAtmUser(AtmUserModel user)
        {
            return this.userAccess.AddAtmUser(user);
        }

        public bool AddTechnician(TechnicianModel technician)
        {
            return this.userAccess.AddTechnician(technician);
        }

        public AtmUserModel GetAtmUserData(int userId)
        {
            return this.userAccess.GetAtmUserData(userId);
        }

        public DataTable GetAllAtmUsers()
        {
            return this.userAccess.GetAllAtmUsers();
        }
        
        public DataTable GetAllTechnicians()
        {
            return this.userAccess.GetAllTechnicians();
        }

        public TechnicianModel GetTechnicianData(int userId)
        {
            return this.userAccess.GetTechnicianData(userId);
        }

        public List<TechnicianModel> GetAllTechniciansList()
        {
            return this.userAccess.GetAllTechniciansList();
        }

        public List<AtmUserModel> GetAllUsers()
        {
            return this.userAccess.GetAllUsers();
        }

        public bool UpdateAtmUser(AtmUserModel user)
        {
            return this.userAccess.UpdateAtmUser(user);
        }

        public bool UpdateTechnician(TechnicianModel technician)
        {
            return this.userAccess.UpdateTechnician(technician);
        }

        public bool DeleteAtmUser(int userId)
        {
            return this.userAccess.DeleteAtmUser(userId);
        }

        public bool DeleteTechnician(int technicianId)
        {
            return this.userAccess.DeleteTechnician(technicianId);
        }
    }
}

