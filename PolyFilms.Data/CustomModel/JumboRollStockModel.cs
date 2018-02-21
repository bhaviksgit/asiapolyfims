using System;

namespace PolyFilms.Data.CustomModel
{
    public class JumboRollStockModel
    {
        public string JumboNo { get; set; }

        public DateTime JumboDate { get; set; }

        public string Product { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal UsedWeight { get; set; }

        public decimal RemainingWeight { get; set; }

        public decimal WasteWeight { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }

        public string CoreNo { get; set; }
    }
}
