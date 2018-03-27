using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace POS.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime PurcheseDateTime { get; set; }
        public double AdvancePayment { get; set; }
        public double DuePayment { get; set; }
        public virtual DiscountToken DiscountToken{ get; set; }
        public virtual User Issuer { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        [NotMapped]
        public double TotalAmmount => GetTotalAmmount();

        [NotMapped]
        public int ProductCount => GetProductCount();

        public int GetProductCount()
        {
            return Orders.Sum(order => order.Quantity);
        }

        public double GetTotalAmmount()
        {
            if (DiscountToken == null)
                return Orders.Sum(order => order.GetTotal());
            return Orders.Sum(order => order.GetTotal()) -
                   (Orders.Sum(order => order.GetTotal()*(DiscountToken.Percentage/100)));
        }


    }
}
