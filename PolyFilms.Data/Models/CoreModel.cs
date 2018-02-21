using System.Collections.Generic;
using PolyFilms.Common;
using System.ComponentModel.DataAnnotations;
using PolyFilms.Data.Repositories;

namespace PolyFilms.Data.Models
{
    public class CoreModel :IValidatableObject
    {
        [ScaffoldColumn(false)]
        public short CoreId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Name", Order = 1)]
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(50, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Thickness", Order = 2)]
        [Required(ErrorMessageResourceName = "ThicknessRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Thickness { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Weight", Order = 3)]
        [Required(ErrorMessageResourceName = "WeightRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Weight { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CustomRepository.CheckCoreAlreadyExists(Name, Thickness, CoreId))
            {
                var fieldName = new[] { "Name" };
                yield return new ValidationResult("Core with same Name & Thickness already Exists.", fieldName);
            }
        }
    }
}
