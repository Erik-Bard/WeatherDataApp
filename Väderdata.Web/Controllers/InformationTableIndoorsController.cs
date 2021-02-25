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
    public class InformationTableIndoorsController : Controller
    {
        private readonly WeatherContext _context;

        public InformationTableIndoorsController(WeatherContext context)
        {
            _context = context;
        }

        public ActionResult Index(string sort, string search)
        {
            ViewData["SortDate"] = String.IsNullOrEmpty(sort) ? "Date-Desc" : "";
            ViewData["SortTemp"] = sort == "Temperature" ? "TemperatureDescending" : "Temperature";
            ViewData["SortHum"] = sort == "Humidity" ? "HumidityDescending" : "Humidity";
            ViewData["SortMould"] = sort == "MouldRisk" ? "MouldRiskDescending" : "MouldRisk";
            IEnumerable<InformationTableIndoor> IndoorModel = GetDataIndoor();

            switch(sort)
            {
                case "Date-Desc":
                    IndoorModel = IndoorModel.OrderByDescending(x => x.SelectDate);
                    break;
                case "Temperature":
                    IndoorModel = IndoorModel.OrderBy(x => x.AvgTemp);
                    break;
                case "TemperatureDescending":
                    IndoorModel = IndoorModel.OrderByDescending(x => x.AvgTemp);
                    break;
                case "Humidity":
                    IndoorModel = IndoorModel.OrderBy(x => x.AvgHum);
                    break;
                case "HumidityDescending":
                    IndoorModel = IndoorModel.OrderByDescending(x => x.AvgHum);
                    break;
                case "MouldRisk":
                    IndoorModel = IndoorModel.OrderBy(x => x.MouldRank);
                    break;
                case "MouldRiskDescending":
                    IndoorModel = IndoorModel.OrderByDescending(x => x.MouldRank);
                    break;
                default:
                    IndoorModel = IndoorModel.OrderBy(x => x.SelectDate);
                    break;

            }
            if (!String.IsNullOrEmpty(search))
            {
                IndoorModel = IndoorModel.Where(s => s.SelectDate == DateTime.Parse(search));
            }
            return View(IndoorModel);
        }

        public List<InformationTableIndoor> GetDataIndoor()
        {
            var IndoorModel = new List<InformationTableIndoor>();
            var dates = new List<DateTime>();

            foreach (var item in _context.AvgTempAndHumidities.ToList())
            {
                if (item.Plats == "Inne")
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
                            where t.Plats == "Inne"
                            select t).ToList();
                var Mould = (from m in _context.MouldRisks
                             where m.SelectDate == date
                             where m.Plats == "Inne"
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

                var line = new InformationTableIndoor
                {
                    SelectDate = new DateTime(date.Year, date.Month, date.Day),
                    AvgHum = Math.Round(avgHum, 2),
                    AvgTemp = Math.Round(avgTemp, 2),
                    MouldRisk = mouldRisk,
                    MouldRank = mouldRank
                };
                IndoorModel.Add(line);
            }
            return IndoorModel;
        }

            // GET: InformationTableIndoors/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationTableIndoor = await _context.InformationTableIndoor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informationTableIndoor == null)
            {
                return NotFound();
            }

            return View(informationTableIndoor);
        }

        // GET: InformationTableIndoors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InformationTableIndoors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectDate,AvgTemp,AvgHum,MouldRisk")] InformationTableIndoor informationTableIndoor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informationTableIndoor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(informationTableIndoor);
        }

        // GET: InformationTableIndoors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationTableIndoor = await _context.InformationTableIndoor.FindAsync(id);
            if (informationTableIndoor == null)
            {
                return NotFound();
            }
            return View(informationTableIndoor);
        }

        // POST: InformationTableIndoors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectDate,AvgTemp,AvgHum,MouldRisk")] InformationTableIndoor informationTableIndoor)
        {
            if (id != informationTableIndoor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informationTableIndoor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationTableIndoorExists(informationTableIndoor.Id))
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
            return View(informationTableIndoor);
        }

        // GET: InformationTableIndoors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informationTableIndoor = await _context.InformationTableIndoor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (informationTableIndoor == null)
            {
                return NotFound();
            }

            return View(informationTableIndoor);
        }

        // POST: InformationTableIndoors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informationTableIndoor = await _context.InformationTableIndoor.FindAsync(id);
            _context.InformationTableIndoor.Remove(informationTableIndoor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationTableIndoorExists(int id)
        {
            return _context.InformationTableIndoor.Any(e => e.Id == id);
        }
    }
}
