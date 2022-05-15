using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adınız"), Required(ErrorMessage = "Bu alan gereklidir"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Soyadınız"), Required(ErrorMessage = "Bu alan gereklidir"), StringLength(50)]
        public string Surname { get; set; }
        [Display(Name = "Mail Adresiniz"), Required(ErrorMessage = "Bu alan gereklidir"), StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "Telefon"), StringLength(15)]
        public string Phone { get; set; }
        [Display(Name = "Kullanıcı Adı"), StringLength(50), Required(ErrorMessage = "Bu alan gereklidir")]
        public string Username { get; set; }
        [Display(Name = "Şifre"), StringLength(150), Required(ErrorMessage = "Bu alan gereklidir"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
