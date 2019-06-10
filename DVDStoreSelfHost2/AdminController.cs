using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.Http;

namespace DVDStoreSelfHost2
{
    public class AdminController : System.Web.Http.ApiController
    {
        public List<string> GetCategoryList()
        {
           DataTable lcResult = clsDBConnection.GetDataTable("SELECT Name FROM Category", null);
         //DataTable lcResult = clsDBConnection.GetDataTable("SELECT CONCAT(Name, '   ', Description) FROM Category", null);
            List<string> lcNames = new List<string>();
            foreach (DataRow dr in lcResult.Rows)
                lcNames.Add((string)dr[0]);
                

            return lcNames;
        }
                
        public clsCategory GetProductList(string Name)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Name", Name);
            DataTable lcResult =
                clsDBConnection.GetDataTable("SELECT * FROM Category WHERE Name = @Name", par);
            if (lcResult.Rows.Count > 0)
                return new clsCategory()
                {
                    CategoryName = (string)lcResult.Rows[0]["Name"],
                    Description = (string)lcResult.Rows[0]["Description"],
                    
                    CategoryList = getCategoryProducts(Name)
                };
            else
                return null;
        }

        public List<clsOrder> GetOrderList()
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            //par.Add("Name", Name);
            DataTable lcResult = clsDBConnection.GetDataTable("SELECT * FROM Orders", par);
            List<clsOrder> lcOrders = new List<clsOrder>();
            foreach (DataRow dr in lcResult.Rows)
                lcOrders.Add(dataRow2Orders(dr));
            return lcOrders;
        }

        public clsProducts GetProduct(string Name)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Name", Name);
            DataTable lcResult =
                clsDBConnection.GetDataTable("SELECT * FROM Products WHERE DVDName = @Name", par);
            if (lcResult.Rows.Count > 0)
                return new clsProducts()
                {
                    QuanityInStock = (Int32)lcResult.Rows[0]["QuanityInStock"],
                    DVDName = (string)lcResult.Rows[0]["DVDName"]
                    
                };
            else
                return null;
        }

        public clsOrder GetOrderInfo(string ID)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("ID", ID);
            DataTable lcResult =
                clsDBConnection.GetDataTable("SELECT * FROM Orders WHERE OrderNumber = @ID", par);
            if (lcResult.Rows.Count > 0)
                return new clsOrder()
                {
                    Name = (string)lcResult.Rows[0]["Name"],
                    Address = (string)lcResult.Rows[0]["Address"],
                    PhoneNumber = (int)lcResult.Rows[0]["Phone"],
                    Quanity = (int)lcResult.Rows[0]["Quanity"],
                    PricePerItem = (int)lcResult.Rows[0]["PricePerItem"],
                    ProductName = (string)lcResult.Rows[0]["ProductsName"],
                    OrderNumber = (int)lcResult.Rows[0]["OrderNumber"]

                };
            else
                return null;
        }

        public string PostProduct(clsProducts prProduct)
        {   // insert     
            try
            {
                int lcRecCount = clsDBConnection.Execute("INSERT INTO Products " +
                    "(DVDName, Description, Price, DVDType, LastModified, QuanityInStock, DVDCondition, Category) " +
                    "VALUES (@DVDName, @Description, @Price, @DVDType, Now(), @QuanityInStock, @DVDCondition, @Category)",
                    prepareProductParameters(prProduct));
                if (lcRecCount == 1)
                    return "One Product Added";
                else
                    return "Unexpected Product Insert Count: " + lcRecCount;

            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }

        public string PutProduct(clsProducts prProduct)
        {
            try
            {
                int lcRecCount = clsDBConnection.Execute(
                    "UPDATE Products SET Description = @Description, Price = @Price, LastModified = Now(), QuanityInStock = @QuanityInStock, DVDCondition = @DVDCondition WHERE DVDName = @DVDName",
                    prepareProductParameters(prProduct));
                if (lcRecCount == 1)
                    return "One Product Updated";
                else
                    return "Unexpected Product Update Count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }

        public string DeleteProduct(clsProducts prProduct)
        {
            try
            {
                int lcRecCount = clsDBConnection.Execute(
                    "DELETE FROM Products WHERE DVDName = @DVDName AND Category = @Category",
                    prepareProductParameters(prProduct));
                if (lcRecCount == 1)
                    return "One Product Deleted";
                else
                    return "Unexpected Product Delete count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }

        public string DeleteOrder(clsOrder prOrder)
        {
            try
            {
                int lcRecCount = clsDBConnection.Execute(
                    "DELETE FROM Orders WHERE OrderNumber = @OrderNumber",
                    prepareOrderParameters(prOrder));
                if (lcRecCount == 1)
                    return "One Order Deleted";
                else
                    return "Unexpected Product Delete count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }

        public string CreateOrder(clsOrder prOrder)
        {   // insert     
            try
            {
                int lcRecCount = clsDBConnection.Execute("INSERT INTO Orders " +
                    "(Quanity, Name, Address, Phone, PricePerItem, ProductsName) " +
                    "VALUES (@Quanity, @Name, @Address, @Phone, @PricePerItem, @ProductsName)",
                    prepareOrderParameters(prOrder));
                if (lcRecCount == 1)
                    return "Your Order Has Been Submitted";
                else
                    return "Unexpected Product Insert Count: " + lcRecCount;

            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }

        [HttpPut]public string UpdateQuanityInStock(clsProducts prProduct)
        {
            try
            {

                int lcRecCount = clsDBConnection.Execute(
                    "UPDATE Products SET QuanityInStock = @QuanityInStock WHERE DVDName = @DVDName",
                    prepareProductParameters(prProduct));
                if (lcRecCount == 1)
                    return "Order Updated";
                else
                    return "Unexpected order update count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }

        private Dictionary<string, object> prepareProductParameters(clsProducts prProducts)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(10);
            par.Add("Category", prProducts.Category);
            par.Add("DVDName", prProducts.DVDName);
            par.Add("Description", prProducts.Description);
            par.Add("DVDCondition", prProducts.DVDCondition);
            par.Add("DVDType", prProducts.DVDType);
            par.Add("Price", prProducts.Price);
            //par.Add("LastModified", prProducts.LastModified);
            par.Add("QuanityInStock", prProducts.QuanityInStock);
            

            return par;
        }

        private Dictionary<string, object> prepareOrderParameters(clsOrder prOrder)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(10);
            par.Add("Name", prOrder.Name);
            par.Add("Address", prOrder.Address);
            par.Add("Phone", prOrder.PhoneNumber);
            par.Add("Quanity", prOrder.Quanity);
            par.Add("PricePerItem", prOrder.PricePerItem);
            par.Add("ProductsName", prOrder.ProductName);
            par.Add("OrderNumber", prOrder.OrderNumber);
            
            return par;
        }

        private List<clsProducts> getCategoryProducts(string Name)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("Name", Name);
            DataTable lcResult = clsDBConnection.GetDataTable("SELECT * FROM Products WHERE Category = @Name", par);
            List<clsProducts> lcProducts = new List<clsProducts>();
            foreach (DataRow dr in lcResult.Rows)
                lcProducts.Add(dataRow2Products(dr));
            return lcProducts;
        }
              
        private clsProducts dataRow2Products(DataRow prDataRow)
        {
            return new clsProducts()
            {
                DVDName = Convert.ToString(prDataRow["DVDName"]),
                Description = Convert.ToString(prDataRow["Description"]),
                Price = Convert.ToDecimal(prDataRow["Price"]),
                DVDType = Convert.ToString(prDataRow["DVDType"]),
                LastModified = Convert.ToDateTime(prDataRow["LastModified"]),
                QuanityInStock = Convert.ToInt32(prDataRow["QuanityInStock"]),
                DVDCondition = Convert.ToString(prDataRow["DVDCondition"]),
                Category = Convert.ToString(prDataRow["Category"])
            };
        }

        private clsOrder dataRow2Orders(DataRow prDataRow)
        {
            return new clsOrder()
            {
                Name = Convert.ToString(prDataRow["Name"]),
                Address = Convert.ToString(prDataRow["Address"]),
                PhoneNumber = Convert.ToInt32(prDataRow["Phone"]),
                Quanity = Convert.ToInt32(prDataRow["Quanity"]),
                PricePerItem = Convert.ToDecimal(prDataRow["PricePerItem"]),
                ProductName = Convert.ToString(prDataRow["ProductsName"]),
                OrderNumber = Convert.ToInt32(prDataRow["OrderNumber"])
                
            };
        }


    }
}
