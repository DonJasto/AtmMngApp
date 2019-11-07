using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using AtmTeller.Data.DataModel;
using System.Windows.Forms;

namespace AtmTeller.Data.DataAccess
{
    class AtmAccess : ConnectionAccess , IAtmAccess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Data table</returns>
        public int AddAtm(AtmModel atm)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                int index = 0;
                try
                {

                    
                    cmd = new SqlCommand("procAddAtm", con);
                    
                    cmd.Parameters.Add(new SqlParameter("@Atm_name", atm.Name));
                    cmd.Parameters.Add(new SqlParameter("@Atm_serial_number", atm.SerialNumber));
                    cmd.Parameters.Add(new SqlParameter("@Atm_model", atm.Model));
                    cmd.Parameters.Add(new SqlParameter("@Atm_address", atm.Address));
                    cmd.Parameters.Add(new SqlParameter("@Atm_location", atm.Location));
                    cmd.Parameters.Add(new SqlParameter("@Atm_status", atm.Status));
                    cmd.Parameters.Add(new SqlParameter("@Atm_accounting_number", atm.AccountingNumber));
                    cmd.Parameters.Add("@new_id", SqlDbType.Int, 30);
                    cmd.Parameters["@new_id"].Direction = ParameterDirection.Output;
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    index = Convert.ToInt32(cmd.Parameters["@new_id"].Value);
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

                if (index > 0)
                {
                    return index;
                }
                else
                {
                    return -1;
                }



            }
        }

        public bool AddAtmComponent(AtmModel atm,AtmComponentModel atmComponent)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {

                    //TO DO napravit storanu procc za update component 
                    cmd = new SqlCommand("procAddComponent", con);
                    cmd.Parameters.Add(new SqlParameter("@Component_serial_number", atmComponent.SerialNumber));
                    cmd.Parameters.Add(new SqlParameter("@Component_name", atmComponent.Name));
                    cmd.Parameters.Add(new SqlParameter("@Component_description", atmComponent.Description));
                    cmd.Parameters.Add(new SqlParameter("@Component_status_ok", atmComponent.Status));

                    cmd.Parameters.Add(new SqlParameter("@Atm_ID", atm.Id));





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

        public DataTable GetComponentByAtmId(int atmId)
        {
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                //DataRow dataRow = null;
                try
                {
                    cmd = new SqlCommand("procGetComponentByAtmIdAndName", con);
                    cmd.Parameters.Add(new SqlParameter("@Atm_ID", atmId));
                    //cmd.Parameters.Add(new SqlParameter("@Component_name", componentName));
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    //dataRow = dt.Rows.Count > 0 ? dt.Rows[0] : null;
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

        public AtmModel GetAtmData(int atmId)
        {
            DataTable data = new DataTable();
            //DataRow dataRow;
            AtmModel _atm = new AtmModel();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                //DataRow dataRow = null;
                try
                {
                    cmd = new SqlCommand("procGetAtmById", con);
                    cmd.Parameters.Add(new SqlParameter("@Atm_ID", atmId));
                    //cmd.Parameters.Add(new SqlParameter("@Component_name", componentName));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    _atm.Id = Convert.ToInt16(dt.Rows[0]["Atm_ID"]);
                    _atm.Name = dt.Rows[0]["Atm_name"].ToString();
                    _atm.SerialNumber = dt.Rows[0]["Atm_serial_number"].ToString();
                    _atm.Model = dt.Rows[0]["Atm_model"].ToString();
                    _atm.Address = dt.Rows[0]["Atm_address"].ToString();
                    _atm.Location = dt.Rows[0]["Atm_location"].ToString();
                    // TO DO Fali mi status 
                    _atm.AccountingNumber = Convert.ToInt32(dt.Rows[0]["Atm_accounting_number"]);

                    //dataRow = dt.Rows.Count > 0 ? dt.Rows[0] : null;
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
                return _atm;



            }
        }

        public AtmModel GetAtmByOrderId(int orderId)
        {
            //string test = "2018-01-01";
            //DateTime dTime = DateTime.ParseExact(test, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            DataTable data = new DataTable();
            AtmModel _atm = new AtmModel();


            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataRow dataRow = null;
                try
                {
                    cmd = new SqlCommand("procGetAtmByOrderId", con);
                    cmd.Parameters.Add(new SqlParameter("@Order_ID", orderId));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    

                   
                    _atm.Id = Convert.ToInt16(dt.Rows[0]["Atm_ID"]);
                    _atm.Name = dt.Rows[0]["Atm_name"].ToString();
                    _atm.SerialNumber = dt.Rows[0]["Atm_serial_number"].ToString();
                    _atm.Model = dt.Rows[0]["Atm_model"].ToString();
                    _atm.Address = dt.Rows[0]["Atm_address"].ToString();
                    _atm.Location = dt.Rows[0]["Atm_location"].ToString();
                    // TO DO Fali mi status 
                    _atm.AccountingNumber = Convert.ToInt32(dt.Rows[0]["Atm_accounting_number"]);




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
                return _atm;



            }
        }

        public DataTable GetAllAtms()
        {
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("procGetAllAtms", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(data);
                    cmd.Connection.Close();
                }
                catch (Exception)
                {
                    return data;
                }
            }
            return data;




        }//End GetAllAtms

        public List<AtmModel> GetAllAtmsList()
        {
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                //Create Technician List 
                List<AtmModel> _atms = new List<AtmModel>();

                try
                {
                    cmd = new SqlCommand("procGetAllAtms", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        AtmModel _atm = new AtmModel();
                        _atm.Id = Convert.ToInt32(row["Atm_ID"]);
                        _atm.Name = row["Atm_name"].ToString();
                        _atm.SerialNumber = row["Atm_serial_number"].ToString();
                        _atm.Model = row["Atm_model"].ToString();
                        _atm.Address = row["Atm_address"].ToString();
                        _atm.Location = row["Atm_location"].ToString();
                        _atm.Status = row["Atm_status"].ToString();
                        _atm.AccountingNumber = Convert.ToInt32(row["Atm_accounting_number"]);
                        _atms.Add(_atm);

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
                return _atms;



            }

        }//End GetAllAtmsList

        public bool UpdateAtm(AtmModel atm)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {

                    //TO DO napravit storanu procc za update component 
                    cmd = new SqlCommand("procUpdateAtm", con);
                    cmd.Parameters.Add(new SqlParameter("@Atm_ID", atm.Id));
                    cmd.Parameters.Add(new SqlParameter("@Atm_name", atm.Name));
                    cmd.Parameters.Add(new SqlParameter("@Atm_serial_number", atm.SerialNumber));
                    cmd.Parameters.Add(new SqlParameter("@Atm_model", atm.Model));
                    cmd.Parameters.Add(new SqlParameter("@Atm_address", atm.Address));
                    cmd.Parameters.Add(new SqlParameter("@Atm_location", atm.Location));
                    cmd.Parameters.Add(new SqlParameter("@Atm_status", atm.Status));
                    cmd.Parameters.Add(new SqlParameter("@Atm_accounting_number", atm.AccountingNumber));





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

        public bool UpdateAtmComponent(AtmModel atm , AtmComponentModel atmComponent)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {

                    //TO DO napravit storanu procc za update component 
                    cmd = new SqlCommand("procUpdateComponent", con);
                    cmd.Parameters.Add(new SqlParameter("@Component_serial_number", atmComponent.SerialNumber));
                    cmd.Parameters.Add(new SqlParameter("@Component_name", atmComponent.Name));
                    cmd.Parameters.Add(new SqlParameter("@Component_description", atmComponent.Description));
                    cmd.Parameters.Add(new SqlParameter("@Component_status_ok", atmComponent.Status));

                    cmd.Parameters.Add(new SqlParameter("@Atm_ID", atm.Id));





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

        public bool DeleteAtm(int atmId)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procDeleteAtm", con);
                    cmd.Parameters.Add(new SqlParameter("@Atm_ID", atmId));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    //MessageBox.Show(x.GetBaseException().ToString(), "Error",
                    //MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Error : Odabrani bankomat je u evidenciji intervencija i nemože se brisati dok se ne izbrišu izvještaji o odabranom bankomatu",
                                    "Program nije u mogučnosti izvršiti zadanu naredbu", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
