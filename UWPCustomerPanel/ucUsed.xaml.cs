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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UWPCustomerPanel
{
    public sealed partial class ucUsed : IWorkControl
    {
        public ucUsed()
        {
            this.InitializeComponent();
        }

        public void PushData(clsProducts prProduct)
        {
            prProduct.DVDCondition = lblCondition.Text.ToString();
            prProduct.QuanityOrdered = Convert.ToInt32(txtQuanity.Text);
        }

        public void UpdateControl(clsProducts prProduct)
        {
            lblConditionResult.Text = prProduct.DVDCondition.ToString();
            //txtQuanity.Text = prProduct.QuanityOrdered.ToString();
                
        }
    }
}
