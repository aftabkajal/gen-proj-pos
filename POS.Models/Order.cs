using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public double SellingPrice { get; set; }
        public double Discount { get; set; }

        public virtual Sale Sale { get; set; }

        [NotMapped]
        public double Total => GetTotal();
        public double GetTotal()
        {
            return (Quantity * SellingPrice) - ((Quantity * SellingPrice) * (Discount / 100));
        }
    }
}
