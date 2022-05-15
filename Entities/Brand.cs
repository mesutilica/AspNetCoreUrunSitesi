using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Marka Adı"), Required(ErrorMessage = "Bu alan gereklidir"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Marka Logosu"), StringLength(150)]
        public string Logo { get; set; }
        [Display(Name = "Marka Açıklaması"), DataType(DataType.MultilineText)] // çoklu satır desteği
        public string Descripton { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
