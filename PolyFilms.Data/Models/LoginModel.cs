using System.ComponentModel.DataAnnotations;

namespace PolyFilms.Data.Models
{
    public class LoginModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username must be less than or equal to 15 characters")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(15, ErrorMessage = "Password must be less than or equal to 15 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        
    }
}
