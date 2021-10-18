using BicycleShop.Data;
using BicycleShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BicycleShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly BicycleContext _context;

        public AdminController(BicycleContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Bicycles.ToList());
        }

        public IActionResult Remove(int id)
        {
            var bicycle = _context.Bicycles.FirstOrDefault(x => x.Id == id);
            if(bicycle != null)
            {
                _context.Bicycles.Remove(bicycle);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create(int? id = null)
        {
            if(id != null)
            {
                var bicycle = _context.Bicycles.FirstOrDefault(x => x.Id == id);
                return View(bicycle);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Bicycle bicycle)
        {
            if(!ModelState.IsValid) return View(bicycle);

            if(bicycle.Id == null)
                _context.Bicycles.Add(bicycle);
            else
                _context.Bicycles.Update(bicycle);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
