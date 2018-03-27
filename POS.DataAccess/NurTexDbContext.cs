using System.Data.Entity;
using POS.Models;

namespace POS.DataAccess
{
    public class NurTexDbContext:DbContext
    {
        static NurTexDbContext DbContext = new NurTexDbContext();
        public NurTexDbContext() : base("NurTexDb") { }

        public static NurTexDbContext GetContext()
        {
            return DbContext;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<DiscountToken> DiscountTokens { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
