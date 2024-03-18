using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            Carts = new HashSet<Cart>();
            Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool Service { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
