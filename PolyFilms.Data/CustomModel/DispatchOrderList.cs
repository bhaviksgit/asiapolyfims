using System;

namespace PolyFilms.Data.CustomModel
{
    public class DispatchOrderList
    {
        public long OrderId { get; set; }
        public string OrderNo { get; set; }

        public DateTime OrderDate { get; set; }

        public string Product { get; set; }

        public decimal TotalQuantity { get; set; }

        public decimal RemainingQuantity { get; set; }
    }
}
