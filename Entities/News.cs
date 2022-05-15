using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class News : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), Required(ErrorMessage = "Bu alan gereklidir"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "İçerik"), Required(ErrorMessage = "Bu alan gereklidir"), DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Display(Name = "Resim"), StringLength(150)]
        public string Image { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
