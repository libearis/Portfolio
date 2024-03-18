using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Seller
    {
        public Seller()
        {
            Carts = new HashSet<Cart>();
            Products = new HashSet<Product>();
            Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Balance { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
