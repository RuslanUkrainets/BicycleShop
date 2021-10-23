using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BicycleShop.Models.ViewModels
{
    public class BicycleListViewModel
    {
        public List<Bicycle> Bicycles { get; set; }
        public SelectList Years { get; set; }
        public int Page { get; set; }
    }
}
