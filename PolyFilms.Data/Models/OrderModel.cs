using PolyFilms.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.Models
{
    public class OrderModel
    {
        [ScaffoldColumn(false)]
        public long OrderId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "OrderNo")]
        public string OrderNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "OrderDate")]
        [Required(ErrorMessageResourceName = "OrderDateRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public DateTime OrderDate { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "BuyerId")]
        [Required(ErrorMessageResourceName = "BuyerIdRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public long BuyerId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "ConsigneeId")]
        //[Required(ErrorMessageResourceName = "ConsigneeIdRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public long ConsigneeId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Remarks")]
        [StringLength(100, ErrorMessageResourceName = "RemarksLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Specialinstruction { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "DeliverySchedule")]
        [Required(ErrorMessageResourceName = "DeliveryScheduleRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public DateTime DeliverySchedule { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "DeliveryAddress")]
        public string DeliveryAddress { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Status")]
        public short OrderStatusId { get; set; }

        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? SequenceNo { get; set; }
    }
}
