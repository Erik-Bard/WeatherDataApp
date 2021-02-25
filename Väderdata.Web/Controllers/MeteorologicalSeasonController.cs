using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Väderdata.Web.Context;
using Väderdata.Web.Data;

namespace Väderdata.Web.Controllers
{
    public class MeteorologicalSeasonController : Controller
    {
        private readonly WeatherContext _context;

        public MeteorologicalSeasonController(WeatherContext context)
        {
            _context = context;
        }

        // GET: MeteorologiskSäsong
        public async Task<IActionResult> Index()
        {
            return View(await _context.WeatherSeason.ToListAsync());
        }

        // GET: MeteorologiskSäsong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meteorologicalSeason = await _context.WeatherSeason
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meteorologicalSeason == null)
            {
                return NotFound();
            }

            return View(meteorologicalSeason);
        }

        // GET: MeteorologiskSäsong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeteorologiskSäsong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AutumnStart,AutumnDateTime,WinterStart,WinterDateTime")] MeteorologicalSeason meteorologicalSeason)
        {
            if (ModelState.IsValid)
            {
                DateTime WinterCheck = new DateTime(2016,10,01);
                DateTime AutumnCheck = new DateTime(2016,10,01);
                var Autumn = MeteorologicalSeason.AutumnDate(_context, AutumnCheck);
                var Winter = MeteorologicalSeason.WinterDate(_context, WinterCheck);
                if(Autumn == null)
                {
                    meteorologicalSeason.AutumnStart = "Hösten kom aldrig 2016";
                }
                if( Winter == null)
                {
                    meteorologicalSeason.WinterStart = "Vintern kom aldrig 2016";
                }
                if(Winter != null )
                {
                    meteorologicalSeason.WinterStart = "Vintern faller på detta datum 2016";
                }
                if (Autumn != null)
                {
                    meteorologicalSeason.AutumnStart = "Hösten faller på detta datum 2016";
                }
                meteorologicalSeason.AutumnDateTime = Autumn;
                meteorologicalSeason.WinterDateTime = Winter;
                _context.Add(meteorologicalSeason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meteorologicalSeason);
        }

        // GET: MeteorologiskSäsong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meteorologiskSäsong = await _context.WeatherSeason
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meteorologiskSäsong == null)
            {
                return NotFound();
            }

            return View(meteorologiskSäsong);
        }

        // POST: MeteorologiskSäsong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meteorologicalSeason = await _context.WeatherSeason.FindAsync(id);
            _context.WeatherSeason.Remove(meteorologicalSeason);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeteorologiskSäsongExists(int id)
        {
            return _context.WeatherSeason.Any(e => e.Id == id);
        }
    }
}
