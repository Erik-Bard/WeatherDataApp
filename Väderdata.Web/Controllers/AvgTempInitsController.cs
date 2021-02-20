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
    public class AvgTempInitsController : Controller
    {
        private readonly WeatherContext _context;

        public AvgTempInitsController(WeatherContext context)
        {
            _context = context;
        }

        // GET: AvgTempInits
        public async Task<IActionResult> Index()
        {
            return View(await _context.avgTempInit.ToListAsync());
        }

        // GET: AvgTempInits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avgTempInit = await _context.avgTempInit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avgTempInit == null)
            {
                return NotFound();
            }

            return View(avgTempInit);
        }

        // GET: AvgTempInits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvgTempInits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectDate,Plats")] AvgTempInit avgTempInit)
        {
            if (ModelState.IsValid)
            {
                var tempDisplay = AvgTempInit.Calculate(_context, avgTempInit.SelectDate, avgTempInit.Plats);
                avgTempInit.AverageTemperature = tempDisplay;
                _context.Add(avgTempInit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avgTempInit);
        }

        // GET: AvgTempInits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avgTempInit = await _context.avgTempInit.FindAsync(id);
            if (avgTempInit == null)
            {
                return NotFound();
            }
            return View(avgTempInit);
        }

        // POST: AvgTempInits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectDate")] AvgTempInit avgTempInit)
        {
            if (id != avgTempInit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avgTempInit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvgTempInitExists(avgTempInit.Id))
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
            return View(avgTempInit);
        }

        // GET: AvgTempInits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avgTempInit = await _context.avgTempInit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avgTempInit == null)
            {
                return NotFound();
            }

            return View(avgTempInit);
        }

        // POST: AvgTempInits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avgTempInit = await _context.avgTempInit.FindAsync(id);
            _context.avgTempInit.Remove(avgTempInit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvgTempInitExists(int id)
        {
            return _context.avgTempInit.Any(e => e.Id == id);
        }
    }
}
