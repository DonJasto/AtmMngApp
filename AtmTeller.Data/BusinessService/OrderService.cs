using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmTeller.Data.DataModel;
using AtmTeller.Data.DataAccess;

namespace AtmTeller.Data.BusinessService
{
    public class OrderService : IOrderService
    {
        /// <summary>
        /// interface of OrderAccess
        /// </summary>
        private IOrderAccess orderAccess;

        /// <summary>
        /// Initializes a new instance of the OrderService class
        /// </summary>
        public OrderService()
        {
            this.orderAccess = new OrderAccess();
        }

        public bool AddErrorOrder(DateTime orderDate, AtmModel atm, AtmUserModel user)
        {
            return this.orderAccess.AddErrorOrder(orderDate, atm, user);
        }

        public bool AddMalfunctionOrder(DateTime orderDate, AtmModel atm, AtmUserModel user)
        {
            return this.orderAccess.AddMalfunctionOrder(orderDate, atm, user);
        }

        public bool AddRefillOrder(DateTime orderDate, AtmModel atm, AtmUserModel user, int refillNewBill100, int refillNewBill200)
        {
            return this.orderAccess.AddRefillOrder(orderDate, atm, user, refillNewBill100, refillNewBill200);
        }

        public bool DeleteOrder(int orderId)
        {
            return this.orderAccess.DeleteOrder(orderId);
        }

        public DataTable GetAllCards()
        {
            return this.orderAccess.GetAllCards();
        }

        public DataTable GetAllCardsByAtmId(int Id)
        {
            return this.orderAccess.GetAllCardsByAtmId(Id);
        }

        public DataTable GetAllErrorOrdersByAtmId(int Id)
        {
            return this.orderAccess.GetAllErrorOrdersByAtmId(Id);
        }

        public DataTable GetAllMalfunctionOrders()
        {
            return this.orderAccess.GetAllMalfunctionOrders();
        }

        public DataTable GetAllMalfunctionOrdersByAtmId(int Id)
        {
            return this.orderAccess.GetAllMalfunctionOrdersByAtmId(Id);
        }

        public List<OrderModel> GetAllOrders()
        {
            return this.orderAccess.GetAllOrders();
        }

        public DataTable GetAllOrdersByDate(DateTime testDate)
        {
            return this.orderAccess.GetAllOrdersByDate(testDate);
        }

        public DataTable GetAllRefillOrders()
        {
            return this.orderAccess.GetAllRefillOrders();
        }

        /// <summary>
        /// Method to get  refill order by order_id
        /// </summary>
        /// <returns>Data table</returns>
        public RefillModel GetRefillReportByOrderId(int orderId)
        {
            AtmService atmService = new AtmService();
            RefillModel refillReport = new RefillModel();
            refillReport = this.orderAccess.GetRefillReportByOrderId(orderId);
            refillReport.ATM = atmService.GetAtmData(refillReport.ATM.Id);
            return refillReport;
        }


        public DataTable GetAllRefillOrdersByAtmId(int Id)
        {
            return this.orderAccess.GetAllRefillOrdersByAtmId(Id);
        }

        public List<ErrorReportModel> GetAllErrorOrders()
        {
            return this.orderAccess.GetAllErrorOrders();
        }

        /// <summary>
        /// Method to get  error order by order_id
        /// </summary>
        /// <returns>Data table</returns>
        public ErrorReportModel GetErrorReportByOrderId(int orderId)
        {
            AtmService atmService = new AtmService();
            ErrorReportModel errorReport = new ErrorReportModel();
            errorReport = this.orderAccess.GetErrorReportByOrderId(orderId);
            errorReport.ATM = atmService.GetAtmData(errorReport.ATM.Id);

            return errorReport;
        }

        public bool UpdateErrorOrder(ErrorReportModel errorModel)
        {
            return this.orderAccess.UpdateErrorOrder(errorModel);
        }

        public bool UpdateMalfunctionOrder(MalfunctionReportModel malfunctionModel)
        {
            return this.orderAccess.UpdateMalfunctionOrder(malfunctionModel);
        }

        public bool UpdateRefillOrder(RefillModel refillModel)
        {
            return this.orderAccess.UpdateRefillOrder(refillModel);
        }

        public List<TechnicianModel> GetAllTechniciansByCompanyName(string companyName)
        {
           return this.orderAccess.GetAllTechniciansByCompanyName(companyName);
        }

        public MalfunctionReportModel GetMalfunctionReportByOrderId(int orderId)
        {
            AtmService atmService = new AtmService();
            MalfunctionReportModel malfunctionReport = new MalfunctionReportModel();
            malfunctionReport = this.orderAccess.GetMalfunctionReportByOrderId(orderId);
            malfunctionReport.ATM = atmService.GetAtmData(malfunctionReport.ATM.Id);

            return malfunctionReport;
        }

        public List<RefillModel> GetAllAtmRefillOrders(AtmModel atm)
        {
            var allAtmsRefill = GetAllAtmsRefillOrders();
            var allAtmRefillOrders = new List<RefillModel>();
            foreach (var item in allAtmsRefill)
            {
                if (item.ATM.Id == atm.Id)
                {
                     allAtmRefillOrders.Add(item);
                }
            }

            return allAtmRefillOrders;
        }

        public List<RefillModel> GetAllAtmsRefillOrders()
        {
            return this.orderAccess.GetAllAtmsRefillOrders();
        }

        public List<MalfunctionReportModel> GetAllMalfunctionsList()
        {
            return this.orderAccess.GetAllMalfunctionsList();
        }
    }
}
