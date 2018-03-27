using POS.Models;

namespace POS.DataAccess
{
    public class CustomerAccess:GenericDataAccess<Customer>
    {
        public CustomerAccess() : base(NurTexDbContext.GetContext())
        {
            
        }
    }
}
