using System.Collections.Generic;
using PolyFilms.Common;
using System.ComponentModel.DataAnnotations;
using PolyFilms.Data.Repositories;

namespace PolyFilms.Data.Models
{
    public class RolesModel :IValidatableObject
    {
        [ScaffoldColumn(false)]
        public int RoleId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Role", Order = 1)]
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(100, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string RoleName { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Status", Order = 2)]
        public bool IsActive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CustomRepository.CheckRoleExistsOrNot(RoleName,RoleId))
            {
                var fieldName = new[] { "RoleName" };
                yield return new ValidationResult("Role is Already Exists.", fieldName);
            }
        }
    }
}
