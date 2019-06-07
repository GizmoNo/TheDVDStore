using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCustomerPanel
{
    public class clsCategory
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<clsProducts> CategoryList { get; set; }
    }

    public class clsProducts
    {
        public string DVDName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DVDType { get; set; }
        public DateTime LastModified { get; set; }
        public int QuanityInStock { get; set; }
        public string DVDCondition { get; set; }
        public string Category { get; set; }
        public int QuanityOrdered { get; set; }


        private static string[] lstProductType = { "New", "Used" };
        public static string[] LstProductType => lstProductType;

        public override string ToString()
        {
            return
                DVDName + "\t" + "\t" + Description + "\t" + "\t" + Price;
        }

        public static clsProducts NewProduct(string prChoice)
        {
            return new clsProducts() { DVDType = prChoice };
        }
    }

    public class clsOrder
    {
        public int OrderNumber { get; set; }
        public int Quanity { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public decimal PricePerItem { get; set; }
        public string ProductName { get; set; }
    }
}
