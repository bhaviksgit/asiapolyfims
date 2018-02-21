using System;
using System.Collections.Generic;
using System.Text;

namespace PolyFilms.Data.CustomModel
{
    public class SalesOrderModel
    {
        public string SlittingRollNo { get; set; }

        public string FilmType { get; set; }

        public decimal Micron { get; set; }

        public string DispatchNo { get; set; }

        public DateTime? DispatchDate { get; set; }

        public decimal Quantity { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime? InvoiceDate { get; set; }
    }
}
