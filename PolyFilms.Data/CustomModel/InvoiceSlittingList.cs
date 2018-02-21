using PolyFilms.Common;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.CustomModel
{
    public class InvoiceSlittingList
    {
        public long SlittingId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "OrderNo")]
        public string OrderNo { get; set; }

        //[Display(ResourceType = typeof(resLabels), Name = "SlittingDate")]
        //public DateTime SlittingDate { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingRollNo")]
        public string SlittingRollNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Product")]
        public string Product { get; set; }
       
        [Display(ResourceType = typeof(resLabels), Name = "Quality")]
        public string Quality { get; set; }

        
        [Display(ResourceType = typeof(resLabels), Name = "Width")]
        public decimal Width { get; set; }

        
        [Display(ResourceType = typeof(resLabels), Name = "Thickness")]
        public decimal Thickness { get; set; }
        
        [Display(ResourceType = typeof(resLabels), Name = "Netweight")]
        public decimal? Netweight { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "UnitPrice")]
        public decimal UnitPrice { get; set; }

        //[Display(ResourceType = typeof(resLabels), Name = "TotalPrice")]
        //public decimal? TotalPrice { get; set; }

        
    }
}
