using BicycleShop.Data;
using BicycleShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly BicycleContext _context;
        public HomeController(BicycleContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bicycles = _context.Bicycles.ToList();
            return View(bicycles);
        }
    }
}
