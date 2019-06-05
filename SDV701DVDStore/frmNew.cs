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
    public partial class frmNew : frmProduct
    {
        public frmNew()
        {
            InitializeComponent();
        }

        public static readonly frmNew Instance = new frmNew();

        public static void Run(clsProducts prNew)
        {
            frmNew.Instance.SetDetails(prNew);
        }
    }
}
