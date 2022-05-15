using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), Required(ErrorMessage = "Başlık Boş Geçilemez!"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "İçerik"), Required(ErrorMessage = "İçerik Boş Geçilemez!"), DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Display(Name = "Resim"), StringLength(150)]
        public string Image { get; set; }
        [Display(Name = "Ürün Fiyatı"), Required(ErrorMessage = "Ürün Fiyatı Boş Geçilemez!")]
        public decimal Price { get; set; }
        [Display(Name = "Ürün Stok"), Required(ErrorMessage = "Ürün Stok Boş Geçilemez!")]
        public int Stock { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Ürün Kategorisi")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Display(Name = "Ürün Markası")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
