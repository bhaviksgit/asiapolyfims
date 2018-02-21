using PolyFilms.Common;
using PolyFilms.Data.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.Models
{
    public class OrderDetailModel :IValidatableObject
    {
        [ScaffoldColumn(false)]
        public int OrderDetailId { get; set; }

        [ScaffoldColumn(false)]
        public long OrderId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Product")]
        [Required(ErrorMessageResourceName = "ProductRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("ComboBoxProduct")]
        public short ProductId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Thickness")]
        [UIHint("readOnlyNumeric")]
        public decimal? Thickness { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Width")]
        [Required(ErrorMessageResourceName = "WidthRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Width { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Od")]
        [Required(ErrorMessageResourceName = "OdRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Od { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Quantity")]
        [Required(ErrorMessageResourceName = "QuantityRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("NumericTextbox")]
        public decimal? Quantity { get; set; }

        [ScaffoldColumn(false)]
        public decimal TotalSlittingDone { get; set; }

        [ScaffoldColumn(false)]
        [Display(ResourceType = typeof(resLabels), Name = "RemainingSlitting")]
        public decimal? RemainingSlitting { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           
                if (CustomRepository.CheckProductExistsINOrder(OrderId,ProductId, OrderDetailId))
                {
                    var fieldName = new[] { "ProductId" };
                    yield return new ValidationResult("Product Already Exists.", fieldName);
                }
            
        }
    }
}
