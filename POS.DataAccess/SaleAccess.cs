using POS.Models;

namespace POS.DataAccess
{
    public class SaleAccess:GenericDataAccess<Sale>
    {
        public SaleAccess() : base(NurTexDbContext.GetContext()) { }
    }
}
