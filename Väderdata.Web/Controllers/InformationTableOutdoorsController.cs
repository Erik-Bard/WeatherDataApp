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
    public class InformationTableOutdoorsController : Controller
    {
        private readonly WeatherContext _context;

        public InformationTableOutdoorsController(WeatherContext context)
        {
            _context = context;
        }

        public ActionResult Index(string sort, string search)
        {
            ViewData["SortDate"] = String.IsNullOrEmpty(sort) ? "Date-Desc" : "";
            ViewData["SortTemp"] = sort == "Temperature" ? "TemperatureDescending" : "Temperature";
            ViewData["SortHum"] = sort == "Humidity" ? "HumidityDescending" : "Humidity";
            ViewData["SortMould"] = sort == "MouldRisk" ? "MouldRiskDescending" : "MouldRisk";
            IEnumerable<InformationTableOutdoor> OutdoorModel = GetDataOutdoor();

            switch (sort)
            {
                case "Date-Desc":
                    OutdoorModel = OutdoorModel.OrderByDescending(x => x.SelectDate);
                    break;
                case "Temperature":
                    OutdoorModel = OutdoorModel.OrderBy(x => x.AvgTemp);
                    break;
                case "TemperatureDescending":
                    OutdoorModel = OutdoorModel.OrderByDescending(x => x.AvgTemp);
                    break;
                case "Humidity":
                    OutdoorModel = OutdoorModel.OrderBy(x => x.AvgHum);
                    break;
                case "HumidityDescending":
                    OutdoorModel = OutdoorModel.OrderByDescending(x => x.AvgHum);
                    break;
                case "MouldRisk":
                    OutdoorModel = OutdoorModel.OrderBy(x => x.MouldRank);
                    break;
                case "MouldRiskDescending":
                    OutdoorModel = OutdoorModel.OrderByDescending(x => x.MouldRank);
                    break;
                default:
                    OutdoorModel = OutdoorModel.OrderBy(x => x.SelectDate);
                    break;

            }
            if (!String.IsNullOrEmpty(search))
            {
                OutdoorModel = OutdoorModel.Where(s => s.SelectDate == DateTime.Parse(search));
            }
            return View(OutdoorModel);
        }

        public List<InformationTableOutdoor> GetDataOutdoor()
        {
            var OutdoorModel = new List<InformationTableOutdoor>();
            var dates = new List<DateTime>();

            foreach (var item in _context.AvgTempAndHumidities.ToList())
            {
                if (item.Plats == "Ute")
                {
                    dates.Add(item.SelectDate);
                }
                else
                {
                    continue;
                }
            }

            var OrderedDates = dates.OrderByDescending(x => x).ToList();
            var firstDate = OrderedDates.Last();
            var lastDate = OrderedDates.First();

            for (DateTime date = firstDate; date <= lastDate; date = date.AddDays(1))
            {
                var TempHum = (from t in _context.AvgTempAndHumidities
                               where t.SelectDate == date
                               where t.Plats == "Ute"
                               select t).ToList();
                var Mould = (from m in _context.MouldRisks
                             where m.SelectDate == date
                             where m.Plats == "Ute"
                             select m).ToList();
                double avgTemp = 0;
                double avgHum = 0;
                string mouldRisk = "";
                int mouldRank = 0;
                if (TempHum.Count() > 0)
                {
                    avgTemp = double.Parse((TempHum[0].AverageTemperature).ToString());
                    avgHum = double.Parse((TempHum[0].AverageHumidity).ToString());
                }
                if (Mould.Count() > 0)
                {
                    mouldRisk = (Mould[0].RiskFörMögel).ToString();
                    mouldRank = Mould[0].MögelIndex;
                }

                var line = new InformationTableOutdoor
                {
                    SelectDate = new DateTime(date.Year, date.Month, date.Day),
                    AvgHum = Math.Round(avgHum, 2),
                    AvgTemp = Math.Round(avgTemp, 2),
                    MouldRisk = mouldRisk,
                    MouldRank = mouldRank
                };
                OutdoorModel.Add(line);
            }
            return OutdoorModel;
        }


        // GET: InformationTableOutdoors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationTableOutdoor = await _context.InformationTableOutdoor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informationTableOutdoor == null)
            {
                return NotFound();
            }

            return View(informationTableOutdoor);
        }

        // GET: InformationTableOutdoors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InformationTableOutdoors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectDate,AvgTemp,AvgHum,MouldRisk")] InformationTableOutdoor informationTableOutdoor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informationTableOutdoor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(informationTableOutdoor);
        }

        // GET: InformationTableOutdoors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationTableOutdoor = await _context.InformationTableOutdoor.FindAsync(id);
            if (informationTableOutdoor == null)
            {
                return NotFound();
            }
            return View(informationTableOutdoor);
        }

        // POST: InformationTableOutdoors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectDate,AvgTemp,AvgHum,MouldRisk")] InformationTableOutdoor informationTableOutdoor)
        {
            if (id != informationTableOutdoor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informationTableOutdoor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationTableOutdoorExists(informationTableOutdoor.Id))
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
            return View(informationTableOutdoor);
        }

        // GET: InformationTableOutdoors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationTableOutdoor = await _context.InformationTableOutdoor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informationTableOutdoor == null)
            {
                return NotFound();
            }

            return View(informationTableOutdoor);
        }

        // POST: InformationTableOutdoors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informationTableOutdoor = await _context.InformationTableOutdoor.FindAsync(id);
            _context.InformationTableOutdoor.Remove(informationTableOutdoor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationTableOutdoorExists(int id)
        {
            return _context.InformationTableOutdoor.Any(e => e.Id == id);
        }
    }
}
