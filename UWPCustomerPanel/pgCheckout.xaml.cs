using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        }
    }
}
