using PolyFilms.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.Models
{
    public class UserModel
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

      
        [Display(ResourceType = typeof(resLabels), Name = "Name", Order = 1)]
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(100, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(resLabels), Name = "Password", Order = 3)]
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(15, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Password { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Role", Order = 4)]
        [Required(ErrorMessageResourceName = "RoleRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("GridForeignKey")]
        public int RoleId { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Username", Order = 2)]
        [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(50, ErrorMessageResourceName = "UsernameLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Display(ResourceType = typeof(resLabels), Name = "Email", Order = 5)]
        [StringLength(50, ErrorMessageResourceName = "EmailLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [UIHint("String")]
        public string EmailAddress { get; set; }


        [ScaffoldColumn(false)]
        public string Token { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? TokenExpiryDateTime { get; set; }

        [Display(ResourceType = typeof(resLabels), Name = "Status", Order = 6)]
        public bool IsActive { get; set; }

        [ScaffoldColumn(false)]
        public bool IsSuperAdmin { get; set; }
    }
}
