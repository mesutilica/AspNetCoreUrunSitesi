using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "İçerik"), DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Display(Name = "Resim"), StringLength(150), Required(ErrorMessage = "Bu alan gereklidir")]
        public string Image { get; set; }
        [Display(Name = "Resim Link"), StringLength(100)]
        public string Link { get; set; }
    }
}
