using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public long CategoryId { get; set; }
        public long SellerId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public bool Discontinue { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Seller Seller { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
