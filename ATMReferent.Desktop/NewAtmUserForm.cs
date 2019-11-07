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
    public partial class NewAtmUserForm : Form
    {
        /// <summary>
        /// Interface of AtmUser
        /// </summary>
        IUserService userService;
        InterventionsView _parentForm;

        /// <summary>
        /// Variable to store error message
        /// </summary>
        private string errorMessage;

        public NewAtmUserForm(InterventionsView parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
            userService = new UserService();
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

        private void BtnAddNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateUpdate())
                {
                    AtmUserModel userModel = new AtmUserModel();


                    userModel.Name = txtNewAtmUserName.Text.Trim();
                    userModel.Surname = txtNewAtmUserSurname.Text.Trim();
                    userModel.ContactNumber = txtNewAtmUserContactNumber.Text.Trim();
                    userModel.Email = txtNewAtmUserContactMail.Text.Trim();
                    userModel.Branch = txtNewAtmUserBranch.Text.Trim();
                    userModel.Company = txtNewAtmUserCompany.Text.Trim();
                    userModel.UserName = txtNewAtmUserUsername.Text.Trim();
                    userModel.UserPassword = txtNewAtmUserPassword.Text.Trim();
                    userModel.UserType = txtNewAtmUserWorkplace.Text.Trim();

                    //Add new AtmUser and return bool
                    var flag = this.userService.AddAtmUser(userModel);

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

        private void BtnAddNewUserClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewAtmUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parentForm.LoadAtmUsersGridView();
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
