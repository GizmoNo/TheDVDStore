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
    public sealed partial class pgProduct : Page
    {

        private delegate void LoadWorkControlDelegate(clsProducts prProducts);
        private Dictionary<string, Delegate> _ProductContent;
        private clsProducts _Products = new clsProducts();
        public pgProduct()
        {
            this.InitializeComponent();

            _ProductContent = new Dictionary<string, Delegate>
            {
                {"New", new LoadWorkControlDelegate(RunNew) },
                {"Used", new LoadWorkControlDelegate(RunUsed) }
            };
        }

        private void updatePage(clsProducts prProduct)
        {
            _Products = prProduct;
            lblDVDName.Text = _Products.DVDName.EmptyIfNull();
            lblDescriptionResult.Text = _Products.Description.ToString();
            lblQuanityOnHandResult.Text = _Products.QuanityInStock.ToString();
            lblPriceResult.Text = _Products.Price.ToString();
            lblTypeResult.Text = _Products.DVDType.ToString();
            (ctcProductSpecs.Content as
                IWorkControl).UpdateControl(prProduct);
        }

        private void pushData()
        {
            (ctcProductSpecs.Content as
                IWorkControl).PushData(_Products);
        }

        private void RunNew(clsProducts prProduct)
        {
            ctcProductSpecs.Content = new ucNew();
            
        }

        private void RunUsed(clsProducts prProduct)
        {
            ctcProductSpecs.Content = new ucUsed();
        }
               
        private void dispatchProductContent(clsProducts prProduct)
        {
            _ProductContent[prProduct.DVDType].DynamicInvoke(prProduct);
            updatePage(prProduct);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            dispatchProductContent(e.Parameter as clsProducts);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void BtnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            //Create new class and input order into database
            pushData();
            Frame.Navigate(typeof(pgCheckout), _Products);
        }
    }
}
