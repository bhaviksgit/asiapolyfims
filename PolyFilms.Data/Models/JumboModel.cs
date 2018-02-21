using PolyFilms.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.Models
{
    public class JumboModel
    {
        [ScaffoldColumn(false)]
        public long JumboId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "JumboDate")]
        [Required(ErrorMessageResourceName = "JumboDateRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public DateTime JumboDate { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Product")]
        [Required(ErrorMessageResourceName = "ProductRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short ProductId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "JumboNo")]
        public string JumboNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "TimeIn")]
        [Required(ErrorMessageResourceName = "TimeInRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public DateTime TimeIn { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "TimeOut")]
        [Required(ErrorMessageResourceName = "TimeOutRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public DateTime TimeOut { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Length")]
        [Required(ErrorMessageResourceName = "LengthRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Length { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Width")]
        [Required(ErrorMessageResourceName = "WidthRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Width { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Weight")]
        [Required(ErrorMessageResourceName = "WeightRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Weight { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Thickness")]
        [Required(ErrorMessageResourceName = "ThicknessRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Thickness { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(ResourceType = typeof(resLabels), Name = "LineNo")]
        [StringLength(1, ErrorMessageResourceName = "LineNoLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "LineNoRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string LineNo { get; set; }

       
        [Display(ResourceType = typeof(resLabels), Name = "CoreNo")]
        [StringLength(20, ErrorMessageResourceName = "CoreNoLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "CoreNoRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string CoreNo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Shift")]
        [Required(ErrorMessageResourceName = "ShiftRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short ShiftId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Speed")]
        public decimal? Speed { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "ShiftIncharge")]
        [Required(ErrorMessageResourceName = "ShiftInchargeRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public int ShiftInchargeId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Status")]
        public short? StatusId { get; set; }

        public decimal TotalSlitJumbo { get; set; }
        public decimal? RemainingJumbo { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "JumboStatusRemarks")]
        public string JumboStatusRemarks { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(ResourceType = typeof(resLabels), Name = "Winder")]
        [Required(ErrorMessageResourceName = "WinderRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(1, ErrorMessageResourceName = "WinderLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Winder { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Treatment")]
        [Required(ErrorMessageResourceName = "TreatmentRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short TreatmentId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "WasteWeight")]
        public decimal WasteWeight { get; set; }

        public DateTime? CreatedDate { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public short? SequenceNo { get; set; }
        public bool IsJumboUsed { get; set; }
    }
}
