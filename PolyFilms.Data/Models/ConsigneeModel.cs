using System.Collections.Generic;
using PolyFilms.Common;
using System.ComponentModel.DataAnnotations;
using PolyFilms.Data.Repositories;

namespace PolyFilms.Data.Models
{
    public class ConsigneeModel :IValidatableObject
    {
        [ScaffoldColumn(false)]
        public long ConsigneeId { get; set; }

        [ScaffoldColumn(false)]
        public long BuyerId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Name", Order = 1)]
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(50, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "DeliveryAddress", Order = 2)]
        [Required(ErrorMessageResourceName = "DeliveryAddressRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("TextArea")]
        public string DeliveryAddress { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Location", Order = 3)]
        [StringLength(50, ErrorMessageResourceName = "LocationLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "LocationRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Location { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "PhoneNumber", Order = 4)]
        [StringLength(20, ErrorMessageResourceName = "PhoneNumberLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "PhoneNumberRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string PhoneNumber { get; set; }

        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Display(ResourceType = typeof(resLabels), Name = "Email", Order = 5)]
        [StringLength(50, ErrorMessageResourceName = "EmailLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("Email")]
        public string EmailAddress { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "GSTNO", Order = 6)]
        [StringLength(50, ErrorMessageResourceName = "GSTNOLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "GSTNORequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Gstnumber { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "PanNumber", Order = 7)]
        [StringLength(50, ErrorMessageResourceName = "PanNumberLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "PanNumberRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string PanNumber { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Status", Order = 8)]
        public bool IsActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CustomRepository.CheckConsigneeExistsOrNot(Gstnumber, PanNumber, PhoneNumber, ConsigneeId))
            {
                var fieldName = new[] { "Name" };
                yield return new ValidationResult("Consignee is Already Exists.", fieldName);
            }
        }
    }
}
