﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCustomerPanel
{
    static class clsUtil
    {
        public static string EmptyIfNull(this string prString)
        {
            return prString == null ? string.Empty : prString;
        }
    }
}
