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
    public partial class frmCategory : Form
    {
        private clsCategory _Category;
        
        private static Dictionary<string, frmCategory> _CategoryFormList = new Dictionary<string, frmCategory>();
        
        public frmCategory()
        {
            InitializeComponent();
            cboChoice.DataSource = clsProducts.LstProductType; //UWP datasorce is ItemSource
            cboChoice.SelectedIndex = 0;
        }

        public static void Run(string prCategoryName)
        {
            
            frmCategory lcCategoryForm;
            if (string.IsNullOrEmpty(prCategoryName) ||
                !_CategoryFormList.TryGetValue(prCategoryName, out lcCategoryForm))
            {
                lcCategoryForm = new frmCategory();
                if (string.IsNullOrEmpty(prCategoryName))
                    lcCategoryForm.SetDetails(new clsCategory());
                else
                {
                    _CategoryFormList.Add(prCategoryName, lcCategoryForm);
                    lcCategoryForm.refreshFormFromDB(prCategoryName);
                }
            }
            else
            {
                lcCategoryForm.Show();
                lcCategoryForm.Activate();
            }
        }

        public void SetDetails(clsCategory prCategory)
        {

            _Category = prCategory;
            lblCategory.Enabled = string.IsNullOrEmpty(_Category.CategoryName);
            updateDisplay();
            updateList();
            Show();
        }

        public void updateList()
        {
            
            lstProducts.DataSource = null;
            if (_Category.CategoryList != null)
                lstProducts.DataSource = _Category.CategoryList;
            
        }

        private void updateDisplay()
        {
            lblCategory.Text = _Category.CategoryName;
            lblDescription.Text = _Category.Description;
        }

        private async void refreshFormFromDB(string prCategoryName)
        {
            SetDetails(await ServiceClient.GetProductListAsync(prCategoryName));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                clsProducts lcProduct = clsProducts.NewProduct(cboChoice.SelectedItem.ToString());
                if (lcProduct != null)
                {
                                       
                                            
                    lcProduct.Category = _Category.CategoryName;
                    frmProduct.DispatchProductForm(lcProduct);
                    if (!string.IsNullOrEmpty(lcProduct.DVDName))
                    {
                        refreshFormFromDB(_Category.CategoryName);
                        
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmProduct.DispatchProductForm(lstProducts.SelectedItem as clsProducts);
            refreshFormFromDB(_Category.CategoryName);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Hide();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult lcResult = MessageBox.Show("Are You Sure You Want To Delete This Product?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(lcResult == DialogResult.Yes)
            {
                MessageBox.Show(await ServiceClient.DeleteProductAsync(lstProducts.SelectedItem as clsProducts));
                refreshFormFromDB(_Category.CategoryName);
                frmAdminPanel._Instance.UpdateDisplay();
            }
                       
            
        }
    }
}
