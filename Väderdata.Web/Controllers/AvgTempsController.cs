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
    public class AvgTempsController : Controller
    {
        private readonly WeatherContext _context;

        public AvgTempsController(WeatherContext context)
        {
            _context = context;
        }

        // GET: AvgTemps
        public async Task<IActionResult> Index()
        {
            return View(await _context.AvgTemp.ToListAsync());
        }

        // GET: AvgTemps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avgTemp = await _context.AvgTemp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avgTemp == null)
            {
                return NotFound();
            }

            return View(avgTemp);
        }

        // GET: AvgTemps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvgTemps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectDate,Plats")] AvgTemp avgTemp)
        {
            if (ModelState.IsValid)
            {
                // VÄLJ DATUM
                // => 24h
                // => Tm=(aT07+bT13+cT19+dTx+eTn)/100   // FORMEL FÖR MEDELTEMPERATUR
                // => SKRIV UT I WEBLÄSAREN
                AvgTempInit.Calculate(_context, avgTemp.SelectDate, avgTemp.Plats);
                //avgTemp.CalculateAvgTemp(avgTemp.SelectDate, avgTemp.Plats);
                _context.Add(avgTemp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avgTemp);
        }

        // GET: AvgTemps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avgTemp = await _context.AvgTemp.FindAsync(id);
            if (avgTemp == null)
            {
                return NotFound();
            }
            return View(avgTemp);
        }

        // POST: AvgTemps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectDate")] AvgTemp avgTemp)
        {
            if (id != avgTemp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avgTemp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvgTempExists(avgTemp.Id))
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
            return View(avgTemp);
        }

        // GET: AvgTemps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avgTemp = await _context.AvgTemp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avgTemp == null)
            {
                return NotFound();
            }

            return View(avgTemp);
        }

        // POST: AvgTemps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avgTemp = await _context.AvgTemp.FindAsync(id);
            _context.AvgTemp.Remove(avgTemp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvgTempExists(int id)
        {
            return _context.AvgTemp.Any(e => e.Id == id);
        }
    }
}
