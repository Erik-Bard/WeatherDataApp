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
    public class BalconyDoorsController : Controller
    {
        private readonly WeatherContext _context;

        public BalconyDoorsController(WeatherContext context)
        {
            _context = context;
        }

        // GET: BalconyDoors
        public async Task<IActionResult> Index()
        {
            return View(await _context.BalconyDoor.ToListAsync());
        }

        // GET: BalconyDoors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balconyDoor = await _context.BalconyDoor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (balconyDoor == null)
            {
                return NotFound();
            }

            return View(balconyDoor);
        }

        // GET: BalconyDoors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BalconyDoors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OpeningDoor,ClosingDoor")] BalconyDoor balconyDoor)
        {
            if (ModelState.IsValid)
            {
                //TimeSpan timeDifference = BalconyDoor.CalculateOpenTime(_context, balconyDoor.OpeningDoor, balconyDoor.ClosingDoor);
                //balconyDoor.TimeSpan = timeDifference;
                //_context.Add(balconyDoor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(balconyDoor);
        }

        // GET: BalconyDoors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balconyDoor = await _context.BalconyDoor.FindAsync(id);
            if (balconyDoor == null)
            {
                return NotFound();
            }
            return View(balconyDoor);
        }

        // POST: BalconyDoors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OpeningDoor,ClosingDoor")] BalconyDoor balconyDoor)
        {
            if (id != balconyDoor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(balconyDoor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BalconyDoorExists(balconyDoor.Id))
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
            return View(balconyDoor);
        }

        // GET: BalconyDoors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balconyDoor = await _context.BalconyDoor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (balconyDoor == null)
            {
                return NotFound();
            }

            return View(balconyDoor);
        }

        // POST: BalconyDoors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var balconyDoor = await _context.BalconyDoor.FindAsync(id);
            _context.BalconyDoor.Remove(balconyDoor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BalconyDoorExists(int id)
        {
            return _context.BalconyDoor.Any(e => e.Id == id);
        }
    }
}
