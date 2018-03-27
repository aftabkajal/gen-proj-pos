using POS.Models;

namespace POS.DataAccess
{
    public class DiscountTokenAccess:GenericDataAccess<DiscountToken>
    {
        public DiscountTokenAccess() : base(NurTexDbContext.GetContext())
        {
            
        }
    }
}
