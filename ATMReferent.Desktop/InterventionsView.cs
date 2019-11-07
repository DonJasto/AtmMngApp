using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ATMReferent.Desktop.Properties;
using AtmTeller.Data.BusinessService;
using AtmTeller.Data.DataModel;
using AtmTeller.Data.Enums;
using Microsoft.Reporting.WinForms;

namespace ATMReferent.Desktop
{
    public partial class InterventionsView : Form
    {

        /// <summary>
        /// Interface of OrderService
        /// </summary>
        IOrderService orderService;
        IAtmService atmService;
        IUserService userService;
        DataTable atmData;
        DataView atmDataView;
        DateTime dateOfInter = DateTime.Today;//yyyy-mm-dd
        DateTime currentDate = DateTime.Today;//yyyy-mm-dd
        DateTime dateOfInterStatus = DateTime.Today;//yyyy-mm-dd

        /// <summary>
        /// Variable to store error message
        /// </summary>
        private string errorMessage;

        /// <summary>
        /// Order id
        /// </summary>
        private int orderId;
        private int atmId;
        private int userId;

        public InterventionsView()
        {
            InitializeComponent();
            this.CustomizeInterventionsView();

            orderService = new OrderService();
            userService = new UserService();
            atmService = new AtmService();


            ///Init Blank report
            ///
            ///
            this.rVReports.LocalReport.DataSources.Clear();
            rVReports.Reset();
            rVReports.LocalReport.ReportPath = "Reports/BlankReport.rdlc";
            var allAtmsDataSource = new AtmService();
            var allAtms = allAtmsDataSource.GetAllAtmsList();
            Microsoft.Reporting.WinForms.ReportDataSource rprtDTSource = new Microsoft.Reporting.WinForms.ReportDataSource();
            rprtDTSource.Name = "DataSet1";
            rprtDTSource.Value = this.AtmModelBindingSource;
            this.rVReports.LocalReport.DataSources.Add(rprtDTSource);
            this.AtmModelBindingSource.DataSource = allAtms;
            this.rVReports.RefreshReport();


        }

        /// <summary>
        /// Method to show general error message on any system level exception
        /// </summary>
        private void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                //Resources.System_Error_Message, 
                Resources.System_Error_Message_Title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// To generate the error message
        /// </summary>
        /// <param name="error">error message</param>
        private void AddErrorMessage(string error)
        {
            if (this.errorMessage == string.Empty)
            {
                this.errorMessage = Resources.Error_Message_Header + "\n\n";
            }

            this.errorMessage += error + "\n";
        }

