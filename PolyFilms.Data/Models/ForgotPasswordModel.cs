using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PolyFilms.Common;

namespace PolyFilms.Data.Models
{
    public class ForgotPasswordModel 
    {
        
        public int UserId { get; set; }
        
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(resLabels), Name = "Password")]
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        [StringLength(15, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        [Display(ResourceType = typeof(resLabels), Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceName = "ConfirmPasswordRequired", ErrorMessageResourceType = typeof(resValidationMsg))]
        public string ConfirmPassword { get; set; }


       
    }
}
