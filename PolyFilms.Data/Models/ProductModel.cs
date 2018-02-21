using PolyFilms.Common;
using PolyFilms.Data.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.Models
{
    public class ProductModel : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public short ProductId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "FilmType", Order = 1)]
        [Required(ErrorMessageResourceName = "FilmTypeRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(50, ErrorMessageResourceName = "FilmTypeLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string FilmType { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "MainFeatures", Order = 2)]
        [Required(ErrorMessageResourceName = "MainFeaturesRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(50, ErrorMessageResourceName = "MainFeaturesLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string MainFeatures { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Thickness", Order = 3)]
        [Required(ErrorMessageResourceName = "ThicknessRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Thickness { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "PreTreatment", Order = 4)]
        [Required(ErrorMessageResourceName = "PreTreatmentRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(50, ErrorMessageResourceName = "PreTreatmentLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string PreTreatment { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "MainApplication", Order = 5)]
        [Required(ErrorMessageResourceName = "MainApplicationRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(50, ErrorMessageResourceName = "MainApplicationLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string MainApplication { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Status", Order = 6)]
        public bool IsActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CustomRepository.CheckProductExistsOrNot(FilmType, (decimal)Thickness,ProductId))
            {
                var fieldName = new[] { "FilmType" };
                yield return new ValidationResult("Product With Same micron Already Exists.", fieldName);
            }
        }
    }
}
