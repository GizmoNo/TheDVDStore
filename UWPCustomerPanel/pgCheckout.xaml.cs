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

        clsProducts _CustomerProduct = new clsProducts();
        clsProducts _DatabaseProduct = new clsProducts();
        clsOrder _Order = new clsOrder();
        public pgCheckout()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                
                base.OnNavigatedTo(e);
                if (e.Parameter != null)
                {
                    await ErrorCheckingStockAvalibility(e);
                }
                
            }
            catch (Exception)
            {
                lblError.Text = "An Error Occured";
            }
        }

        private async System.Threading.Tasks.Task ErrorCheckingStockAvalibility(NavigationEventArgs e)
        {
            _CustomerProduct = e.Parameter as clsProducts;
            _DatabaseProduct = await ServiceClient.GetProductAsync(_CustomerProduct.DVDName);
            if (_DatabaseProduct.QuanityInStock >= 1)
            {
                btnBuy.IsEnabled = true;
            }
            else
            {
                btnBuy.IsEnabled = false;
                lblError.Text = "We are Currently Out Of Stock Please Check Back Later";
            }
            if (_DatabaseProduct.QuanityInStock < _CustomerProduct.QuanityOrdered)
            {
                btnBuy.IsEnabled = false;
                lblError.Text = "You Are Trying To Order More Than We Have In Stock. Please Go Back And Change " +
                    "The Quanity You Wish Or Order To Match Or Be Less Than We Have In Stock";

            }
            else
            {
                btnBuy.IsEnabled = true;
            }
            updateDisplay();
        }

        private void updateDisplay()
        {
            lblQuanityValue.Text = _CustomerProduct.QuanityOrdered.ToString();
            lblPriceValue.Text = _CustomerProduct.Price.ToString();
            lblTotalValue.Text = (_CustomerProduct.Price * _CustomerProduct.QuanityOrdered).ToString();
            lblProductName.Text = _CustomerProduct.DVDName.ToString();

        }

        private async void updateDisplayOnDatabaseChanage()
        {
            _DatabaseProduct = await ServiceClient.GetProductAsync(_CustomerProduct.DVDName);
            if (_DatabaseProduct.QuanityInStock >= 1)
            {
                btnBuy.IsEnabled = true;
            }
            else
            {
                btnBuy.IsEnabled = false;
                lblError.Text = "We are Currently Out Of Stock Please Check Back Later";
            }
            if (_DatabaseProduct.QuanityInStock < _CustomerProduct.QuanityOrdered)
            {
                btnBuy.IsEnabled = false;
                lblError.Text = "You Are Trying To Order More Than We Have In Stock. Please Go Back And Change " +
                    "The Quanity You Wish Or Order To Match Or Be Less Than We Have In Stock";

            }
            else
            {
                btnBuy.IsEnabled = true;
            }
            updateDisplay();
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

        //Error Check Customer Input Fields
        private async void BtnBuy_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblError.Text = "Please Enter Your Name";
            }
            else
            {
                if (string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    lblError.Text = "Please Enter Your Address";
                }
                else
                {
                    if (string.IsNullOrEmpty(txtPhoneNumber.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
                    {
                        lblError.Text = "Please Enter Your PhoneNumber";
                    }
                    else
                    {
                        MessageDialog lcMessageBox = new MessageDialog("Confirm Order?");
                        lcMessageBox.Commands.Add(new UICommand("Yes", async x =>
                        {
                            _DatabaseProduct = await ServiceClient.GetProductAsync(_CustomerProduct.DVDName);
                            if (_DatabaseProduct.QuanityInStock != _CustomerProduct.QuanityInStock)
                            {
                                MessageDialog lcCheckDatabaseForChanage = new MessageDialog("Stock On Hand Has Changed. The Form Will Now Refresh");
                                lcCheckDatabaseForChanage.Commands.Add(new UICommand("Reload", async z =>
                                {
                                    int lcCustomerQuanityOrdered;
                                    lcCustomerQuanityOrdered = _CustomerProduct.QuanityOrdered;
                                    _CustomerProduct = await ServiceClient.GetProductAsync(_DatabaseProduct.DVDName);
                                    _CustomerProduct.QuanityOrdered = lcCustomerQuanityOrdered;
                                    updateDisplayOnDatabaseChanage();


                                }));
                                await lcCheckDatabaseForChanage.ShowAsync();
                            }
                            else
                            {
                                pushData();
                                await ServiceClient.CreateOrder(_Order);
                                _CustomerProduct = await ServiceClient.GetProductAsync(_Order.ProductName);
                                _CustomerProduct.QuanityInStock = _CustomerProduct.QuanityInStock - _Order.Quanity;
                                await ServiceClient.UpdateQuanityInStock(_CustomerProduct);
                                MessageDialog lcConfirmOrder = new MessageDialog("Thank You Your Order Has Been Placed. We Will Now Redirect You Back To The Main Screen");
                                lcConfirmOrder.Commands.Add(new UICommand("OK", y =>
                                {
                                    Frame.Navigate(typeof(pgCustomerPanel));
                                }));
                                await lcConfirmOrder.ShowAsync();
                            }


                        }));
                        lcMessageBox.Commands.Add(new UICommand("No"));
                        await lcMessageBox.ShowAsync();
                    }
                }
            }

            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            
        }

        private void TxtPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtPhoneNumber.Text, "[^0-9]"))
            {
                txtPhoneNumber.Text = txtPhoneNumber.Text.Remove(txtPhoneNumber.Text.Length - 1);
            }
        }
    }
}
