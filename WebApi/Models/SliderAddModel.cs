using Entities;
using Microsoft.AspNetCore.Http;

namespace WebApi.Models
{
    public class SliderAddModel
    {
        public Slider Slider { get; set; }
        public IFormFile SliderImage { get; set; }
    }
}
