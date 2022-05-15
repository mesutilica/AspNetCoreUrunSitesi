using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Contact : IEntity
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
        [Display(Name = "Mesajınız"), Required(ErrorMessage = "Bu alan gereklidir"), DataType(DataType.MultilineText)]
        public string Message { get; set; }
        [Display(Name = "Mesaj Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
