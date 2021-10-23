using BicycleShop.Data;
using BicycleShop.Extentions;
using BicycleShop.Infrastructure.Enums;
using BicycleShop.Models;
using BicycleShop.Models.ViewModels;
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
        private const int TAKE_ITEMS = 10;
        public HomeController(BicycleContext context)
        {
            _context = context;
        }

        //public IActionResult Index(SortType sortType, string year = "all")
        //{
        //    List<Bicycle> bicycles = null;

        //    List<string> selectedList = new List<string>() { "all" };

        //    switch (sortType)
        //    {
        //        case SortType.NameAsc:
        //            bicycles = _context.Bicycles.OrderBy(x => x.Name).ToList();
        //            break;
        //        case SortType.WheelDiameterAsc:
        //            bicycles = _context.Bicycles.OrderBy(x => x.WheelDiameter).ToList();
        //            break;
        //        case SortType.ColorAsc:
        //            bicycles = _context.Bicycles.OrderBy(x => x.Color).ToList();
        //            break;
        //        case SortType.ChainAsc:
        //            bicycles = _context.Bicycles.OrderBy(x => x.Chain).ToList();
        //            break;
        //        case SortType.WeightAsc:
        //            bicycles = _context.Bicycles.OrderBy(x => x.Weight).ToList();
        //            break;
        //        case SortType.YearAsc:
        //            bicycles = _context.Bicycles.OrderBy(x => x.Year).ToList();
        //            break;
        //        case SortType.PriceAsc:
        //            bicycles = _context.Bicycles.OrderBy(x => x.Price).ToList();
        //            break;
        //        default:
        //            bicycles = _context.Bicycles.ToList();
        //            break;
        //    }

        //    if (year != "all")
        //    {
        //        bicycles = bicycles.Where(x => x.Year == year).ToList();
        //    }

        //    selectedList.AddRange(_context.Bicycles.Select(x => x.Year).Distinct());

        //    return View(new BicycleListViewModel { Bicycles = bicycles, Years = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectedList)});
        //}

        public IActionResult Index() => View();

        public IActionResult BicyclesList(SortType sortType, string search = "", string year = "all", int page = 1)
        {
            List<Bicycle> bicycles = _context.Bicycles.ToList();            

            List<string> selectedList = new() { "all" };

            bicycles = Common.GetItemsOfPage(bicycles, ref page).ToList();

            switch (sortType)
            {
                case SortType.NameAsc:
                    bicycles = bicycles.OrderBy(x => x.Name).ToList();
                    break;
                case SortType.WheelDiameterAsc:
                    bicycles = bicycles.OrderBy(x => x.WheelDiameter).ToList();
                    break;
                case SortType.ColorAsc:
                    bicycles = bicycles.OrderBy(x => x.Color).ToList();
                    break;
                case SortType.ChainAsc:
                    bicycles = bicycles.OrderBy(x => x.Chain).ToList();
                    break;
                case SortType.WeightAsc:
                    bicycles = bicycles.OrderBy(x => x.Weight).ToList();
                    break;
                case SortType.YearAsc:
                    bicycles = bicycles.OrderBy(x => x.Year).ToList();
                    break;
                case SortType.PriceAsc:
                    bicycles = bicycles.OrderBy(x => x.Price).ToList();
                    break;
                case SortType.DefaultAsc:
                    break;
                default:
                    break;
            }

            if (year != "all")
            {
                bicycles = bicycles.Where(x => x.Year == year).ToList();
            }

            selectedList.AddRange(_context.Bicycles.Select(x => x.Year).Distinct());

            if(!string.IsNullOrEmpty(search))
            {
                bicycles = bicycles.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView(new BicycleListViewModel 
            { 
                Bicycles = bicycles, 
                Years = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(selectedList),
                Page = page
            });
        }
    }
}
