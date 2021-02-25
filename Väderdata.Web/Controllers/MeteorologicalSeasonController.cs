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

        // GET: MeteorologicalSeason
        public async Task<IActionResult> Index()
        {
            return View(await _context.WeatherSeason.ToListAsync());
        }

        // GET: MeteorologicalSeason/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: MeteorologicalSeason/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeteorologicalSeason/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HöstStart,HöstDatum,VinterStart,VinterDatum")] MeteorologicalSeason meteorologicalSeason)
        {
            if (ModelState.IsValid)
            {
                var Autumn = MeteorologicalSeason.AutumnDate(_context, meteorologicalSeason.HöstDatum);
                var Winter = MeteorologicalSeason.WinterDate(_context, meteorologicalSeason.VinterDatum);
                if(Autumn == null)
                {
                    meteorologicalSeason.HöstStart = "Hösten Kom aldrig detta år";
                }
                if( Winter == null)
                {
                    meteorologicalSeason.VinterStart = "Vintern Kom aldrig detta år";
                }
                if(Winter != null )
                {
                    meteorologicalSeason.VinterStart = "Vintern Faller på detta datum i år";
                }
                if (Autumn != null)
                {
                    meteorologicalSeason.HöstStart = "Hösten Faller på detta datum i år";
                }
                meteorologicalSeason.HöstDatum = Autumn;
                meteorologicalSeason.VinterDatum = Winter;
                _context.Add(meteorologicalSeason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meteorologicalSeason);
        }

        // GET: MeteorologicalSeason/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meteorologiskSäsong = await _context.WeatherSeason.FindAsync(id);
            if (meteorologiskSäsong == null)
            {
                return NotFound();
            }
            return View(meteorologiskSäsong);
        }

        // POST: MeteorologicalSeason/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HöstDatum,VinterDatum")] MeteorologicalSeason meteorologicalSeason)
        {
            if (id != meteorologicalSeason.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meteorologicalSeason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeteorologicalSeasonExists(meteorologicalSeason.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meteorologicalSeason);
        }

        // GET: MeteorologicalSeason/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: MeteorologicalSeason/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meteorologicalSeason = await _context.WeatherSeason.FindAsync(id);
            _context.WeatherSeason.Remove(meteorologicalSeason);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeteorologicalSeasonExists(int id)
        {
            return _context.WeatherSeason.Any(e => e.Id == id);
        }
    }
}
