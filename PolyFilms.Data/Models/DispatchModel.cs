using PolyFilms.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.Models
{
    public class DispatchModel
    {
        //[ScaffoldColumn(false)]
        public long DispatchId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "DispatchNo")]
        public string DispatchNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Order")]
        //[Required(ErrorMessageResourceName = "OrderRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public long? OrderId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "BuyerId")]
        [Required(ErrorMessageResourceName = "BuyerIdRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public long BuyerId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "ConsigneeId")]
        [Required(ErrorMessageResourceName = "ConsigneeIdRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public long ConsigneeId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "DispatchDate")]
        [Required(ErrorMessageResourceName = "DispatchDateRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public DateTime DispatchDate { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "IsInvoiceGenerated")]
        public bool IsInvoiceGenerated { get; set; }

        public bool IsFinalize { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "ModeOfTransport")]
        [Required(ErrorMessageResourceName = "ModeOfTransportRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string ModeOfTransport { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "NameOfTransporter")]
        [Required(ErrorMessageResourceName = "NameOfTransporterRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string NameOfTransporter { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "VehicleNo")]
        [Required(ErrorMessageResourceName = "VehicleNoRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string VehicleNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Lrno")]
        [Required(ErrorMessageResourceName = "LrnoRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Lrno { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "AppGrossWeight")]
        public decimal AppGrossWeight { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "TotalRoll")]
        public short TotalRoll { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Remarks")]
        public string Remarks { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int SequenceNo { get; set; }
        public string checkedSlittingId { get; set; }

       
    }
}
