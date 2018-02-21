using PolyFilms.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.Models
{
    public class SlittingModel
    {
        [ScaffoldColumn(false)]
        public long SlittingId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingDate")]
        [Required(ErrorMessageResourceName = "SlittingDateRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public DateTime SlittingDate { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingRollNo")]
        public string SlittingRollNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Shift")]
        [Required(ErrorMessageResourceName = "ShiftRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short ShiftId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Order")]
        //[Required(ErrorMessageResourceName = "OrderRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public long? OrderId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Jumbo")]
        public long? JumboId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "PrimarySlitting")]
        public long? PrimarySlittingId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Product")]
        [Required(ErrorMessageResourceName = "ProductRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short ProductId { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(ResourceType = typeof(resLabels), Name = "Location")]
        [Required(ErrorMessageResourceName = "LocationRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(1, ErrorMessageResourceName = "LocationSlittingLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Location { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Length")]
        [Required(ErrorMessageResourceName = "LengthRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Length { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Width")]
        [Required(ErrorMessageResourceName = "WidthRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Width { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Od")]
        [Required(ErrorMessageResourceName = "OdRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Od { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Thickness")]
        [Required(ErrorMessageResourceName = "ThicknessRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Thickness { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Grossweight")]
        [Required(ErrorMessageResourceName = "GrossweightRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Grossweight { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Netweight")]
        public decimal? Netweight { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Core")]
        [Required(ErrorMessageResourceName = "CoreRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short CoreId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "CoreWeight")]
        public decimal? CoreWeight { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Quality")]
        [Required(ErrorMessageResourceName = "QualityRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short Quality { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Joint")]
        public int? Joint { get; set; }

        //[Display(ResourceType = typeof(resLabels), Name = "JumboJoint")]
        //public List<int> JumboJointId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Remarks")]
        public string Remarks { get; set; }

        public decimal? Difference { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Treatment")]
        [Required(ErrorMessageResourceName = "TreatmentRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short TreatmentId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "JumboJointId1")]
        public long? JumboJointId1 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "JumboJointId2")]
        public long? JumboJointId2 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "JumboJointId3")]
        public long? JumboJointId3 { get; set; }


        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int SequenceNo { get; set; }
        public bool IsPrimarySlitting { get; set; }
        public bool IsSlittingUsed { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingJointId1")]
        public long? SlittingJointId1 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingJointId2")]
        public long? SlittingJointId2 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingJointId3")]
        public long? SlittingJointId3 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "TotalPrice")]
        public decimal? TotalPrice { get; set; }

        public int? OperatorId { get; set; }
    }
}
