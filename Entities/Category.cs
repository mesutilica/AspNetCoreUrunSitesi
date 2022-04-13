using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [DisplayName("Kategori Adı"), Required, StringLength(50)]
        public string Name { get; set; }
        [DisplayName("Kategori Açıklaması"), DataType(DataType.MultilineText)]
        public string Descripton { get; set; }
        [DisplayName("Durum")]
        public bool IsActive { get; set; }
        [DisplayName("Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now; // DateTime? ile bu alanın boş geçilebilir olmasını istedik yoksa zorunlu olurdu
    }
}
