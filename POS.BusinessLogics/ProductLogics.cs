using System;
using System.Collections.Generic;
using System.Linq;
using POS.DataAccess;
using POS.Models;


namespace POS.BusinessLogics
{
    /// <summary>
    /// Product Model related Business Logics goes here...
    /// </summary>
    public class ProductLogics
    {
        private ProductAccess _productAccess = new ProductAccess();
        public string AddProduct(Product product)
        {
            try
            {
                var hasProdct = _productAccess.GetSingle(product1 => product1.ProductId == product.ProductId);
                if (hasProdct == null)
                {
                    return _productAccess.Add(product) ?
                    "Product Added Successfully."
                    : "Product Not Added.";
                }
                else
                {
                    return "Product already added.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message+"\nContact your service provider.";
            }
        }

        public List<Product> GetAllProduct()
        {
            try
            {
                return _productAccess.GetAll().ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public string DeleteProduct(Product entity)
        {
            try
            {
                return _productAccess.Delete(entity) ? "Product Deleted successfully." : "Product can't be deleted.";
            }
            catch (Exception ex)
            {
                return ex.Message + "\nContact your service provider.";
            }
        }

        public string UpdateProduct(Product entity)
        {
            try
            {
                return _productAccess.Update(entity) ? "Product Updated successfully." : "Product can't be deleted.";
            }
            catch (Exception ex)
            {
                return ex.Message + "\nContact your service provider.";
            }
        }

        public Product SearchProduct(string productId)
        {
            try
            {
                return _productAccess.GetSingle(product => product.ProductId == productId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
