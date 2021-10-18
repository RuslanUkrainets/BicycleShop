using System.ComponentModel.DataAnnotations;

namespace BicycleShop.Models
{
    public class Bicycle
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string WheelDiameter { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Chain { get; set; }
        [Required]
        public string Weight { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public int? Price { get; set; }
    }
}
