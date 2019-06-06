using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCustomerPanel
{
    interface IWorkControl
    {
        void PushData(clsProducts prProduct);

        void UpdateControl(clsProducts prProduct);
    }
}