        /// <summary>
        /// Method to Customize InterventionsView Form
        /// </summary>   
        public void CustomizeInterventionsView()
        {
            // Define the border style of the form to a dialog box.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;
            this.Width = 1354;
            this.Height = 787;

            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Method to load InterventionStatus DWG
        /// </summary>
        /// <param name="data">data table</param>
        private void LoadInterventionStatusGridView(DataTable data)
        {
            // Setting the data source and table for data grid view to display the data
            FormatDataOrdersDataGridView(data);

            dgvInterventionStatus.DataSource = data;
            //dgvInterventionStatus.DataMember = data.TableName;
            dgvInterventionStatus.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        /// <summary>
        /// Method to load AllAtmsEditing DWG
        /// </summary>   
        public void LoadAtmsGridView()
        {
            DataTable data = this.atmService.GetAllAtms();

            dvgAllAtmsEditing.DataSource = data;

            dvgAllAtmsEditing.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        /// <summary>
        /// Method to load AllAtms DWG
        /// </summary>   
        public void LoadAllAtmsGridView()
        {
            //retrive datatable with all atms
            atmData = this.atmService.GetAllAtms();

            //assign data to DataView 
            atmDataView = atmData.DefaultView;
            //assign DataView to Datagridview 
            dgvAllAtms.DataSource = atmDataView;

            dgvAllAtms.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //Label total sum atms
            lblAtmsCount.Text = dgvAllAtms.RowCount.ToString();
        }

        /// <summary>
        /// Method to AtmUserEditng DWG
        /// </summary>   
        public void LoadAtmUsersGridView()
        {
            DataTable userData = this.userService.GetAllAtmUsers();

            dvgAllAtmUsersEditing.DataSource = userData;

            dvgAllAtmUsersEditing.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        /// <summary>
        /// Method to AtmTechniciansEditng DWG
        /// </summary>   
        public void LoadTechniciansGridView()
        {
            DataTable technicianData = this.userService.GetAllTechnicians();

            dvgAllTechniciansEditing.DataSource = technicianData;

            dvgAllTechniciansEditing.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        /// <summary>
        /// Method to format data to orders grid view
        /// </summary>
        /// <param name="data">data table</param>
        private DataTable FormatDataOrdersDataGridView(DataTable data)
        {
            // format DataTable to display the data in datagridview        

            data.Columns.Remove("Atm_user_id");
            //data.Columns.Remove("Order_ID");
            data.Columns.Remove("Order_date");

            //coluns index 0....7
            data.Columns["Order_ID"].ColumnName = "Nalog No.";
            data.Columns["Atm_name"].ColumnName = "Naziv";
            data.Columns["Atm_serial_number"].ColumnName = "Serijski broj";
            data.Columns["Atm_model"].ColumnName = "Model";
            data.Columns["Atm_address"].ColumnName = "Adresa";
            data.Columns["Atm_Location"].ColumnName = "Mjesto";
            data.Columns["Refill_type"].ColumnName = "Intervencija";
            data.Columns["Order_status_completed"].ColumnName = "Status";

            return data;
        }

        /// <summary>
        /// Form constructor
        /// </summary>
        private void InterventionsView_Load(object sender, EventArgs e)
        {
            DataTable data = this.orderService.GetAllOrdersByDate(currentDate);
            this.LoadInterventionStatusGridView(data);


            
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            //query DataView
            string outputInfo = "";
            string[] keyWords = txtSearchName.Text.Split(' ');

            foreach (string word in keyWords)
            {
                if (outputInfo.Length == 0)
                {
                    outputInfo = "(Atm_name LIKE '%" + word + "%' OR Atm_address LIKE '%" +
                        word + "%' OR Atm_location LIKE '%" + word + "%')";
                }
                else
                {
                    outputInfo += " AND (Atm_name LIKE '%" + word + "%' OR Atm_address LIKE '%" +
                        word + "%' OR Atm_location LIKE '%" + word + "%')";
                }
            }
            //Applies the filter to the DataView
            atmDataView.RowFilter = outputInfo;

        }

        private void btnAddAtmToInterventions_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvAllAtms.Rows)
            {
                if (item.Selected == true)
                {
                    int n = dgvAddAtmToIntervention.Rows.Add();
                    //dgvAddAtmToIntervention.Rows[n].Cells[5].Value = item.Cells[0].Value.ToString(); //id
                    dgvAddAtmToIntervention.Rows[n].Cells[0].Value = item.Cells[1].Value.ToString(); //naziv
                    dgvAddAtmToIntervention.Rows[n].Cells[1].Value = item.Cells[2].Value.ToString(); //ser.broj
                    dgvAddAtmToIntervention.Rows[n].Cells[2].Value = item.Cells[4].Value.ToString(); //adresa
                    dgvAddAtmToIntervention.Rows[n].Cells[3].Value = item.Cells[5].Value.ToString(); //lokacija
                    dgvAddAtmToIntervention.Rows[n].Cells[5].Value = item.Cells[0].Value.ToString();
                    dgvAddAtmToIntervention.Rows[n].Cells[6].Value = String.Empty;
                    dgvAddAtmToIntervention.Rows[n].Cells[7].Value = String.Empty;
                    dgvAddAtmToIntervention.Columns["Column6"].Visible = false;
                    dgvAddAtmToIntervention.Columns["Column7"].Visible = false;
                    dgvAddAtmToIntervention.Columns["Column8"].Visible = false;


                }
            }
        }

        private void dgvAddAtmToIntervention_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //if (this.dgvAddAtmToIntervention.IsCurrentCellDirty)
            //{
            //    // This fires the cell value changed handler below
            //    dgvAddAtmToIntervention.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //}
        }

        private void dgvAddAtmToIntervention_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        
          
        }

        private void dtpCalendar_ValueChanged(object sender, EventArgs e)
        {
            dateOfInter = dtpCalendar.Value.Date;
            //to do

        }

        private void dtpCalendarInterPerDate_ValueChanged(object sender, EventArgs e)
        {
            dateOfInterStatus = dtpCalendarInterPerDate.Value.Date;
 
            DataTable data = this.orderService.GetAllOrdersByDate(dateOfInterStatus);
            this.LoadInterventionStatusGridView(data);
        }

        private void btnSaveInterventions_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(dateOfInter.ToShortDateString());
            AtmModel atm = new AtmModel();
            //TO DO Rješit USERA DA SE LOGIRA , Globalnu variablu stavit 
            var userid = 1;
            AtmUserModel user = new AtmUserModel();
            user.Id = userid;
            //ako je punjenje

            foreach (DataGridViewRow item in dgvAddAtmToIntervention.Rows)
            {
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)item.Cells[4];


                if (cb.Value != null)
                {
                    atm.Id = Convert.ToInt32(item.Cells[5].Value);//id


                    switch (cb.Value.ToString())
                    {
                        case "Punjenje":
                            int a = Convert.ToInt32(item.Cells[6].Value);
                            int b = Convert.ToInt32(item.Cells[7].Value);
                            var flagRefill = this.orderService.AddRefillOrder(dateOfInter, atm, user, a, b);
                            break;
                        case "Greska":
                            var flagError = this.orderService.AddErrorOrder(dateOfInter, atm, user);
                            break;
                        case "Kvar":
                            var flagMal = this.orderService.AddMalfunctionOrder(dateOfInter, atm, user);
                            break;
                        case "Zadrzana kartica":
                            break;
                        default:
                            throw new ArgumentException("Nepoznata intervencija");
                    }

                }
                else
                {
                    MessageBox.Show("Odaberite intervenciju");
                }//end if 


            }


        }

