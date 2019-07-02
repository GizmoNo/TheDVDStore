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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
        }

        protected clsProducts _Products = new clsProducts();

        public void SetDetails(clsProducts prProducts)
        {
            _Products = prProducts;
            updateForm();
            ShowDialog();
            
        }

        private bool lcResult;
        private async void btnCreate_Click(object sender, EventArgs e)
        {

            if (isValid() == true)
            {
                
                if (txtName.Enabled)
                {

                    lcResult = CheckAllFields();
                    if (lcResult == true)
                    {
                        pushData();
                        MessageBox.Show(await ServiceClient.InsertProductAsync(_Products));
                        txtName.Enabled = false;
                        Hide();
                    }

                    else
                    {
                        MessageBox.Show("Please Make Sure All Fields Are Filled In");

                    }

                }

                else
                {
                    lcResult = CheckAllFields();
                    if (lcResult == true)
                    {
                        pushData();
                        MessageBox.Show(await ServiceClient.UpdateProductAsync(_Products));
                        txtName.Enabled = false;
                        Hide();
                    }

                    else
                    {
                        MessageBox.Show("Please Make Sure All Fields Are Filled In");

                    }

                }


            }
        }

        protected virtual void updateForm()
        {
            if (_Products.DVDName != null)
            { 
                txtName.Enabled = false;
            }
            else
            {
                txtName.Enabled = true;
            }
            txtName.Text = _Products.DVDName;
            txtDescription.Text = _Products.Description;
            txtPrice.Text = Convert.ToString(_Products.Price);
            txtTotalUnits.Text = _Products.QuanityInStock.ToString();
            lblLastModified.Text = _Products.LastModified.ToString();
            

        }

        protected virtual void pushData()
        {
            _Products.DVDName = txtName.Text;
            _Products.Description = txtDescription.Text;
            
            _Products.Price = decimal.Parse(txtPrice.Text);
            _Products.QuanityInStock = int.Parse(txtTotalUnits.Text);
        }

        public virtual bool isValid()
        {
            return true;
        }

        public delegate void LoadProductFromDelegate(clsProducts prProduct);
        public static Dictionary<string, Delegate> _ProductForm = new Dictionary<string, Delegate>
        {
            {"New", new LoadProductFromDelegate(frmNew.Run) },
            {"Used", new LoadProductFromDelegate(frmUsed.Run) }
            
        };

        public static void DispatchProductForm(clsProducts prProduct)
        {
            _ProductForm[prProduct.DVDType].DynamicInvoke(prProduct);
                                   
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

       

        private bool CheckAllFields()
        {
            bool lcResult = true;
            foreach (TextBox TB in this.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(TB.Text) || string.IsNullOrWhiteSpace(TB.Text))
                {
                    lcResult =  false;
                    //TB.BackColor = Color.Red;
                    break;
                }
                else
                {
                    lcResult = true;
                }
            }
            return lcResult;


        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckInput(e);
        }
                
        private void txtTotalUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckInput(e);
        }

        private static void CheckInput(KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void frmProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
