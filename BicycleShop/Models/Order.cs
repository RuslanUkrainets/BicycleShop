using System.ComponentModel.DataAnnotations;

namespace BicycleShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactPhone { get; set; }
        public int? BicycleId { get; set; }
        public Bicycle Bicycle { get; set; }
    }
}