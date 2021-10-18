using BicycleShop.Data;
using BicycleShop.Extentions;
using BicycleShop.Models;
using BicycleShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BicycleShop.Controllers
{
    public class CartController : Controller
    {
        private readonly BicycleContext _context;
        public CartController(BicycleContext context)
        {
            _context = context;
        }
        public IActionResult Index(string returnUrl)
        {
            var cart = GetCart();

            if(string.IsNullOrEmpty(returnUrl)) returnUrl = "/Home/Index";

            var viewModel = new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        public IActionResult AddToCart(int id, string returnUrl)
        {
            var bicycle = _context.Bicycles.FirstOrDefault(x => x.Id == id);
            if(bicycle != null)
            {
                var cart = GetCart();
                cart.AddItem(bicycle, 1);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult RemoveFromCart(int id, string returnUrl)
        {
            var bicycle = _context.Bicycles.FirstOrDefault(x => x.Id == id);
            if (bicycle != null)
            {
                var cart = GetCart();
                cart.RemoveLine(bicycle);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return cart;
        }
    }
}
