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
    public sealed partial class pgCustomerPanel : Page
    {
        public pgCustomerPanel()
        {
            this.InitializeComponent();

            
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                lstCategories.ItemsSource = await ServiceClient.GetCategoryListAsync();
            }
            catch (Exception)
            {
                lblError.Text = "An Error has occored while loading. Contact Your Administrator."; 
            }
        }

        private void CategoryChoice()
        {
            if (lstCategories.SelectedItem != null)
                Frame.Navigate(typeof(pgCategory), lstCategories.SelectedItem);
        }

        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            CategoryChoice();
        }
    }
}
