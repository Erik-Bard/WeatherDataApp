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
    public class SortByMouldRiskController : Controller
    {
        private readonly WeatherContext _context;

        public SortByMouldRiskController(WeatherContext context)
        {
            _context = context;
        }

        // GET: SortByMouldRisk
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.MögelRisks.ToListAsync());
        //}
        public ViewResult Index()
        {
            IQueryable<MögelRisk> Mögel = MögelRisk.SortByMögelRisk(_context);

            //Order the items
            Mögel = Mögel.OrderBy(s => s.MögelIndex);

            //Display View from sorted as a list of the objects
            return View(Mögel.ToList());
        }


        // GET: SortByMouldRisk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mögelRisk = await _context.MögelRisks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mögelRisk == null)
            {
                return NotFound();
            }

            return View(mögelRisk);
        }

        // GET: SortByMouldRisk/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SortByMouldRisk/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectDate,Plats,RiskFörMögel,MögelIndex")] MögelRisk mögelRisk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mögelRisk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mögelRisk);
        }

        // GET: SortByMouldRisk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mögelRisk = await _context.MögelRisks.FindAsync(id);
            if (mögelRisk == null)
            {
                return NotFound();
            }
            return View(mögelRisk);
        }

        // POST: SortByMouldRisk/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectDate,Plats,RiskFörMögel,MögelIndex")] MögelRisk mögelRisk)
        {
            if (id != mögelRisk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mögelRisk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MögelRiskExists(mögelRisk.Id))
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
            return View(mögelRisk);
        }

        // GET: SortByMouldRisk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mögelRisk = await _context.MögelRisks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mögelRisk == null)
            {
                return NotFound();
            }

            return View(mögelRisk);
        }

        // POST: SortByMouldRisk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mögelRisk = await _context.MögelRisks.FindAsync(id);
            _context.MögelRisks.Remove(mögelRisk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MögelRiskExists(int id)
        {
            return _context.MögelRisks.Any(e => e.Id == id);
        }
    }
}
