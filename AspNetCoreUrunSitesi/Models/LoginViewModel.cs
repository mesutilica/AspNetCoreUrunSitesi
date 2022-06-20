using System.ComponentModel.DataAnnotations;

namespace AspNetCoreUrunSitesi.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Kullanıcı Adı"), StringLength(50), Required(ErrorMessage = "Bu alan gereklidir")]
        public string Username { get; set; }
        [Display(Name = "Şifre"), StringLength(150), Required(ErrorMessage = "Bu alan gereklidir")]//, DataType(DataType.Password)
        public string Password { get; set; }
    }
}
