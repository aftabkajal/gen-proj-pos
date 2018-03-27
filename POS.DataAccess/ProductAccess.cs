using POS.Models;
namespace POS.DataAccess
{
    public class ProductAccess:GenericDataAccess<Product>
    {
        public ProductAccess() : base(NurTexDbContext.GetContext()) { }
    }
}
