using System;
using System.Collections.Generic;
using System.Linq;
using POS.Models;
using POS.DataAccess;

namespace POS.BusinessLogics
{
    public class SaleLogics
    {
        private SaleAccess _saleAccess = new SaleAccess();
        private ProductLogics _productLogics = new ProductLogics();
        private CustomerLogics _customerLogics = new CustomerLogics();

        public string SaveNewSale(Sale newSale)
        {
            try
            {
                if (!_saleAccess.Add(newSale))
                    return "Sale not successfull.\nSome Error Occoured.";
                foreach (Order order in newSale.Orders)
                {
                    order.Product.Quantity-=order.Quantity;
                    _productLogics.UpdateProduct(order.Product);
                }
                if(newSale.Customer!=null)
                    _customerLogics.AddPoints(newSale.Customer, (int)newSale.TotalAmmount / 100);
                return "Sale successfull";
            }
            catch (Exception ex)
            {
                return ex.Message + "\nContact to service provider.";
            }
        }

        public Sale GetSaleById(string saleId)
        {
            return _saleAccess.GetSingle(sale => sale.Id == Convert.ToInt32(saleId));
        }
        public List<Sale> SalesReport(DateTime fromDateTime, DateTime toDateTime)
        {
            try
            {
                return
                    _saleAccess.GetAll()
                        .Where(s => s.PurcheseDateTime.Date >= fromDateTime.Date)
                        .Where(s => s.PurcheseDateTime.Date <= toDateTime.Date)
                        .ToList();
            }
            catch (Exception ex)
            {
                return new List<Sale>();
            }
        }

        public List<ProductWiseSalesReportViewModel> ProductWiseSaleReport(DateTime fromDateTime, DateTime toDateTime)
        {
            var sales = SalesReport(fromDateTime, toDateTime);
            var orders = new List<Order>();
            foreach (var sale in sales)
            {
                orders.AddRange(sale.Orders);
            }
            var dictionary = new Dictionary<Product, int>();
            foreach (var order in orders.Where(order => order.Product != null))
            {
                if (dictionary.ContainsKey(order.Product))
                {
                    dictionary[order.Product] += order.Quantity;
                }
                else
                {
                    dictionary.Add(order.Product,order.Quantity);
                }
            }
            return dictionary.Select(keyValuePair => new ProductWiseSalesReportViewModel()
            {
                Product = keyValuePair.Key, Quantity = keyValuePair.Value
            }).ToList();
        }
    }
}
