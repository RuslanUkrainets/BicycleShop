using BicycleShop.Data;
using BicycleShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BicycleShop.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class BicyclesController : Controller
    {
        private readonly BicycleContext _context;

        public BicyclesController(BicycleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bicycle>>> Get() => await _context.Bicycles.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Bicycle>> Get(int id)
        {
            var bicycle = await _context.Bicycles.FirstOrDefaultAsync(x => x.Id == id);
            if (bicycle == null)
                return NotFound();
            return bicycle;
        }

        [HttpPost]
        public async Task<ActionResult<Bicycle>> Post([FromForm] Bicycle bicycle)
        {            
            _context.Bicycles.Add(bicycle);
            await _context.SaveChangesAsync();
            return Ok(bicycle);
        }
        
        [HttpPut]
        public async Task<ActionResult<Bicycle>> Put(Bicycle bicycle)
        {
            _context.Bicycles.Update(bicycle);
            await _context.SaveChangesAsync();
            return Ok(bicycle);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bicycle = await _context.Bicycles.FirstOrDefaultAsync(x => x.Id == id);
            if(bicycle == null)
                return NotFound();

            _context.Bicycles.Remove(bicycle);
            await _context.SaveChangesAsync();
            return Ok(bicycle);
        }
    }
}
