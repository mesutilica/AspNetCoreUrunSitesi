using Entities;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreUrunSitesi.Models
{
    public class SliderAddModel
    {
        public Slider Slider { get; set; }
        public IFormFile SliderImage { get; set; }
    }
}
