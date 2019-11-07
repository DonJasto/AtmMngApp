using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtmTeller.Data.BusinessService;
using AtmTeller.Data.DataModel;

namespace ATMTellerApp.Forms
{
    public partial class InterventionsView : Form
    {
        /// <summary>
        /// Interface of OrderService
        /// </summary>
        IOrderService orderService;
        IAtmService atmService;
        DateTime date = new DateTime(2018, 2, 2);
       

        /// <summary>
        /// Order id
        /// </summary>
        private int orderId;

        

        public InterventionsView()
        {
            InitializeComponent();
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

            orderService = new OrderService();
            atmService = new AtmService();
        }

        /// <summary>
        /// Method to load data grid view
        /// </summary>
        /// <param name="data">data table</param>
        private void LoadDataGridView(DataTable data)
        {
            // Setting the data source and table for data grid view to display the data
            FormatDataOrdersDataGridView(data);
            dataGridView1.DataSource = data;
            dataGridView1.DataMember = data.TableName;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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


        private void InterventionsView_Load(object sender, EventArgs e)
        {

            DataTable data = this.orderService.GetAllOrdersByDate(date);

            //var data = this.orderService.GetAllOrdersByDate(date);
            //label1.Text = data.Rows[0]["Order_date"].ToString();
            this.LoadDataGridView(data);

            this.cmbReplaceComponentMal.SelectedIndex = 0;
            //MessageBox.Show(this.cmbReplaceComponentMal.SelectedIndex.ToString());


            dataGridView1.DataSource = data;
            //dataGridView1.DataMember = data.TableName;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        private void Punjenje_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            MalfunctionReportModel malfunctionReport = this.orderService.GetMalfunctionReportByOrderId(orderId);

            int atmId = malfunctionReport.ATM.Id;
            DataTable atmDataTable = this.atmService.GetComponentByAtmId(atmId);


            foreach (DataRow row in atmDataTable.Rows)
            {
                //if (cmbCompanyMal.SelectedItem != null)
                //{
                    string comp = cmbComponentsMal.SelectedItem.ToString();



                    if (row["Component_name"].ToString().Contains(comp))
                    {
                        txtSerialComponentMal.Text = row["Component_serial_number"].ToString();
                        txtDescriptionComponentMal.Text = row["Component_description"].ToString();
                    }
                //}
            }//end for each
        }

        private void groupBox14_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //TO DO
            cmbComponentsMal.Enabled = false;
            txtSerialComponentMal.Enabled = false;
            txtDescriptionComponentMal.Enabled = false;


            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            label2.Text = dataGridView1[6, dataGridView1.SelectedRows[0].Index].Value.ToString();


            if (dataGridView1[6, dataGridView1.SelectedRows[0].Index].Value.ToString().Trim() == "Kvar")
            {
                tabControl1.SelectedTab = tabPage5;

                MalfunctionReportModel malfunctionReport = new MalfunctionReportModel();

                try
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        //collect order id from datagridview
                        string malfunctionOrderId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        orderId = int.Parse(malfunctionOrderId);

                        malfunctionReport = this.orderService.GetMalfunctionReportByOrderId(orderId);

                        lblAtmNameMal.Text = malfunctionReport.ATM.Name.ToString();
                        lblAtmModelMal.Text = malfunctionReport.ATM.Model.ToString();
                        lblAtmSerialMal.Text = malfunctionReport.ATM.SerialNumber.ToString();
                        lblAtmAddressMal.Text = malfunctionReport.ATM.Address.ToString();
                        lblAtmLocationMal.Text = malfunctionReport.ATM.Location.ToString();

                        lblOrderDateMal.Text = malfunctionReport.OrderDate.ToString();
                        txtMalTime.Text = malfunctionReport.MalfunctionTime.ToString("hh:mm:ss");
                        txtMalTimeEnd.Text = malfunctionReport.MalfunctionTimeEnd.ToString("hh:mm:ss");

                        txtStateCurrentMal.Text = malfunctionReport.StateCurrent.ToString();
                        txtActionsTakenMal.Text = malfunctionReport.ActionsTaken.ToString();
                        txtStateAfterMal.Text = malfunctionReport.StateAfter.ToString();

                        txtSpareMaterialMal.Text = malfunctionReport.SpareMaterial.ToString();



                    }
                }
                catch (Exception)
                {

                    throw;
                }




            }
            else if(dataGridView1[6, dataGridView1.SelectedRows[0].Index].Value.ToString().Trim() == "Punjenje")
            {
                tabControl1.SelectedTab = tabPage1;
                DataGridView dvg = (DataGridView)sender;

                RefillModel refillReport = new RefillModel();

                try
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {   
                        //Collect order id from datagridview
                        string refillOrderId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        orderId = int.Parse(refillOrderId);

                        //Return Refill from database
                        refillReport = this.orderService.GetRefillReportByOrderId(orderId);

                        //Fill InterventionsView
                        lblAtmName.Text = refillReport.ATM.Name.ToString();
                        lblAtmModel.Text = refillReport.ATM.Model.ToString();

                        //Calculate total sum
                        int newBill100 = refillReport.RefillNewBill100;
                        int newBill200 = refillReport.RefillNewBill200;
                        int totalCash = newBill100 * 100 + newBill200 * 200;

                        //Fill InterventionsView
                        lblAtmTotalRefill.Text = totalCash.ToString();

                        lblAtmAccountingNumber.Text = refillReport.ATM.AccountingNumber.ToString();
                        lblAtmAddress.Text = refillReport.ATM.Address.ToString();
                        lblAtmLocation.Text = refillReport.ATM.Location.ToString();

                        lblAtmRefillNewBill100.Text = refillReport.RefillNewBill100.ToString();
                        lblAtmRefillNewBill200.Text = refillReport.RefillNewBill200.ToString();

                        lblOrderDate.Text = refillReport.OrderDate.ToString();
                        txtRefillTime.Text = refillReport.RefillTime.ToString("hh:mm:ss");
                        txtRefillTimeEnd.Text = refillReport.RefillTimeEnd.ToString("hh:mm:ss");

                        lblAtmRefillNewBill100.Text = refillReport.RefillNewBill100.ToString();
                        lblAtmRefillNewBill200.Text = refillReport.RefillNewBill200.ToString();

                        txtAtmRefillNewBill100.Text = refillReport.RefillNewBill100.ToString();
                        txtAtmRefillNewBill200.Text = refillReport.RefillNewBill200.ToString();

                        txtAtmRefillLocalBill100.Text = refillReport.ReturnLocalBill100.ToString();
                        txtAtmRefillLocalBill200.Text = refillReport.ReturnLocalBill200.ToString();
                        txtAtmRefillServerBill100.Text = refillReport.ReturnServerBill100.ToString();
                        txtAtmRefillServerBill200.Text = refillReport.ReturnServerBill200.ToString();

                        txtAtmCashBill100.Text = refillReport.CashBill100.ToString();
                        txtAtmCashBill200.Text = refillReport.CashBill200.ToString();

                        //calculate do atm has more or less cass that it suppose to 
                        int serverCash100 = refillReport.ReturnServerBill100;
                        int returnedCash100 = refillReport.CashBill100;
                        
                        int serverCash200 = refillReport.ReturnServerBill200;
                        int returnedCash200 = refillReport.CashBill200;
                        int diff100 = serverCash100 - returnedCash100;
                        int diff200 = serverCash200 - returnedCash200;

                        //Fill InterventionsView
                        txtServerCashDifference100.Text = diff100.ToString();
                        txtServerCashDifference200.Text = diff200.ToString();


                    }
                }
                catch (Exception)
                {

                    throw;
                }




            }
            else if (dataGridView1[6, dataGridView1.SelectedRows[0].Index].Value.ToString().Trim() == "Greska")
            {
                tabControl1.SelectedTab = tabPage2;

                DataGridView dvg = (DataGridView)sender;

                ErrorReportModel errorReport = new ErrorReportModel();

                try
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        //collect order id from datagridview
                        string errorOrderId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        orderId = int.Parse(errorOrderId);
                        
                        errorReport = this.orderService.GetErrorReportByOrderId(orderId);

                        lblAtmNameError.Text = errorReport.ATM.Name.ToString();
                        lblAtmModelError.Text = errorReport.ATM.Model.ToString();
                        lblAtmSerialError.Text = errorReport.ATM.SerialNumber.ToString();
                        lblAtmAddressError.Text = errorReport.ATM.Address.ToString();
                        lblAtmLocationError.Text = errorReport.ATM.Location.ToString();

                        lblOrderDateError.Text = errorReport.OrderDate.ToString();
                        txtErrorTime.Text = errorReport.ErrorTime.ToString("hh:mm:ss");
                        txtErrorTimeEnd.Text = errorReport.ErrorTimeEnd.ToString("hh:mm:ss");

                        txtStateCurrentError.Text = errorReport.StateCurrent.ToString();
                        txtActionsTakenError.Text = errorReport.ActionsTaken.ToString();
                        txtStateAfterError.Text = errorReport.StateAfter.ToString();




                    }
                }
                catch (Exception)
                {

                    throw;
                }


            }
            else if (dataGridView1[6, dataGridView1.SelectedRows[0].Index].Value.ToString().Trim() == "Zadržana kartica")
            {
                tabControl1.SelectedTab = tabPage3;
            }



        }

        private void cmbCompanyMal_SelectedIndexChanged(object sender, EventArgs e)
        {
            string companyName = "";

            if (cmbReplaceComponentMal.SelectedIndex != -1)
            {
                this.cmbTechnicianMal.DataSource = null;
                cmbTechnicianMal.Items.Clear();

                companyName = cmbCompanyMal.SelectedItem.ToString();

                List<TechnicianModel> techniciansData = new List<TechnicianModel>();
            
                techniciansData = this.orderService.GetAllTechniciansByCompanyName(companyName);

                //for (int i = 0; i < techniciansData.Rows.Count; i++)
                //{

                //    cmbTechnicianMal.Items.Add((string)techniciansData.Rows[i]["Name"]+(string)techniciansData.Rows[i]["Surname"]);
                //}

                cmbTechnicianMal.DataSource = techniciansData; //the data table which contains data
                cmbTechnicianMal.ValueMember = "Id";   // column name which you want in SelectedValue
                cmbTechnicianMal.DisplayMember = "Name"; // column name that you need to display as text

            }

        }

        private void cmbReplaceComponentMal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReplaceComponentMal.SelectedIndex == 1)
            {
                cmbComponentsMal.Enabled = true;
                txtSerialComponentMal.Enabled = true;
                txtDescriptionComponentMal.Enabled = true;

            }
        }

        private void btnSaveRefillReport_Click(object sender, EventArgs e)
        {
            //TO DO VALIDATE AND ERROR MSGBOXES 
            RefillModel refillModel = new RefillModel();
            refillModel.Id = orderId;
            refillModel.RefillTime = Convert.ToDateTime(txtRefillTime.Text);
            refillModel.RefillTimeEnd = Convert.ToDateTime(txtRefillTimeEnd.Text);
            refillModel.RefillNewBill100 = Convert.ToInt16(txtAtmRefillNewBill100.Text);
            refillModel.RefillNewBill200 = Convert.ToInt16(txtAtmRefillNewBill200.Text);
            refillModel.ReturnLocalBill100 = Convert.ToInt16(txtAtmRefillLocalBill100.Text);   // ovo triba prominit krivo san napisa ime textboxa
            refillModel.ReturnLocalBill200 = Convert.ToInt16(txtAtmRefillLocalBill200.Text);   // ovo triba prominit krivo san napisa ime textboxa
            refillModel.ReturnServerBill100 = Convert.ToInt16(txtAtmRefillServerBill100.Text); // i  ovo triba prominit krivo san napisa ime textboxa
            refillModel.ReturnServerBill200 = Convert.ToInt16(txtAtmRefillServerBill200.Text); // i ovo triba prominit krivo san napisa ime textboxa
            refillModel.CashBill100 = Convert.ToInt16(txtAtmCashBill100.Text);
            refillModel.CashBill200 = Convert.ToInt16(txtAtmCashBill200.Text);
            refillModel.StatusCompleted = "Y";

            // TO DO Spremi listice 

            var flag = this.orderService.UpdateRefillOrder(refillModel);

            lblAtmRefillNewBill100.Text = refillModel.RefillNewBill100.ToString();
            lblAtmRefillNewBill200.Text = refillModel.RefillNewBill200.ToString();

            var data = this.orderService.GetAllOrdersByDate(date);
            this.LoadDataGridView(data);
        }

        private void btnSaveErrorReport_Click(object sender, EventArgs e)
        {
            ErrorReportModel errorReportModel = new ErrorReportModel();
            errorReportModel.Id = orderId;
            errorReportModel.ErrorTime = Convert.ToDateTime(txtErrorTime.Text); 
            errorReportModel.ErrorTimeEnd = Convert.ToDateTime(txtErrorTimeEnd.Text);

            errorReportModel.StateCurrent = txtStateCurrentError.Text;
            errorReportModel.ActionsTaken = txtActionsTakenError.Text;
            errorReportModel.StateAfter = txtStateAfterError.Text;

            errorReportModel.StatusCompleted = "Y";

            var flag = this.orderService.UpdateErrorOrder(errorReportModel);

            DataTable data = this.orderService.GetAllOrdersByDate(date);
            this.LoadDataGridView(data);

        }

        private void btnMalfunctionReport_Click(object sender, EventArgs e)
        {
            MalfunctionReportModel malfunctionReportModel = new MalfunctionReportModel();

            //NE ZABORAVI GA INICIALIZIRAT jer ce izbacit gresku 
            //DataRow selectedDataRow = ((DataRowView)cmbTechnicianMal.SelectedItem).Row;
            //int technicianId = Convert.ToInt32(selectedDataRow["Tehnician_ID"]);
            TechnicianModel tech = new TechnicianModel();
            
            if (cmbTechnicianMal.SelectedItem != null)
            {
                tech = (TechnicianModel)cmbTechnicianMal.SelectedItem;
            }
            //int technicianId = selectedDataRow.Id;
            int technicianId = tech.Id;
            //label84.Text = technicianId.ToString();

            malfunctionReportModel.Id = orderId;
            malfunctionReportModel.MalfunctionTime = Convert.ToDateTime(txtMalTime.Text);
            malfunctionReportModel.MalfunctionTimeEnd = Convert.ToDateTime(txtMalTimeEnd.Text);
            malfunctionReportModel.StateCurrent = txtStateCurrentMal.Text;
            malfunctionReportModel.ActionsTaken = txtActionsTakenMal.Text;
            malfunctionReportModel.StateAfter = txtStateAfterMal.Text;

            malfunctionReportModel.StatusCompleted = "Y";
            malfunctionReportModel.SpareMaterial = txtSpareMaterialMal.Text;
            TechnicianModel technician = new TechnicianModel();
            technician.Id = technicianId;
            malfunctionReportModel.Technician = technician;

            //find id of ATM , send Atm through UpdateAtmComponent 
            var malfunctionReport = this.orderService.GetMalfunctionReportByOrderId(orderId);

            //int atmId = malfunctionReport.ATM.Id;

            //ovo mi vraca false vidit zasto 
            //vraca false jer je stavljeno no count u proceduri
            var flag = this.orderService.UpdateMalfunctionOrder(malfunctionReportModel);

            AtmComponentModel atmComponent = new AtmComponentModel();
            if (cmbComponentsMal.SelectedIndex > -1)
            {
                atmComponent.SerialNumber = txtSerialComponentMal.Text;
                atmComponent.Description = txtDescriptionComponentMal.Text;
                atmComponent.Name = cmbComponentsMal.SelectedItem.ToString();
                atmComponent.Status = "Y";

                var flagMal = this.atmService.UpdateAtmComponent(malfunctionReport.ATM , atmComponent);
            }

            DataTable data = this.orderService.GetAllOrdersByDate(date);
            this.LoadDataGridView(data);


        }

        private void btnRegisterNewMalfunctionOrder_Click(object sender, EventArgs e)
        {
            //TO DO Rješit USERA DA SE LOGIRA , Globalnu variablu stavit 
            //Login
            var userid = 1;
            AtmUserModel user = new AtmUserModel();
            user.Id = userid;
            DateTime date = new DateTime(2018, 1, 1);
            AtmModel atm = this.atmService.GetAtmByOrderId(orderId);
            var flag = this.orderService.AddMalfunctionOrder(date,atm,user);

            DataTable data = this.orderService.GetAllOrdersByDate(date);
            this.LoadDataGridView(data);
        }
    }
}
