using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCustomerPanel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pgCheckout : Page
    {

        clsProducts _Product = new clsProducts();
        clsOrder _Order = new clsOrder();
        public pgCheckout()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                //lcArtistName = _Artist.Name;

                base.OnNavigatedTo(e);
                if (e.Parameter != null)
                {
                     _Product = e.Parameter as clsProducts;
                    
                    updateDisplay();
                }
                
                    
            }
            catch (Exception)
            {
                //txbMessage.Text = "An Error Occured";
            }
        }

        private void updateDisplay()
        {
            lblQuanityValue.Text = _Product.QuanityOrdered.ToString();
            lblPriceValue.Text = _Product.Price.ToString();
            //decimal lcResult = _Product.Price * _Product.QuanityOrdered;
            //lblTotalValue.Text = lcResult.ToString();
            lblTotalValue.Text = (_Product.Price * _Product.QuanityOrdered).ToString();
            lblProductName.Text = _Product.DVDName.ToString();

        }

        private void pushData()
        {
            _Order.Name = txtName.Text;
            _Order.Address = txtAddress.Text;
            _Order.PhoneNumber = Convert.ToInt32(txtPhoneNumber.Text);
            _Order.ProductName = lblProductName.Text;
            _Order.PricePerItem = Convert.ToDecimal(lblPriceValue.Text);
            _Order.Quanity = Convert.ToInt32(lblQuanityValue.Text);

        }

        private async void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog lcMessageBox = new MessageDialog("Confirm Order?");
            lcMessageBox.Commands.Add(new UICommand("Yes", async x =>
            {
                pushData();
                await ServiceClient.CreateOrder(_Order);
                _Product = await ServiceClient.GetProductAsync(_Order.ProductName);
                _Product.QuanityInStock = _Product.QuanityInStock - _Order.Quanity;
                await ServiceClient.UpdateQuanityInStock(_Product);
                MessageDialog lcConfirmOrder = new MessageDialog("Thank You Your Order Has Been Placed. We Will Now Redirect You Back To The Main Screen");
                lcConfirmOrder.Commands.Add(new UICommand("OK",  y => 
                {
                    Frame.Navigate(typeof(pgCustomerPanel));
                }));
                await lcConfirmOrder.ShowAsync();
                //lblError.Text +=
            }));
            lcMessageBox.Commands.Add(new UICommand("No"));
            await lcMessageBox.ShowAsync();
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
