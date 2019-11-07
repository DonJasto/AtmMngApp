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
    public class AtmService : IAtmService
    {
        /// <summary>
        /// interface of AtmAccess
        /// </summary>
        private IAtmAccess atmAccess;

        /// <summary>
        /// Initializes a new instance of the AtmService class
        /// </summary>
        public AtmService()
        {
            this.atmAccess = new AtmAccess();
        }

        public int AddAtm(AtmModel atm)
        {
            return this.atmAccess.AddAtm(atm);
        }

        public bool AddAtmComponents(AtmModel atm)
        {
            if (atm.Id != 0)
            {
                foreach (var component in atm.components)
                {
                    AddAtmComponent(atm, component);
                }


                return true;
            }
            else
            { return false; }
        }

        public bool AddAtmComponent(AtmModel atm, AtmComponentModel atmComponent)
        {
            return this.atmAccess.AddAtmComponent(atm, atmComponent);
        }

        public AtmModel GetAtmByOrderId(int orderId)
        {
            return this.atmAccess.GetAtmByOrderId(orderId);
        }

        public DataTable GetAllAtms()
        {
            return this.atmAccess.GetAllAtms();
        }

        public List<AtmModel> GetAllAtmsList()
        {
            return this.atmAccess.GetAllAtmsList();
        }

        public AtmModel GetAtmData(int atmId)
        {
            return this.atmAccess.GetAtmData(atmId);
        }

        public DataTable GetComponentByAtmId(int atmId)
        {
            return this.atmAccess.GetComponentByAtmId(atmId);
        }

        public bool UpdateAtmComponent(AtmModel atm ,AtmComponentModel atmComponent)
        {
            return this.atmAccess.UpdateAtmComponent(atm , atmComponent);
        }

        public bool UpdateAtm(AtmModel atm)
        {
            return this.atmAccess.UpdateAtm(atm);
        }

        public bool DeleteAtm(int atmId)
        {
            return this.atmAccess.DeleteAtm(atmId);
        }
        
    }
}

