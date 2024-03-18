using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Transaction
    {
        public long Id { get; set; }
        public long SellerId { get; set; }
        public long BuyerId { get; set; }
        public long ProductId { get; set; }
        public long ShipmentId { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }

        public virtual Buyer Buyer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Seller Seller { get; set; } = null!;
        public virtual Shipment Shipment { get; set; } = null!;
    }
}
