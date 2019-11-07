using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMReferent.Desktop.Properties;
using System.Windows.Forms;
using AtmTeller.Data.BusinessService;
using AtmTeller.Data.DataModel;

namespace ATMReferent.Desktop
{
    public partial class NewAtmForm : Form
    {
        /// <summary>
        /// Interface of AtmService
        /// </summary>
        
        IAtmService atmService;
        DataTable atmData;
        DataView atmDataView;
        InterventionsView _parentForm;

        /// <summary>
        /// Variable to store error message
        /// </summary>
        private string errorMessage;

        public NewAtmForm(InterventionsView parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
            atmService = new AtmService();

            //Eventhandler for updating parent form 
            //this.FormClosing += System.Windows.Forms.FormClosingEventHandler(this.NewAtmFormFormClosing);
        }
        //private void NewAtmFormFormClosing(object sender, FormClosingEventArgs e)
        

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

        private void AddNewAtm_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateUpdate())
                {
                    AtmModel atmModel = new AtmModel();
                   

                        atmModel.Name = txtNewAtmName.Text.Trim();
                        atmModel.SerialNumber = txtNewAtmSerialNumber.Text.Trim();
                        atmModel.Model = txtNewAtmModel.Text.Trim();
                        atmModel.Address = txtNewAtmAddress.Text.Trim();
                        atmModel.Location = txtNewAtmLocation.Text.Trim();
                        atmModel.AccountingNumber = txtNewAtmAccountingNumber.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtNewAtmAccountingNumber.Text);
                        atmModel.Status = "U funkciji";
                        
                    
                    //Add new Atm and return id
                    int atmId = this.atmService.AddAtm(atmModel);
                    atmModel.Id = atmId;

                    //Read from Form
                    var cardReader = new AtmComponentModel();
                        cardReader.SerialNumber = txtNewCardReaderSerialNumber.Text.Trim();
                        cardReader.Description = txtNewCardReaderDescription.Text.Trim();
                        cardReader.Status = "Y";
                    cardReader.Name = "Card Reader";
                    var receiptPrinter = new AtmComponentModel();
                        receiptPrinter.SerialNumber = txtNewPrinterSerialNumber.Text.Trim();
                        receiptPrinter.Description = txtNewPrinterDescription.Text.Trim();
                        receiptPrinter.Status = "Y";
                    receiptPrinter.Name = "Receipt Printer";
                    var dispenser = new AtmComponentModel();
                        dispenser.SerialNumber = txtNewDispenserSerialNumber.Text.Trim();
                        dispenser.Description = txtNewDispenserDescription.Text.Trim();
                        dispenser.Status = "Y";
                    dispenser.Name = "Dispenser";
                    var display = new AtmComponentModel();
                        display.SerialNumber = txtNewDisplaySerialNumber.Text.Trim();
                        display.Description = txtNewDisplayDescription.Text.Trim();
                        display.Status = "Y";
                        display.Name = "Display";
                    var keyboard = new AtmComponentModel();
                        keyboard.SerialNumber = txtNewKeyboardSerialNumber.Text.Trim();
                        keyboard.Description = txtNewKeyboardDescription.Text.Trim();
                        keyboard.Status = "Y";
                    keyboard.Name = "Keyboard";
                    var cpu = new AtmComponentModel();
                        cpu.SerialNumber = txtNewCPUSerialNumber.Text.Trim();
                        cpu.Description = txtNewCPUDescription.Text.Trim();
                        cpu.Status = "Y";
                    cpu.Name = "Cpu";
                    var alarm = new AtmComponentModel();
                        alarm.SerialNumber = txtNewAlarmSerialNumber.Text.Trim();
                        alarm.Description = txtNewAlarmDescription.Text.Trim();
                        alarm.Status = "Y";
                    alarm.Name = "Alarm";
                    var networkInterface = new AtmComponentModel();
                        networkInterface.SerialNumber = txtNewNetworkInterfaceSerialNumber.Text.Trim();
                        networkInterface.Description = txtNetworkInterfaceDescription.Text.Trim();
                        networkInterface.Status = "Y";
                    networkInterface.Name = "networkInterface";

                    //Add components to ATMModel
                    atmModel.components.Add(cardReader);
                    atmModel.components.Add(receiptPrinter);
                    atmModel.components.Add(dispenser);
                    atmModel.components.Add(display);
                    atmModel.components.Add(keyboard);
                    atmModel.components.Add(cpu);
                    atmModel.components.Add(alarm);
                    atmModel.components.Add(networkInterface);

                    //Save components to atm
                    var flag = this.atmService.AddAtmComponents(atmModel);

                    this.Close();

                    if (flag)
                    {



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
                MessageBox.Show(ex.GetBaseException().ToString(), "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ShowErrorMessage(ex);
            }
        }

        private void AddNewAtmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewAtmForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parentForm.LoadAtmsGridView();
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


    }
}
