using System;

namespace PolyFilms.Data.CustomModel
{
    public class SalesOrderDetailModel
    {
        public long OrderId { get; set; }
        public string OrderNo { get; set; }

        public DateTime OrderDate { get; set; }

        public string Buyer { get; set; }
        
        public string City { get; set; }
        
        public short ProductId { get; set; }

        public string Product { get; set; }
            
        public decimal Micron { get; set; }

        public decimal Width { get; set; }
        public decimal Quantity { get; set; }

        public decimal TotalSlittingDone { get; set; }

        public decimal DispatchedQty { get; set; }

        public decimal RemainigDispatchQty { get; set; }

        public decimal TotalRemainigSlitting { get; set; }

        public decimal SlittingQtyInStock { get; set; }   

        public decimal SlittingToBeProduce { get; set; }

    }
}
