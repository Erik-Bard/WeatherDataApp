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
    public class SortByAverageTempController : Controller
    {
        private readonly WeatherContext _context;

        public SortByAverageTempController(WeatherContext context)
        {
            _context = context;
        }

        // GET: SortByAverageTemp
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.AvgTempAndHumidities.ToListAsync());
        //}
        public ViewResult Index()
        {
            IQueryable<AvgTempAndHumidity> Tempies = AvgTempAndHumidity.SortByHumidity(_context);

            //Order the items
            Tempies = Tempies.OrderByDescending(s => s.AverageTemperature);

            //Display View from sorted as a list of the objects
            return View(Tempies.ToList());
        }

        // GET: SortByAverageTemp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avgTempAndHumidity = await _context.AvgTempAndHumidities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avgTempAndHumidity == null)
            {
                return NotFound();
            }

            return View(avgTempAndHumidity);
        }

        // GET: SortByAverageTemp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SortByAverageTemp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectDate,AverageHumidity,AverageTemperature,Plats")] AvgTempAndHumidity avgTempAndHumidity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avgTempAndHumidity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avgTempAndHumidity);
        }

        // GET: SortByAverageTemp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avgTempAndHumidity = await _context.AvgTempAndHumidities.FindAsync(id);
            if (avgTempAndHumidity == null)
            {
                return NotFound();
            }
            return View(avgTempAndHumidity);
        }

        // POST: SortByAverageTemp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectDate,AverageHumidity,AverageTemperature,Plats")] AvgTempAndHumidity avgTempAndHumidity)
        {
            if (id != avgTempAndHumidity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avgTempAndHumidity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvgTempAndHumidityExists(avgTempAndHumidity.Id))
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
            return View(avgTempAndHumidity);
        }

        // GET: SortByAverageTemp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avgTempAndHumidity = await _context.AvgTempAndHumidities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avgTempAndHumidity == null)
            {
                return NotFound();
            }

            return View(avgTempAndHumidity);
        }

        // POST: SortByAverageTemp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avgTempAndHumidity = await _context.AvgTempAndHumidities.FindAsync(id);
            _context.AvgTempAndHumidities.Remove(avgTempAndHumidity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvgTempAndHumidityExists(int id)
        {
            return _context.AvgTempAndHumidities.Any(e => e.Id == id);
        }
    }
}
