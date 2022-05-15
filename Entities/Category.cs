using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı"), Required(ErrorMessage = "Bu alan gereklidir"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Kategori Açıklaması"), DataType(DataType.MultilineText)]
        public string Descripton { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now; // DateTime? ile bu alanın boş geçilebilir olmasını istedik yoksa zorunlu olurdu
        [Display(Name = "Üst Kategorisi")]
        public int ParentId { get; set; }
        [Display(Name = "Üst Menüde Göster")]
        public bool IsTopMenu { get; set; }
        [Display(Name = "Sıra No")]
        public int OrderNo { get; set; }
    }
}
