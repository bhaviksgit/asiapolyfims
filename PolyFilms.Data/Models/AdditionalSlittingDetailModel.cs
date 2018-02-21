using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PolyFilms.Common;

namespace PolyFilms.Data.Models
{
    public class AdditionalSlittingDetailModel
    {
        [ScaffoldColumn(false)]
        public long SlittingId { get; set; }

        public DateTime SlittingDate { get; set; }

        public short ShiftId { get; set; }

        public long? JumboId { get; set; }

        public long? PrimarySlittingId { get; set; }

        public short ProductId { get; set; }

        public bool IsSlittingLocked { get; set; }

        public int? SetNo { get; set; }

        public long? JumboJointId1 { get; set; }
        public long? JumboJointId2 { get; set; }
        public long? JumboJointId3 { get; set; }
        //public long? SlittingJointId1 { get; set; }
        //public long? SlittingJointId2 { get; set; }
        //public long? SlittingJointId3 { get; set; }
        //public decimal? SlitWasteWeight { get; set; }

        [UIHint("NumericTextbox")]
        [Display(ResourceType = typeof(resLabels), Name = "Thickness")]
        [Required(ErrorMessageResourceName = "ThicknessRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Thickness { get; set; }

        public int? OperatorId { get; set; }

        [ScaffoldColumn(false)]
        [Display(ResourceType = typeof(resLabels), Name = "SlittingRollNo")]
        public string SlittingRollNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Order")]
        //[UIHint("OrderByJumboComboBox")]
        //[Required(ErrorMessageResourceName = "OrderRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public long? OrderId { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(ResourceType = typeof(resLabels), Name = "Location")]
        [Required(ErrorMessageResourceName = "LocationRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(1, ErrorMessageResourceName = "LocationSlittingLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Location { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Length")]
        [Required(ErrorMessageResourceName = "LengthRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Length { get; set; }

        [Range(0, 9999, ErrorMessage = "Invalid width")]
        [Display(ResourceType = typeof(resLabels), Name = "Width")]
        [Required(ErrorMessageResourceName = "WidthRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Width { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Od")]
        [Required(ErrorMessageResourceName = "OdRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Od { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Grossweight")]
        [Required(ErrorMessageResourceName = "GrossweightRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Grossweight { get; set; }

        [UIHint("NumericTextbox")]
        //[ScaffoldColumn(false)]
        [Display(ResourceType = typeof(resLabels), Name = "Netweight")]
        public decimal? Netweight { get; set; }

        //[UIHint("CoreComboBox")]
        [Display(ResourceType = typeof(resLabels), Name = "Core")]
        [Required(ErrorMessageResourceName = "CoreRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short CoreId { get; set; }

        [UIHint("NumericTextbox")]
        [Display(ResourceType = typeof(resLabels), Name = "CoreWeight")]
        public decimal? CoreWeight { get; set; }

        [UIHint("GridForeignKey")]
        [Display(ResourceType = typeof(resLabels), Name = "Quality")]
        [Required(ErrorMessageResourceName = "QualityRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short Quality { get; set; }

        [UIHint("IntNumericTextBox")]
        //[Required(ErrorMessageResourceName = "JointRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Display(ResourceType = typeof(resLabels), Name = "Joint")]
        public int? Joint { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Remarks")]
        public string Remarks { get; set; }

        public decimal? Difference { get; set; }

        [UIHint("GridForeignKey")]
        [Display(ResourceType = typeof(resLabels), Name = "Treatment")]
        [Required(ErrorMessageResourceName = "TreatmentRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short TreatmentId { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int SequenceNo { get; set; }
        public bool IsPrimarySlitting { get; set; }
        public bool IsSlittingUsed { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "TotalPrice")]
        public decimal? TotalPrice { get; set; }

        
    }
}
