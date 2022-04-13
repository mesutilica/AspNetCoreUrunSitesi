using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        [DisplayName("Adınız"), Required, StringLength(50)]
        public string Name { get; set; }
        [DisplayName("Soyadınız"), Required, StringLength(50)]
        public string Surname { get; set; }
        [DisplayName("Mail Adresiniz"), Required, StringLength(50)]
        public string Email { get; set; }
        [DisplayName("Telefon"), StringLength(15)]
        public string Phone { get; set; }
        [DisplayName("Kullanıcı Adı"), StringLength(50), Required]
        public string Username { get; set; }
        [DisplayName("Şifre"), StringLength(150), Required]
        public string Password { get; set; }
        [DisplayName("Durum")]
        public bool IsActive { get; set; }
        [DisplayName("Admin?")]
        public bool IsAdmin { get; set; }
        [DisplayName("Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
