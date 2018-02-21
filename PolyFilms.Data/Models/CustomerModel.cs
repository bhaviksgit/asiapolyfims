using PolyFilms.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PolyFilms.Data.Repositories;

namespace PolyFilms.Data.Models
{
    public class CustomerModel :IValidatableObject
    {
        [ScaffoldColumn(false)]
        public long CustomerId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Name", Order = 1)]
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(50, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Address", Order = 2)]
        [Required(ErrorMessageResourceName = "AddressRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        //[StringLength(100, ErrorMessageResourceName = "AddressLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("TextArea")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "DeliveryAddress", Order = 3)]
        [UIHint("TextArea")]
        public string DeliveryAddress { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Location", Order = 4)]
        [StringLength(50, ErrorMessageResourceName = "LocationLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "LocationRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Location { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "PhoneNumber", Order = 5)]
        [StringLength(20, ErrorMessageResourceName = "PhoneNumberLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "PhoneNumberRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "PhoneNumber2", Order = 6)]
        public string PhoneNumber2 { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$" ,ErrorMessageResourceName ="InvalidEmail" , ErrorMessageResourceType = typeof(resValidationMsg))]
        [Display(ResourceType = typeof(resLabels), Name = "Email", Order = 7)]
        [StringLength(50, ErrorMessageResourceName = "EmailLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("String")]
        public string EmailAddress { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "GSTNO", Order = 8)]
        [StringLength(50, ErrorMessageResourceName = "GSTNOLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "GSTNORequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Gstnumber { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "PanNumber", Order = 9)]
        [StringLength(50, ErrorMessageResourceName = "PanNumberLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "PanNumberRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string PanNumber { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Status", Order = 10)]
        public bool IsActive { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CustomRepository.CheckCustomerExistsOrNot(Gstnumber,PanNumber,PhoneNumber,CustomerId))
            {
                var fieldName = new[] { "Name" };
                yield return new ValidationResult("Customer is Already Exists.", fieldName);
            }
        }
    }
}
