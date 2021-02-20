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
    public class MeteorologiskSäsongController : Controller
    {
        private readonly WeatherContext _context;

        public MeteorologiskSäsongController(WeatherContext context)
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

            var meteorologiskSäsong = await _context.WeatherSeason
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meteorologiskSäsong == null)
            {
                return NotFound();
            }

            return View(meteorologiskSäsong);
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
        public async Task<IActionResult> Create([Bind("Id,HöstDatum,VinterDatum")] MeteorologiskSäsong meteorologiskSäsong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meteorologiskSäsong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meteorologiskSäsong);
        }

        // GET: MeteorologiskSäsong/Edit/5
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

        // POST: MeteorologiskSäsong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HöstDatum,VinterDatum")] MeteorologiskSäsong meteorologiskSäsong)
        {
            if (id != meteorologiskSäsong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meteorologiskSäsong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeteorologiskSäsongExists(meteorologiskSäsong.Id))
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
            return View(meteorologiskSäsong);
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
            var meteorologiskSäsong = await _context.WeatherSeason.FindAsync(id);
            _context.WeatherSeason.Remove(meteorologiskSäsong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeteorologiskSäsongExists(int id)
        {
            return _context.WeatherSeason.Any(e => e.Id == id);
        }
    }
}
