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
    public partial class frmViewOrder : Form
    {
        public frmViewOrder()
        {
            InitializeComponent();
        }

        private clsOrder _Order = new clsOrder();

        public async void showOrderInfo(clsOrder lcOrder)
        {
            _Order = await ServiceClient.GetOrderInfoAsync(lcOrder.OrderNumber);
            updateDisplay();
            Show();
        }

        private void updateDisplay()
        {
            lblOrderNoValue.Text = _Order.OrderNumber.ToString();
            lblNameValue.Text = _Order.Name.ToString();
            lblAddressValue.Text = _Order.Address.ToString();
            lblPhoneValue.Text = _Order.PhoneNumber.ToString();
            lblProductNameValue.Text = _Order.ProductName.ToString();
            lblQuanityValue.Text = _Order.Quanity.ToString();
            lblPricePerItemValue.Text = _Order.PricePerItem.ToString();
            lblTotalCost.Text = Convert.ToString(_Order.PricePerItem * _Order.Quanity);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
