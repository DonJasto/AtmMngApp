using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AtmTeller.Data.DataModel;

namespace AtmTeller.Data.BusinessService
{
    public interface IOrderService
    {
        /// <summary>
        /// Method to get all orders
        /// </summary>
        /// <returns>Data table</returns>
        List<OrderModel> GetAllOrders();

        /// <summary>
        /// Method to get all orders by date
        /// </summary>
        /// <returns>Data table</returns>
        //List<OrderModel> GetAllOrdersByDate(DateTime testDate);
        DataTable GetAllOrdersByDate(DateTime testDate);

        /// <summary>
        /// Method to get all refill orders
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllRefillOrders();

        /// <summary>
        /// Method to get  refill order by order_id
        /// </summary>
        /// <returns>Data table</returns>
        RefillModel GetRefillReportByOrderId(int orderId);
        

        /// <summary>
        /// Method to get all refill orders by atm id 
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllRefillOrdersByAtmId(int Id);

        /// <summary>
        /// Method to get all error orders
        /// </summary>
        /// <returns>Data table</returns>
        List<ErrorReportModel> GetAllErrorOrders();

        /// <summary>
        /// Method to get  refill order by order id
        /// </summary>
        /// <returns>Data table</returns>
        ErrorReportModel GetErrorReportByOrderId(int orderId);

        /// <summary>
        /// Method to get all error orders by atm id 
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllErrorOrdersByAtmId(int Id);

        /// <summary>
        /// Method to get all malfunction orders
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllMalfunctionOrders();

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
        /// Method to create new refill order 
        /// </summary>
        /// <param name="orderDate">Order Date model</param>
        /// <param name="atm">atm model</param>
        /// <param name="user">user model</param>
        /// <returns>true or false</returns>
        bool AddRefillOrder(DateTime orderDate, AtmModel atm, AtmUserModel user, int refillNewBill100, int refillNewBill200);

        /// <summary>
        /// Method to create error order
        /// </summary>
        /// <param name="orderDate">Order Date model</param>
        /// <param name="atm">atm model</param>
        /// <param name="user">user model</param>
        /// <returns>true or false</returns>
        bool AddErrorOrder(DateTime orderDate, AtmModel atm, AtmUserModel user);

        /// <summary>
        /// Method to create malfunction order
        /// </summary>
        /// <param name="orderDate">Order Date model</param>
        /// <param name="atm">atm model</param>
        /// <param name="user">user model</param>
        /// <returns>true or false</returns>
        bool AddMalfunctionOrder(DateTime orderDate, AtmModel atm, AtmUserModel user);

        

        /// <summary>
        /// Method to update club member details
        /// </summary>
        /// <param name="clubMember">club member</param>
        /// <returns></returns>
        bool UpdateRefillOrder(RefillModel refillModel);


        /// <summary>
        /// Method to update error order
        /// </summary>
        /// <param name="errorModel">errorModel</param>
        /// <returns></returns>
        bool UpdateErrorOrder(ErrorReportModel errorModel);

        MalfunctionReportModel GetMalfunctionReportByOrderId(int orderId);

        /// <summary>
        /// Method to update malfunction order
        /// </summary>
        /// <param name="malfunctionModel">malfunctionModel</param>
        /// <returns></returns>
        bool UpdateMalfunctionOrder(MalfunctionReportModel malfunctionModel);

        /// <summary>
        /// Method to delete order
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <returns>true / false</returns>
        bool DeleteOrder(int orderId);

        List<TechnicianModel> GetAllTechniciansByCompanyName(string companyName);

       
        List<RefillModel> GetAllAtmRefillOrders(AtmModel atm);

        List<RefillModel> GetAllAtmsRefillOrders();

        List<MalfunctionReportModel> GetAllMalfunctionsList();

    }
}
