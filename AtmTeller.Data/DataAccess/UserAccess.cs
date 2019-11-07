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
    class UserAccess : ConnectionAccess , IUserAccess
    {
        public bool AddAtmUser(AtmUserModel user)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {

                    //TO DO napravit storanu procc za update component 
                    cmd = new SqlCommand("procAddAtmUser", con);
                    cmd.Parameters.Add(new SqlParameter("@Name", user.Name));
                    cmd.Parameters.Add(new SqlParameter("@Surname", user.Surname));
                    cmd.Parameters.Add(new SqlParameter("@Contact_number", user.ContactNumber));
                    cmd.Parameters.Add(new SqlParameter("@Contact_email", user.Email));
                    cmd.Parameters.Add(new SqlParameter("@Branch", user.Branch));
                    cmd.Parameters.Add(new SqlParameter("@Company", user.Company));
                    cmd.Parameters.Add(new SqlParameter("@Atm_user_name", user.UserName));
                    cmd.Parameters.Add(new SqlParameter("@Atm_user_password", user.UserPassword));
                    cmd.Parameters.Add(new SqlParameter("@User_type", user.UserType));





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

        public bool AddTechnician(TechnicianModel technician)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {

                    //TO DO napravit storanu procc za update component 
                    cmd = new SqlCommand("procAddTechnician", con);
                    cmd.Parameters.Add(new SqlParameter("@Name", technician.Name));
                    cmd.Parameters.Add(new SqlParameter("@Surname", technician.Surname));
                    cmd.Parameters.Add(new SqlParameter("@Contact_number", technician.ContactNumber));
                    cmd.Parameters.Add(new SqlParameter("@Contact_name", technician.Email));
                    cmd.Parameters.Add(new SqlParameter("@Company", technician.Company));

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

        public List<AtmUserModel> GetAllUsers()
        {
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                //Create Technician List 
                List<AtmUserModel> _users = new List<AtmUserModel>();

                try
                {
                    cmd = new SqlCommand("procGetAllAtmUsers", con);
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    foreach (DataRow row in dt.Rows)
                    {
                        AtmUserModel _user = new AtmUserModel();
                        _user.Id = Convert.ToInt32(row["Atm_user_ID"]);
                        _user.Name = row["Name"].ToString();
                        _user.Name = row["Surname"].ToString();
                        _user.Name = row["Contact_number"].ToString();
                        _user.Name = row["Contact_email"].ToString();
                        _user.Name = row["Branch"].ToString();
                        _user.Name = row["Company"].ToString();
                        _user.Name = row["Atm_user_name"].ToString();
                        _user.Name = row["Atm_user_password"].ToString();
                        _user.Name = row["User_type"].ToString();
                        

                        _users.Add(_user);

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
                return _users;



            }
        }

        public AtmUserModel GetAtmUserData(int userId)
        {
            DataTable data = new DataTable();
            //DataRow dataRow;
            AtmUserModel _atmUser = new AtmUserModel();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                //DataRow dataRow = null;
                try
                {
                    cmd = new SqlCommand("procGetAtmUserDataById", con);
                    cmd.Parameters.Add(new SqlParameter("@Atm_user_ID", userId));
                    //cmd.Parameters.Add(new SqlParameter("@Component_name", componentName));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    _atmUser.Id = Convert.ToInt16(dt.Rows[0]["Atm_user_ID"]);
                    _atmUser.Name = dt.Rows[0]["Name"].ToString();
                    _atmUser.Surname = dt.Rows[0]["Surname"].ToString();
                    _atmUser.ContactNumber = dt.Rows[0]["Contact_number"].ToString();
                    _atmUser.Email = dt.Rows[0]["Contact_email"].ToString();
                    _atmUser.Branch = dt.Rows[0]["Branch"].ToString();
                    _atmUser.Company = dt.Rows[0]["Company"].ToString();
                    _atmUser.UserType = dt.Rows[0]["User_type"].ToString();

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
                return _atmUser;



            }
        }

        public TechnicianModel GetTechnicianData(int userId)
        {
            DataTable data = new DataTable();
            //DataRow dataRow;
            TechnicianModel _technician = new TechnicianModel();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                //DataRow dataRow = null;
                try
                {
                    cmd = new SqlCommand("procGetTehnicianDataById", con);
                    cmd.Parameters.Add(new SqlParameter("@Tehnician_ID", userId));
                    //cmd.Parameters.Add(new SqlParameter("@Component_name", componentName));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cmd.Connection.Close();

                    _technician.Id = Convert.ToInt16(dt.Rows[0]["Tehnician_ID"]);
                    _technician.Name = dt.Rows[0]["Name"].ToString();
                    _technician.Surname = dt.Rows[0]["Surname"].ToString();
                    _technician.ContactNumber = dt.Rows[0]["Contact_number"].ToString();
                    _technician.Email = dt.Rows[0]["Contact_name"].ToString();
                    _technician.Company = dt.Rows[0]["Company"].ToString();
                    

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
                return _technician;



            }
        }

        public DataTable GetAllAtmUsers()
        {
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("procGetAllAtmUsers", con);
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
    }//End GetAllUsers

        public DataTable GetAllTechnicians()
        {
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("procGetAllTechnicians", con);
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




        }//End GetAllTechnicians

        public List<TechnicianModel> GetAllTechniciansList()
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
                    cmd = new SqlCommand("procGetAllTechnicians", con);
                    
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
                        _technician.Surname = row["Surname"].ToString();
                        _technician.ContactNumber = row["Contact_number"].ToString();
                        _technician.Email = row["Contact_name"].ToString();
                        _technician.Company = row["Company"].ToString();

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

        public bool UpdateAtmUser(AtmUserModel atmUser)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {

                    //TO DO napravit storanu procc za update component 
                    cmd = new SqlCommand("procUpdateAtmUser", con);
                    cmd.Parameters.Add(new SqlParameter("@Atm_user_ID", atmUser.Id));
                    cmd.Parameters.Add(new SqlParameter("@Name", atmUser.Name));
                    cmd.Parameters.Add(new SqlParameter("@Surname", atmUser.Surname));
                    cmd.Parameters.Add(new SqlParameter("@Contact_number", atmUser.ContactNumber));
                    cmd.Parameters.Add(new SqlParameter("@Contact_email", atmUser.Email));
                    cmd.Parameters.Add(new SqlParameter("@Branch", atmUser.Branch));
                    cmd.Parameters.Add(new SqlParameter("@Company", atmUser.Company));
                    cmd.Parameters.Add(new SqlParameter("@User_type", atmUser.UserType));





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

        public bool UpdateTechnician(TechnicianModel technician)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {

                    //TO DO napravit storanu procc za update component 
                    cmd = new SqlCommand("procUpdateTechnician", con);
                    cmd.Parameters.Add(new SqlParameter("@Tehnician_ID", technician.Id));
                    cmd.Parameters.Add(new SqlParameter("@Name", technician.Name));
                    cmd.Parameters.Add(new SqlParameter("@Surname", technician.Surname));
                    cmd.Parameters.Add(new SqlParameter("@Contact_number", technician.ContactNumber));
                    cmd.Parameters.Add(new SqlParameter("@Contact_name", technician.Email));
                    cmd.Parameters.Add(new SqlParameter("@Company", technician.Company));
                   





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

        public bool DeleteAtmUser(int userId)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procDeleteAtmUser", con);
                    cmd.Parameters.Add(new SqlParameter("@Atm_user_ID", userId));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    //MessageBox.Show(x.GetBaseException().ToString(), "Error",
                    //MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Error : Odabrani korisnik je u evidenciji intervencija i nemože se brisati dok se ne izbrišu izvještaji intervencija koje je unosio",
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

        public bool DeleteTechnician(int technicianId)
        {
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();//maknit
                var rowsAffected = 0;
                try
                {
                    cmd = new SqlCommand("procDeleteTehnician", con);
                    cmd.Parameters.Add(new SqlParameter("@Tehnician_ID", technicianId));

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception x)
                {
                    //MessageBox.Show(x.GetBaseException().ToString(), "Error",
                            //MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Error : Odabrani serviser je u evidenciji kvarova i nemože se brisati dok se ne izbrišu izvještaji kvarova koje je unosio",
                        "Program nije u mogučnosti izvršiti zadanu naredbu",MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                finally
                {
                    cmd.Dispose();
                    
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
