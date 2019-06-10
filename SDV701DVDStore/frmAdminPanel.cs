using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminPanel
{
    public partial class frmAdminPanel : Form
    {
        public frmAdminPanel()
        {
            InitializeComponent();
        }

        public static readonly frmAdminPanel _Instance = new frmAdminPanel();
        public frmViewOrder _ViewOrder = new frmViewOrder();
        private clsOrder _Order = new clsOrder();

        private frmCategory _NewProductForm = new frmCategory();
        public async void UpdateDisplay()
        {
            try
            {
                lstProducts.DataSource = null;
                lstProducts.DataSource = await ServiceClient.GetCategoryListAsync();
                lstOrders.DataSource = null;
                _Order.OrderList = await ServiceClient.GetOrderListAsync();
                lstOrders.DataSource = _Order.OrderList;

                decimal lcTotal = 0;
                foreach (clsOrder lcOrder in _Order.OrderList)
                {
                    lcTotal += lcOrder.PricePerItem * lcOrder.Quanity;
                }
                lblTotal.Text = lcTotal.ToString("C");
            }
            catch
            {
                MessageBox.Show("Error??");
            }
        }

        

        private void frmAdminPanel_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void lstProducts_DoubleClick(object sender, EventArgs e)
        {
           // _NewProductForm.Run(lstProducts.SelectedItem as string);

            string lcKey;

            lcKey = Convert.ToString(lstProducts.SelectedItem);
            if (lcKey != null)
            {
                try
                {
                    
                    frmCategory.Run(lstProducts.SelectedItem as string);
                
                }
                catch (Exception)
                {

                }

                             

            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {

            _ViewOrder.showOrderInfo(lstOrders.SelectedItem as clsOrder);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult lcResult = MessageBox.Show("Are You Sure You Want To Delete This Order?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (lcResult == DialogResult.Yes)
            {
                MessageBox.Show(await ServiceClient.DeleteOrderAsync(lstOrders.SelectedItem as clsOrder));
                
                UpdateDisplay();
            }
        }
    }

    
}
