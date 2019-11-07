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
    public partial class Test : Form
    {
        IOrderService orderService;
        DateTime date = new DateTime(2018, 1, 1);
        public Test()
        {
            InitializeComponent();

            orderService = new OrderService();
            
            //DataTable data = this.orderService.GetAllOrdersByDate(date);
            //label1.Text = data.Rows[0]["Order_date"].ToString();
            //this.LoadDataGridView(data);
        }

        /// <summary>
        /// Method to load data grid view
        /// </summary>
        /// <param name="data">data table</param>
        private void LoadDataGridView(DataTable data)
        {
            // Setting the data source and table for data grid view to display the data
            FormatDataOrdersDataGridView(data);
            dataGridView5.DataSource = data;
            dataGridView5.DataMember = data.TableName;
            dataGridView5.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        /// <summary>
        /// Method to format data to orders grid view
        /// </summary>
        /// <param name="data">data table</param>
        private DataTable FormatDataOrdersDataGridView(DataTable data)
        {
            // format DataTable to display the data in datagridview        
            
            data.Columns.Remove("Atm_user_id");
            data.Columns.Remove("Order_ID");
            data.Columns.Remove("Order_date");


            data.Columns["Atm_name"].ColumnName = "Naziv";
            data.Columns["Atm_serial_number"].ColumnName = "Serijski broj";
            data.Columns["Atm_model"].ColumnName = "Model";
            data.Columns["Atm_address"].ColumnName = "Adresa";
            data.Columns["Atm_Location"].ColumnName = "Mjesto";
            data.Columns["Refill_type"].ColumnName = "Intervencija";
            data.Columns["Order_status_completed"].ColumnName = "Status";

            return data;
        }
    }
}
