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
    public partial class frmUsed : frmProduct
    {
        public frmUsed()
        {
            InitializeComponent();
        }

        public static readonly frmUsed Instance = new frmUsed();

        public static void Run(clsProducts prUsed)
        {
            frmUsed.Instance.SetDetails(prUsed);
        }

        protected override void updateForm()
        {
            base.updateForm();
            txtCondition.Text = _Products.DVDCondition;
        }

        protected override void pushData()
        {
            base.pushData();
            _Products.DVDCondition = txtCondition.Text;
        }
    }
}
