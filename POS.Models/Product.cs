using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Models
{
    public class Product
    {
        public int Id { get; set; }
        [DisplayName("Product Id")]
        public string ProductId { get; set; }
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [DisplayName("Brand")]
        public string Brand { get; set; }
        [DisplayName("Color")]
        public string Color { get; set; }
        [DisplayName("Size")]
        public string Size { get; set; }
        [DisplayName("Current Qty.")]
        public long Quantity { get; set; }
        [DisplayName("Buying Price")]
        public double UnitBuyingPrice { get; set; }
        [DisplayName("Selling Price")]
        public double UnitSellingPrice { get; set; }
        [Column(TypeName = "datetime2")]
        [DisplayName("Entried at")]
        public DateTime EntryDateTime { get; set; }
        [DisplayName("Discount(%)")]
        public double PossibleDiscountPercentage { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
