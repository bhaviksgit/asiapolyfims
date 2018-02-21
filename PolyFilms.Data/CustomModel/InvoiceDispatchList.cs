using PolyFilms.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.CustomModel
{
    public class InvoiceDispatchList
    {
        public long DispatchId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "DispatchNo")]
        public string DispatchNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "DispatchDate")]
        public DateTime DispatchDate { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "AppGrossWeight")]
        public decimal AppGrossWeight { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "TotalRoll")]
        public short TotalRoll { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Remarks")]
        public string Remarks { get; set; }

        public bool IsChecked { get; set; }
    }
}
