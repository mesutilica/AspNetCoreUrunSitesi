using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), Required(ErrorMessage = "Başlık Boş Geçilemez!"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "İçerik"), Required(ErrorMessage = "İçerik Boş Geçilemez!"), DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Display(Name = "Resim"), StringLength(150)]
        public string Image { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "İçerik Kategorisi")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
