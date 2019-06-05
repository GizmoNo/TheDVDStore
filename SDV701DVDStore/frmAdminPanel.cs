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
        

        private frmCategory _NewProductForm = new frmCategory();
        public async void UpdateDisplay()
        {
            try
            {
                lstProducts.DataSource = null;
                lstProducts.DataSource = await ServiceClient.GetCategoryListAsync();
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
    }

    
}
