using PolyFilms.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.CustomModel
{
    public class DispatchSlittingList
    {
        
        public long SlittingId { get; set; }

        public long? OrderId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "OrderNo")]
        public string OrderNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingDate")]
        public DateTime SlittingDate { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingRollNo")]
        public string SlittingRollNo { get; set; }

        //[Display(ResourceType = typeof(resLabels), Name = "Product")]
        public short ProductId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Product")]
        public string ProductName { get; set; }

        //[Display(ResourceType = typeof(resLabels), Name = "Quality")]
        public short Quality { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Quality")]
        public string QualityName { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Length")]
        public decimal Length { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Width")]
        public decimal Width { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Od")]
        public decimal Od { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Thickness")]
        public decimal Thickness { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Grossweight")]
        public decimal Grossweight { get; set; }

        public bool IsChecked { get; set; }
    }

    public class TotalAmountModel
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
