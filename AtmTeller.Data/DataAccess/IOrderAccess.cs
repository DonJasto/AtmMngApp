using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AtmTeller.Data.DataModel;

namespace AtmTeller.Data.DataAccess
{
    interface IOrderAccess
    {

        /// <summary>
        /// Method to create new Order type Refill
        /// </summary>
        /// <param name="">model</param>
        /// <returns>true or false</returns>
        bool AddRefillOrder(DateTime orderDate, AtmModel atm, AtmUserModel user, int refillNewBill100, int refillNewBill200);

        /// <summary>
        /// Method to create new Order type Error
        /// </summary>
        /// <param name="">model</param>
        /// <returns>true or false</returns>
        bool AddErrorOrder(DateTime orderDate, AtmModel atm, AtmUserModel user);

        /// <summary>
        /// Method to create new Order type Malfunction
        /// </summary>
        /// <param name=""> model</param>
        /// <returns>true or false</returns>
        bool AddMalfunctionOrder(DateTime orderDate , AtmModel atm , AtmUserModel user);

        /// <summary>
        /// Method to get all orders
        /// </summary>
        /// <returns>Data table</returns>
        List<OrderModel> GetAllOrders();

        //List<OrderModel> GetAllOrdersByDate(DateTime testDate);
        DataTable GetAllOrdersByDate(DateTime testDate);

        /// <summary>
        /// Method to get all refill orders
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllRefillOrders();

        /// <summary>
        /// Method to get all refill orders by atm id 
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllRefillOrdersByAtmId(int Id);

        /// <summary>
        /// Method to get all error orders by atm id 
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllErrorOrdersByAtmId(int Id);

        /// <summary>
        /// Method to get  refill by order id 
        /// </summary>
        /// <returns>Data table</returns>
        RefillModel GetRefillReportByOrderId(int Id);

        /// <summary>
        /// Method to get all error orders
        /// </summary>
        /// <returns>List</returns>
        List<ErrorReportModel> GetAllErrorOrders();

        /// <summary>
        /// Method to get  error by order id 
        /// </summary>
        /// <returns>Data table</returns>
        ErrorReportModel GetErrorReportByOrderId(int Id);

        /// <summary>
        /// Method to get all malfunction orders
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllMalfunctionOrders();

        MalfunctionReportModel GetMalfunctionReportByOrderId(int orderId);

        /// <summary>
        /// Method to get all malfunction orders by atm id 
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllMalfunctionOrdersByAtmId(int Id);

        /// <summary>
        /// Method to get all refill orders
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllCards();

        /// <summary>
        /// Method to get all refill orders by atm id 
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllCardsByAtmId(int Id);

        /// <summary>
        /// Method to UpdateRefillOrder
        /// </summary>
        /// <param name="refillModel">refillModel</param>
        /// <returns></returns>
        bool UpdateRefillOrder(RefillModel refillModel);

        /// <summary>
        /// Method to UpdateErrorOrder
        /// </summary>
        /// <param name="errorModel">errorModel</param>
        /// <returns></returns>
        bool UpdateErrorOrder(ErrorReportModel errorModel);

        /// <summary>
        /// Method to UpdateMalfunctionOrder
        /// </summary>
        /// <param name="malfunctionModel">malfunctionModel</param>
        /// <returns></returns>
        bool UpdateMalfunctionOrder(MalfunctionReportModel malfunctionModel);

        /// <summary>
        /// Method to DeleteOrder
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <returns>true / false</returns>
        bool DeleteOrder(int orderId);

        List<TechnicianModel> GetAllTechniciansByCompanyName(string companyName);

        

        List<RefillModel> GetAllAtmsRefillOrders();

        List<MalfunctionReportModel> GetAllMalfunctionsList();
    }
}
