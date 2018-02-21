using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PolyFilms.Common;

namespace PolyFilms.Data.Models
{
    public class BasicSlittingDetailModel
    {
        [ScaffoldColumn(false)]
        public long SlittingId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingDate")]
        [Required(ErrorMessageResourceName = "SlittingDateRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public DateTime SlittingDate { get; set; }

        
        [Display(ResourceType = typeof(resLabels), Name = "Shift")]
        [Required(ErrorMessageResourceName = "ShiftRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short ShiftId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Jumbo")]
        public long? JumboId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "PrimarySlitting")]
        public long? PrimarySlittingId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Product")]
        [Required(ErrorMessageResourceName = "ProductRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public short ProductId { get; set; }

        //[Display(ResourceType = typeof(resLabels), Name = "Thickness")]
        //[Required(ErrorMessageResourceName = "ThicknessRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public decimal? Thickness { get; set; }

        [Required(ErrorMessageResourceName = "SetNoRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Display(ResourceType = typeof(resLabels), Name = "SetNo")]
        public int? SetNo { get; set; }

        //[Display(ResourceType = typeof(resLabels), Name = "JumboJoint")]
        //public List<int> JumboJointId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "JumboJointId1")]
        public long? JumboJointId1 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "JumboJointId2")]
        public long? JumboJointId2 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "JumboJointId3")]
        public long? JumboJointId3 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingJointId1")]
        public long? SlittingJointId1 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingJointId2")]
        public long? SlittingJointId2 { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "SlittingJointId3")]
        public long? SlittingJointId3 { get; set; }


        [Display(ResourceType = typeof(resLabels), Name = "Operator")]
        [Required(ErrorMessageResourceName = "OperatorRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public int? OperatorId { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int SequenceNo { get; set; }
        public bool IsPrimarySlitting { get; set; }
        public bool IsSlittingUsed { get; set; }

    }
}
