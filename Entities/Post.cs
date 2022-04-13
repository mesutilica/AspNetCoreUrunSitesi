using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        [DisplayName("Başlık"), Required, StringLength(50)]
        public string Name { get; set; }
        [DisplayName("İçerik"), Required, DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [DisplayName("Resim"), StringLength(50)]
        public string Image { get; set; }
        [DisplayName("Durum")]
        public bool IsActive { get; set; }
        [DisplayName("Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