        private void dgvAddAtmToIntervention_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        
        }

        private void dgvAddAtmToIntervention_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {


            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
           
            
              
        }


        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
            {
                double totPrice = 0;
                var comboBox = (DataGridViewComboBoxEditingControl)sender;
                int rowIndex = comboBox.EditingControlRowIndex;
                ComboBox cb = (ComboBox)sender;
            if (rowIndex != -1 && cb.SelectedItem.ToString() == "Punjenje")
            {
                
                NewRefillForm referentRefill = new NewRefillForm();
                DialogResult dr = referentRefill.ShowDialog();

                // the user then happily enters data
                String bill100 = "";
                String bill200 = "";
                if (dr == DialogResult.OK) //or whatever it is

                {
                    bill100 = referentRefill.txtReferentRefill100.Text;
                    bill200 = referentRefill.txtReferentRefill100.Text;
                }

                dgvAddAtmToIntervention.Rows[rowIndex].Cells[6].Value = bill100;
                dgvAddAtmToIntervention.Rows[rowIndex].Cells[7].Value = bill200;

                referentRefill.Dispose();
            }
            else 
            {
               
            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddNewAtm_Click(object sender, EventArgs e)
        {
            NewAtmForm frmNewAtm = new NewAtmForm(this);
            frmNewAtm.Show();
        }

        private void dvgAllAtmsEditing_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    string currentAtmId = dgv.SelectedRows[0].Cells[0].Value.ToString();
                    atmId = int.Parse(currentAtmId);

                    AtmModel atmData = this.atmService.GetAtmData(atmId);

                    txtAtmName.Text = atmData.Name.ToString();
                    txtAtmSerialNumber.Text = atmData.SerialNumber.ToString();
                    txtAtmModel.Text = atmData.Model.ToString();
                    txtAtmAddress.Text = atmData.Address.ToString();
                    txtAtmLocation.Text = atmData.Location.ToString();
                    txtAtmAccountingNumber.Text = atmData.AccountingNumber.ToString();
                    
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void btnUpdateAtmData_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateUpdate())
                {
                    AtmModel atmModel = new AtmModel()
                    {
                        Id = this.atmId,
                        Name = txtAtmName.Text.Trim(),
                        SerialNumber = txtAtmSerialNumber.Text.Trim(),
                        Model = txtAtmModel.Text.Trim(),
                        Address = txtAtmAddress.Text.Trim(),
                        Location = txtAtmLocation.Text.Trim(),
                        AccountingNumber = txtAtmAccountingNumber.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtAtmAccountingNumber.Text),
                        Status = "U funkciji",
                        
                    };
                    
                    var flag = this.atmService.UpdateAtm(atmModel);
                    
                    
                    if (flag)
                    {

                        this.LoadAtmsGridView();

                        MessageBox.Show(
                            Resources.Update_Successful_Message,
                            Resources.Update_Successful_Message_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                        this.errorMessage,
                        Resources.Update_Error_Message_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// Validates update data
        /// </summary>
        /// <returns>true or false</returns>
        private bool ValidateUpdate()
        {
            //this.errorMessage = string.Empty;

            //if (txt2Name.Text.Trim() == string.Empty)
            //{
            //    this.AddErrorMessage(Resources.Registration_Name_Required_Text);
            //}

            //if (cmb2Occupation.SelectedIndex == -1)
            //{
            //    this.AddErrorMessage(Resources.Registration_Occupation_Select_Text);
            //}

            //if (cmb2MaritalStatus.SelectedIndex == -1)
            //{
            //    this.AddErrorMessage(Resources.Registration_MaritalStatus_Select_Text);
            //}

            //if (cmb2HealthStatus.SelectedIndex == -1)
            //{
            //    this.AddErrorMessage(Resources.Registration_HealthStatus_Select_Text);
            //}

            //return this.errorMessage != string.Empty ? false : true;
            return true;
        }

        private void dvgAllAtmUsersEditing_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    string currentUserId = dgv.SelectedRows[0].Cells[0].Value.ToString();
                    userId = int.Parse(currentUserId);

                    AtmUserModel userData = this.userService.GetAtmUserData(userId);

                    txtAtmUserName.Text = userData.Name.ToString();
                    txtAtmUserSurname.Text = userData.Surname.ToString();
                    txtAtmUserContactNumber.Text = userData.ContactNumber.ToString();
                    txtAtmUserContactMail.Text = userData.Email.ToString();
                    txtAtmUserBranch.Text = userData.Branch.ToString();
                    txtAtmUserCompany.Text = userData.Company.ToString();
                    txtAtmUserWorkPlace.Text = userData.UserType.ToString();

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void BtnUpdateAtmUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateUpdate())
                {
                    AtmUserModel atmUser = new AtmUserModel()
                    {
                        Id = this.userId,
                        Name = txtAtmUserName.Text.Trim(),
                        Surname = txtAtmUserSurname.Text.Trim(),
                        ContactNumber = txtAtmUserContactNumber.Text.Trim(),
                        Email = txtAtmUserContactMail.Text.Trim(),
                        Branch = txtAtmUserBranch.Text.Trim(),
                        Company = txtAtmUserCompany.Text.Trim(),
                        UserType = txtAtmUserWorkPlace.Text.Trim(),

                    };

                    var flag = this.userService.UpdateAtmUser(atmUser);
                    
                    if (flag)
                    {

                    this.LoadAtmUsersGridView();

                    MessageBox.Show(
                        Resources.Update_Successful_Message,
                        Resources.Update_Successful_Message_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                        this.errorMessage,
                        Resources.Update_Error_Message_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void BtnChart_Click(object sender, EventArgs e)
        {
        


        }

        private void BtnInterventionDelete_Click(object sender, EventArgs e)
        {
            int row = dgvInterventionStatus.CurrentCell.RowIndex;
            //dgvInterventionStatus.Rows.RemoveAt(row);

            string currentOrderId = dgvInterventionStatus.SelectedRows[0].Cells[0].Value.ToString();
            orderId = int.Parse(currentOrderId);
            var flagInterDelete = this.orderService.DeleteOrder(orderId);

            DataTable data = this.orderService.GetAllOrdersByDate(dateOfInter);
            this.LoadInterventionStatusGridView(data);

        }

        private void BtnInterventionRefresh_Click(object sender, EventArgs e)
        {
            dtpCalendarInterPerDate.Value = dateOfInter;
            DataTable data = this.orderService.GetAllOrdersByDate(dateOfInter);
            this.LoadInterventionStatusGridView(data);
        }

        private void btnDeleteInterventions_Click(object sender, EventArgs e)
        {
            //Delete from datagridview
            dgvAddAtmToIntervention.Rows.Clear();
            dgvAddAtmToIntervention.Refresh();

            //Clear labels
            lblInterventionsTotal.Text = string.Empty;
            lblInterventionsRefill.Text = string.Empty;
            lblInterventionsErrors.Text = string.Empty;
            lblInterventionsMal.Text = string.Empty;

        }

        private void dgvAddAtmToIntervention_SelectionChanged(object sender, EventArgs e)
        {
            //Fill labels above datagrid
            //AddAtmToIntervention

            int refill = 0;
            int atmError = 0;
            int atmMal = 0;
            int atmInterTotal = 0;
            foreach (DataGridViewRow item in dgvAddAtmToIntervention.Rows)
            {
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)item.Cells[4];
                if (cb.Value != null)
                {
                    

                    switch (cb.Value.ToString())
                    {
                        case "Punjenje":
                            refill++;
                            break;
                        case "Greska":
                            atmError++;
                            break;
                        case "Kvar":
                            atmMal++;
                            break;
                        case "Zadrzana kartica":
                            break;
                        default:
                            throw new ArgumentException("Nepoznata intervencija");
                    }

                    atmInterTotal = refill + atmError + atmMal;


                }//end if 


            }

            lblInterventionsTotal.Text = atmInterTotal.ToString();
            lblInterventionsRefill.Text = refill.ToString();
            lblInterventionsErrors.Text = atmError.ToString();
            lblInterventionsMal.Text = atmMal.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
 

            chartAtmRefill.Series[0].XValueType = ChartValueType.DateTime;
            chartAtmRefill.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
            chartAtmRefill.ChartAreas[0].AxisX.Interval = 1;
            chartAtmRefill.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            chartAtmRefill.ChartAreas[0].AxisX.IntervalOffset = 1;

            //chartAtmRefill.Series[0].XValueType = ChartValueType.DateTime;
            DateTime minDate = new DateTime(2018, 03, 01).AddSeconds(-1);
            DateTime maxDate = DateTime.Now;    // or DateTime.Now;
            chartAtmRefill.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
            chartAtmRefill.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();
            chartAtmRefill.ChartAreas[0].AxisY.Maximum = 700000;


            AtmModel atm1 = new AtmModel();
            atm1.Id = 2;
            var currentAtmRefills = this.orderService.GetAllAtmRefillOrders(atm1);
           
           
            foreach (var item in currentAtmRefills)
            {
                this.chartAtmRefill.Series[0].Points.AddXY(item.OrderDate,item.RefillNewBill100*100);
                this.chartAtmRefill.Series[1].Points.AddXY(item.OrderDate, item.RefillNewBill200*200);
            }
           

        }

        private void BtnTechniciansReport_Click(object sender, EventArgs e)
        {
            //Reports are copying to bin directory 
            //Start report Reports/Technicians -> LocalReport.Clear() ;)
            //DataTable dont work wih reports
            //da bi dobio ModelBindingSource moraš zakacit u designeru report za reportviewer!!

            rVReports.LocalReport.DataSources.Clear();
            rVReports.LocalReport.ReportPath = "Reports/TechniciansReport.rdlc";
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.TechnicianModelBindingSource;
            this.rVReports.LocalReport.DataSources.Add(reportDataSource1);

            
            var technicianDataSource = new UserService();
            var technicians = technicianDataSource.GetAllTechniciansList();
            
            this.TechnicianModelBindingSource.DataSource = technicians;
            this.rVReports.RefreshReport();


        }

        private void BtnAllAtmsReport_Click(object sender, EventArgs e)
        {

            this.rVReports.LocalReport.DataSources.Clear();
            rVReports.Reset(); 
            rVReports.LocalReport.ReportPath = "Reports/AllAtmsReport.rdlc";
            var allAtmsDataSource = new AtmService();
            var allAtms = allAtmsDataSource.GetAllAtmsList();
            Microsoft.Reporting.WinForms.ReportDataSource rprtDTSource = new Microsoft.Reporting.WinForms.ReportDataSource();
            rprtDTSource.Name = "DataSet1";
            rprtDTSource.Value = this.AtmModelBindingSource;
            this.rVReports.LocalReport.DataSources.Add(rprtDTSource);
            this.AtmModelBindingSource.DataSource = allAtms;
            this.rVReports.RefreshReport();

        }

        private void BtnAll_Click(object sender, EventArgs e)
        {


            this.rVReports.LocalReport.DataSources.Clear();
            rVReports.Reset();
            rVReports.LocalReport.ReportPath = "Reports/AllErrorsReport.rdlc";
           
            var allErrorsDataSource = new OrderService();
            var allErrors = allErrorsDataSource.GetAllErrorOrders();
            Microsoft.Reporting.WinForms.ReportDataSource rprtDTSource = new Microsoft.Reporting.WinForms.ReportDataSource();
            rprtDTSource.Name = "DataSet1";
            rprtDTSource.Value = this.ErrorReportModelBindingSource;
            this.rVReports.LocalReport.DataSources.Add(rprtDTSource);
            this.ErrorReportModelBindingSource.DataSource = allErrors;
            this.rVReports.RefreshReport();



        }

        private void BtnAllMalfunctionsReport_Click(object sender, EventArgs e)
        {

            this.rVReports.LocalReport.DataSources.Clear();
            rVReports.Reset();
            rVReports.LocalReport.ReportPath = "Reports/AllMalfunctionsReport.rdlc";

            var allMalfunctionsDataSource = new OrderService();
            var allMalfunctions = allMalfunctionsDataSource.GetAllMalfunctionsList();
            Microsoft.Reporting.WinForms.ReportDataSource rprtDTSource = new Microsoft.Reporting.WinForms.ReportDataSource();
            rprtDTSource.Name = "DataSet1";
            rprtDTSource.Value = this.MalfunctionReportModelBindingSource;
            this.rVReports.LocalReport.DataSources.Add(rprtDTSource);
            this.MalfunctionReportModelBindingSource.DataSource = allMalfunctions;
            this.rVReports.RefreshReport();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        //InterventionsStatus
                        DataTable data = this.orderService.GetAllOrdersByDate(currentDate);
                        this.LoadInterventionStatusGridView(data);
                        break;
                    case 1:
                        //
                        break;
                    case 2:
                        //InterventionsToAdd
                        this.LoadAllAtmsGridView();
                        break;
                    case 3:
                        //Administration of Atms
                        this.LoadAtmsGridView();
                        break;
                    case 4:
                        //Administration Users and Technicians
                        //Load AtmUsersGridView
                        this.LoadAtmUsersGridView();
                        //Load Technicians
                        this.LoadTechniciansGridView();
                        break;
                    case 5:
                        //Load reports
                        this.rVReports.RefreshReport();
                        break;
                    default:
                        throw new ArgumentException("Nepoznat tab");
                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void dvgAllTechniciansEditing_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    string currentUserId = dgv.SelectedRows[0].Cells[0].Value.ToString();
                    userId = int.Parse(currentUserId);

                    TechnicianModel userData = this.userService.GetTechnicianData(userId);

                    txtTechnicianName.Text = userData.Name.ToString();
                    txtTechnicianSurname.Text = userData.Surname.ToString();
                    txtTechnicianContactNumber.Text = userData.ContactNumber.ToString();
                    txtTechnicianContactMail.Text = userData.Email.ToString();
                    txtTechnicianCompany.Text = userData.Company.ToString();

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void BtnUpdateTechnician_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateUpdate())
                {
                    TechnicianModel technician = new TechnicianModel()
                    {
                        Id = this.userId,
                        Name = txtTechnicianName.Text.Trim(),
                        Surname = txtTechnicianSurname.Text.Trim(),
                        ContactNumber = txtTechnicianContactNumber.Text.Trim(),
                        Email = txtTechnicianContactMail.Text.Trim(),
                        Company = txtTechnicianCompany.Text.Trim(),
                        

                    };

                    var flag = this.userService.UpdateTechnician(technician);

                    if (flag)
                    {

                        this.LoadTechniciansGridView();

                        MessageBox.Show(
                            Resources.Update_Successful_Message,
                            Resources.Update_Successful_Message_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                        this.errorMessage,
                        Resources.Update_Error_Message_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            int row = dvgAllAtmUsersEditing.CurrentCell.RowIndex;
            
            string currentUserId = dvgAllAtmUsersEditing.SelectedRows[0].Cells[0].Value.ToString();
            int userId = int.Parse(currentUserId);
            var flagUserDelete = this.userService.DeleteAtmUser(userId);

             this.LoadAtmUsersGridView();
        }

        private void BtnDeleteAtm_Click(object sender, EventArgs e)
        {
            int row = dvgAllAtmsEditing.CurrentCell.RowIndex;

            string currentAtmId = dvgAllAtmsEditing.SelectedRows[0].Cells[0].Value.ToString();
            int atmId = int.Parse(currentAtmId);
            var flagAtmDelete = this.atmService.DeleteAtm(atmId);

            this.LoadAtmsGridView();
        }

        private void BtnDeleteTechnician_Click(object sender, EventArgs e)
        {
            int row = dvgAllTechniciansEditing.CurrentCell.RowIndex;

            string currentTechincianId = dvgAllTechniciansEditing.SelectedRows[0].Cells[0].Value.ToString();
            int techincianId = int.Parse(currentTechincianId);
            var flagTechnicianDelete = this.userService.DeleteTechnician(techincianId);

            this.LoadTechniciansGridView();
        }

        private void BtnAddNewUser_Click(object sender, EventArgs e)
        {
            NewAtmUserForm frmNewAtmUser = new NewAtmUserForm(this);
            frmNewAtmUser.Show();
        }

        private void BtnAddNewTechnician_Click(object sender, EventArgs e)
        {
            NewTechnicianForm frmNewTechnician = new NewTechnicianForm(this);
            frmNewTechnician.Show();
        }

        private void btnSplineTest_Click(object sender, EventArgs e)
        {

            chartAtmRefillTrend.ResetText();
           


            //chartAtmRefill.Series[0].XValueType = ChartValueType.DateTime;
            chartAtmRefillTrend.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
            chartAtmRefillTrend.ChartAreas[0].AxisX.Interval = 1;
            chartAtmRefillTrend.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
            chartAtmRefillTrend.ChartAreas[0].AxisX.IntervalOffset = 1;

            //chartAtmRefill.Series[0].XValueType = ChartValueType.DateTime;
            DateTime minDate = new DateTime(2018, 03, 01).AddSeconds(-1);
            DateTime maxDate = DateTime.Now;    // or DateTime.Now;
            chartAtmRefillTrend.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
            chartAtmRefillTrend.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();
            chartAtmRefillTrend.ChartAreas[0].AxisY.Maximum = 700000;


            AtmModel atm1 = new AtmModel();
            atm1.Id = 2;
            var currentAtmRefills = this.orderService.GetAllAtmRefillOrders(atm1);


            foreach (var item in currentAtmRefills)
            {
                this.chartAtmRefillTrend.Series[0].Points.AddXY(item.OrderDate, (item.RefillNewBill100 * 100)+(item.RefillNewBill200 * 200));
              
            }

        }


    }
}
