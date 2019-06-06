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
    public sealed partial class pgCategory : Page
    {
        private clsCategory _Category = new clsCategory();
        private string lcCategoryName;
        public pgCategory()
        {
            this.InitializeComponent();
        }

        private void updateDisplay()
        {
            lblCategoryName.Text = _Category.CategoryName;
            lstProductList.ItemsSource = null;
            if (_Category.CategoryList != null)
                lstProductList.ItemsSource = _Category.CategoryList;
        }

        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                base.OnNavigatedTo(e);
                if (e.Parameter != null)
                {
                    lcCategoryName = e.Parameter.ToString();
                    _Category = await ServiceClient.GetProductListAsync(lcCategoryName);
                    updateDisplay();
                }
                else
                    _Category = new clsCategory();

            }
            catch(Exception)
            {
                lblError.Text = "An Error Occured";
            }
        }

        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            if (lstProductList.SelectedItem != null)
                Frame.Navigate(typeof(pgProduct), lstProductList.SelectedItem);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            
                Frame.Navigate(typeof(pgCustomerPanel));
        }
    }
}
