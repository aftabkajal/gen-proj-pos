using POS.Models;

namespace POS.DataAccess
{
    public class UserAccess:GenericDataAccess<User>
    {
        public UserAccess() : base(NurTexDbContext.GetContext())
        {
            
        }
    }
}
