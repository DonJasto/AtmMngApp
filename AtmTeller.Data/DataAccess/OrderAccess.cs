using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmTeller.Data.DataModel;
using System.Globalization;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace AtmTeller.Data.DataAccess
{
    class OrderAccess : ConnectionAccess, IOrderAccess
    {
        public bool AddErrorOrder(DateTime orderDate, AtmModel atm, AtmUserModel user)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procAddErrorOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_date", orderDate));
                    cmd.Parameters.Add(new SqlParameter("@Atm_ID", atm.Id));
                    cmd.Parameters.Add(new SqlParameter("@Atm_user_ID", user.Id));


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }

        public bool AddMalfunctionOrder(DateTime orderDate, AtmModel atm, AtmUserModel user)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procAddMalfunctionOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_date", orderDate));
                    cmd.Parameters.Add(new SqlParameter("@Atm_ID", atm.Id));
                    cmd.Parameters.Add(new SqlParameter("@Atm_user_ID", user.Id));


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }

        public bool AddRefillOrder(DateTime orderDate, AtmModel atm, AtmUserModel user, int refillNewBill100 , int refillNewBill200 )
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procAddRefillOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_date", orderDate));
                    cmd.Parameters.Add(new SqlParameter("@Atm_ID", atm.Id));
                    cmd.Parameters.Add(new SqlParameter("@Atm_user_ID", user.Id));
                    cmd.Parameters.Add(new SqlParameter("@Refill_new_bill_100", refillNewBill100));
                    cmd.Parameters.Add(new SqlParameter("@Refill_new_bill_200", refillNewBill200));


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }

        public bool DeleteOrder(int orderId)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procDeleteOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_ID", orderId));
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }

        public DataTable GetAllCards()
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllCardsByAtmId(int Id)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllErrorOrdersByAtmId(int Id)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllMalfunctionOrders()
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllMalfunctionOrdersByAtmId(int Id)
        {
            throw new NotImplementedException();
        }

        public List<OrderModel> GetAllOrders()
        {

             throw new NotImplementedException();
        }

        public DataTable GetAllOrdersByDate(DateTime testDate)
        {
            
            //string test = "2018-01-01";
            //DateTime dTime = DateTime.ParseExact(test, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                List<OrderModel> _orders = new List<OrderModel>();

                try
                {//PROMINITIME PROCEDURE U procGetAllOrdersByDate
                    cmd = new SqlCommand("procGetAllOrdersByDate", con);
                    cmd.Parameters.Add(new SqlParameter("@orderDate", testDate));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    OrderModel _order = new OrderModel();
                    //    _order.Id = Convert.ToInt32(row["Order_ID"]);
                    //    _order.ATM.Name = row["Atm_name"].ToString();
                    //    _order.ATM.SerialNumber = Convert.ToInt32(row["Atm_serial_number"]);
                    //    _order.ATM.Model = row["Atm_model"].ToString();
                    //    _order.ATM.Address = row["Atm_address"].ToString();
                    //    _order.ATM.Location = row["Atm_location"].ToString();
                    //    _order.OrderType = row["Refill_type"].ToString();
                    //    _order.StatusCompleted = row["Order_status_completed"].ToString();
                    //    _orders.Add(_order);

                    //}

                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }
                return dt;



            }

        }

        public DataTable GetAllRefillOrders()
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllRefillOrdersByAtmId(int Id)
        {
            throw new NotImplementedException();
        }

        public RefillModel GetRefillReportByOrderId(int orderId)
        {
            //string test = "2018-01-01";
            //DateTime dTime = DateTime.ParseExact(test, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            RefillModel _refill = new RefillModel();
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataRow dataRow= null;
                try
                {
                    cmd = new SqlCommand("procGetRefillReportByOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_ID", orderId));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();



                    _refill.OrderDate = (DateTime)dt.Rows[0]["Order_date"];
                    _refill.ATM.Id = Convert.ToInt16(dt.Rows[0]["Atm_ID"]);

                    _refill.RefillTime = (DateTime)dt.Rows[0]["Refill_time"];
                    _refill.RefillTimeEnd = (DateTime)dt.Rows[0]["Refill_time_end"];

                    _refill.RefillNewBill100 = Convert.ToInt32(dt.Rows[0]["Refill_new_bill_100"]);
                    _refill.RefillNewBill200 = Convert.ToInt32(dt.Rows[0]["Refill_new_bill_200"]);
                    _refill.ReturnLocalBill100 = Convert.ToInt32(dt.Rows[0]["Return_local_bill_100"]);
                    _refill.ReturnLocalBill200 = Convert.ToInt32(dt.Rows[0]["Return_local_bill_200"]);
                    _refill.ReturnServerBill100 = Convert.ToInt32(dt.Rows[0]["Return_server_bill_100"]);
                    _refill.ReturnServerBill200 = Convert.ToInt32(dt.Rows[0]["Return_server_bill_100"]);

                    _refill.CashBill100 = Convert.ToInt32(dt.Rows[0]["Cash_bill_100"]);
                    _refill.CashBill200 = Convert.ToInt32(dt.Rows[0]["Cash_bill_200"]);

 

                    dataRow = dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }
                return _refill;



            }
        }

        public List<ErrorReportModel> GetAllErrorOrders()
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                //Create Error List 
                List<ErrorReportModel> _errorsReport = new List<ErrorReportModel>();

                try
                {
                    cmd = new SqlCommand("procGetAllErrorOrders", con);
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        ErrorReportModel _error = new ErrorReportModel();
                        _error.Id = Convert.ToInt32(row["Order_ID"]);
                        _error.OrderDate = Convert.ToDateTime(row["Order_date"]);
                        _error.ATM.Id = Convert.ToInt32(row["Atm_ID"]);
                        _error.ATM.Name = row["Atm_name"].ToString();
                        _error.ATM.Address = row["Atm_address"].ToString();
                        _error.OrderType = row["Error_type"].ToString();
                        _error.StateCurrent = row["State_current"].ToString();
                        _error.ActionsTaken = row["Actions_taken"].ToString();
                        _error.StateAfter = row["State_after"].ToString();
                       
                        
                        _errorsReport.Add(_error);

                    }

                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }
                return _errorsReport;



            }
        }

        public List<MalfunctionReportModel> GetAllMalfunctionsList()
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                //Create Malfunction List 
                List<MalfunctionReportModel> _malfunctionReport = new List<MalfunctionReportModel>();

                try
                {
                    cmd = new SqlCommand("procGetAllMalfunctionOrders", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        MalfunctionReportModel _malfunction = new MalfunctionReportModel();
                        _malfunction.Id = Convert.ToInt32(row["Order_ID"]);
                        _malfunction.OrderDate = Convert.ToDateTime(row["Order_date"]);
                        _malfunction.StatusCompleted = row["Order_status_completed"].ToString();
                        _malfunction.ATM.Id = Convert.ToInt32(row["Atm_ID"]);
                        _malfunction.ATM.Name = row["Atm_name"].ToString();
                        _malfunction.ATM.Address = row["Atm_address"].ToString();
                        _malfunction.OrderType = row["Malfunction_type"].ToString();
                        _malfunction.StateCurrent = row["State_current"].ToString();
                        //_malfunction.ActionsTaken = row["Actions_taken"].ToString();
                        //_malfunction.StateAfter = row["State_after"].ToString();
                        _malfunction.SpareMaterial = row["Spare_material"].ToString();
                        _malfunction.Technician.Name = row["Name"].ToString();
                        _malfunction.Technician.Surname = row["Surname"].ToString();
                        _malfunction.Technician.Company = row["Company"].ToString();


                        _malfunctionReport.Add(_malfunction);

                    }

                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }
                return _malfunctionReport;



            }
        }

        public ErrorReportModel GetErrorReportByOrderId(int orderId)
        {
            //string test = "2018-01-01";
            //DateTime dTime = DateTime.ParseExact(test, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            DataTable data = new DataTable();
            ErrorReportModel _errorReport = new ErrorReportModel();


            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataRow dataRow = null;
                try
                {
                    cmd = new SqlCommand("procGetErrorReportByOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_ID", orderId));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    _errorReport.Id = Convert.ToInt16(dt.Rows[0]["Order_ID"]);

                    _errorReport.ATM.Id = Convert.ToInt16(dt.Rows[0]["Atm_ID"]);
                    
                    _errorReport.OrderDate = (DateTime)dt.Rows[0]["Order_date"];
                    _errorReport.ErrorTime = (DateTime)dt.Rows[0]["Error_time"];
                    _errorReport.ErrorTime = (DateTime)dt.Rows[0]["Error_time_end"];
                    

                    _errorReport.StateCurrent = Convert.ToString(dt.Rows[0]["State_current"]);
                    _errorReport.ActionsTaken = Convert.ToString(dt.Rows[0]["Actions_taken"]);
                    _errorReport.StateAfter = Convert.ToString(dt.Rows[0]["State_after"]);



                    dataRow = dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }
                return _errorReport;



            }
        }

        public bool UpdateErrorOrder(ErrorReportModel errorModel)
        {
            //string test = "2018-01-01";
            //DateTime dTime = DateTime.ParseExact(test, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procUpdateErrorOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_ID", errorModel.Id));
                    cmd.Parameters.Add(new SqlParameter("@Error_time", errorModel.ErrorTime));
                    cmd.Parameters.Add(new SqlParameter("@Error_time_end", errorModel.ErrorTimeEnd));
                    cmd.Parameters.Add(new SqlParameter("@State_current", errorModel.StateCurrent));
                    cmd.Parameters.Add(new SqlParameter("@Actions_taken", errorModel.ActionsTaken));
                    cmd.Parameters.Add(new SqlParameter("@State_after", errorModel.StateAfter));
                    cmd.Parameters.Add(new SqlParameter("@Order_status_completed", errorModel.StatusCompleted));




                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }

        public bool UpdateMalfunctionOrder(MalfunctionReportModel malfunctionModel)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procUpdateMalfunctionOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_ID", malfunctionModel.Id));
                    cmd.Parameters.Add(new SqlParameter("@Malfunction_time", malfunctionModel.MalfunctionTime));
                    cmd.Parameters.Add(new SqlParameter("@Malfunction_time_end", malfunctionModel.MalfunctionTimeEnd));
                    cmd.Parameters.Add(new SqlParameter("@State_current", malfunctionModel.StateCurrent));
                    cmd.Parameters.Add(new SqlParameter("@Actions_taken", malfunctionModel.ActionsTaken));
                    cmd.Parameters.Add(new SqlParameter("@State_after", malfunctionModel.StateAfter));
                    cmd.Parameters.Add(new SqlParameter("@Spare_material", malfunctionModel.SpareMaterial));
                    cmd.Parameters.Add(new SqlParameter("@Order_status_completed", malfunctionModel.StatusCompleted));
                    cmd.Parameters.Add(new SqlParameter("@Tehnician_ID", malfunctionModel.Technician.Id));



                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }

        public bool UpdateRefillOrder(RefillModel refillModel)
        {
            //string test = "2018-01-01";
            //DateTime dTime = DateTime.ParseExact(test, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procUpdateRefillOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_ID", refillModel.Id));
                    cmd.Parameters.Add(new SqlParameter("@Refill_time", refillModel.RefillTime));
                    cmd.Parameters.Add(new SqlParameter("@Refill_time_end", refillModel.RefillTimeEnd));
                    cmd.Parameters.Add(new SqlParameter("@Refill_new_bill_100", refillModel.RefillNewBill100));
                    cmd.Parameters.Add(new SqlParameter("@Refill_new_bill_200", refillModel.RefillNewBill200));
                    cmd.Parameters.Add(new SqlParameter("@Return_local_bill_100", refillModel.ReturnLocalBill100));
                    cmd.Parameters.Add(new SqlParameter("@Return_local_bill_200", refillModel.ReturnLocalBill200));
                    cmd.Parameters.Add(new SqlParameter("@Return_server_bill_100", refillModel.ReturnServerBill100));
                    cmd.Parameters.Add(new SqlParameter("@Return_server_bill_200", refillModel.ReturnServerBill200));
                    cmd.Parameters.Add(new SqlParameter("@Cash_bill_100", refillModel.CashBill100));
                    cmd.Parameters.Add(new SqlParameter("@Cash_bill_200", refillModel.CashBill200));
                    cmd.Parameters.Add(new SqlParameter("@Order_status_completed", refillModel.StatusCompleted));

                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }

        public MalfunctionReportModel GetMalfunctionReportByOrderId(int orderId)
        {
            MalfunctionReportModel _malfunction = new MalfunctionReportModel();
            DataTable data = new DataTable();
            
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataRow dataRow = null;
                try
                {
                    cmd = new SqlCommand("procGetMalfunctionReportByOrder", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_ID", orderId));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    _malfunction.Id = Convert.ToInt16(dt.Rows[0]["Order_ID"]);

                    _malfunction.ATM.Id = Convert.ToInt16(dt.Rows[0]["Atm_ID"]);

                    _malfunction.OrderDate = (DateTime)dt.Rows[0]["Order_date"];
                    _malfunction.MalfunctionTime = (DateTime)dt.Rows[0]["Malfunction_time"];
                    _malfunction.MalfunctionTimeEnd = (DateTime)dt.Rows[0]["Malfunction_time_end"];


                    _malfunction.StateCurrent = Convert.ToString(dt.Rows[0]["State_current"]);
                    _malfunction.ActionsTaken = Convert.ToString(dt.Rows[0]["Actions_taken"]);
                    _malfunction.StateAfter = Convert.ToString(dt.Rows[0]["State_after"]);

                    _malfunction.SpareMaterial = Convert.ToString(dt.Rows[0]["Spare_material"]);
                    dataRow = dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }
                return _malfunction;



            }
        }

        public List<TechnicianModel> GetAllTechniciansByCompanyName(string companyName)
        {
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                //Create Technician List 
                List<TechnicianModel> _technicians = new List<TechnicianModel>();

                try
                {
                    cmd = new SqlCommand("procGetAllTechniciansByCompanyName", con);
                    cmd.Parameters.Add(new SqlParameter("@CompanyName", companyName));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        TechnicianModel _technician = new TechnicianModel();
                        _technician.Id = Convert.ToInt32(row["Tehnician_ID"]);
                        _technician.Name = row["Name"].ToString();

                        _technicians.Add(_technician);

                    }
                    
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }
                return _technicians;



            }
        }

        public List<RefillModel> GetAllAtmsRefillOrders()
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                //Create Error List 
                List<RefillModel> _refillList = new List<RefillModel>();

                try
                {
                    cmd = new SqlCommand("procGetAllRefillOrders", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        RefillModel _refill = new RefillModel();
                        _refill.Id = Convert.ToInt32(row["Order_ID"]);
                        _refill.OrderDate = Convert.ToDateTime(row["Order_date"]);
                        //_refill.ATM.Name = row["Atm_status_completed"].ToString();
                        _refill.ATM.Id = Convert.ToInt32(row["Atm_ID"]);
                        _refill.ATM.Name = row["Atm_name"].ToString();
                        _refill.ATM.Address = row["Atm_address"].ToString();
                        _refill.RefillNewBill100 = Convert.ToInt32(row["Refill_new_bill_100"]);
                        _refill.RefillNewBill200 = Convert.ToInt32(row["Refill_new_bill_200"]);
                        _refill.ReturnLocalBill100 = Convert.ToInt32(row["Return_local_bill_100"]);
                        _refill.ReturnLocalBill200 = Convert.ToInt32(row["Return_local_bill_200"]);
                        _refill.ReturnServerBill100 = Convert.ToInt32(row["Return_server_bill_100"]);
                        _refill.ReturnServerBill200 = Convert.ToInt32(row["Return_server_bill_200"]);
                        _refill.CashBill100 = Convert.ToInt32(row["Cash_bill_100"]);
                        _refill.CashBill200 = Convert.ToInt32(row["Cash_bill_200"]);

                        _refill.RefillTime = Convert.ToDateTime(row["Refill_time"]);
                        _refill.RefillTime = Convert.ToDateTime(row["Refill_time_end"]);

                        _refillList.Add(_refill);

                    }

                }
                catch (Exception x)
                {
                    MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Dispose();
                    //pl.MySQLConn.Close();
                }
                return _refillList;



            }
        }

    }
}