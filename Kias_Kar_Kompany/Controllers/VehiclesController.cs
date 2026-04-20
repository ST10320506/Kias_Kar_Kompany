using Kias_Kar_Kompany.Data;
using Kias_Kar_Kompany.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Kias_Kar_Kompany.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Kias_Kar_KompanyContext _context;

        public VehiclesController(Kias_Kar_KompanyContext context)
        {
            _context = context;
        }

        // GET: VehiclesController
        public async Task<IActionResult> Index()
        {
            var mvcKias_Kar_KompanyContext = _context.Vehicle.Include(g => g.Manufacturer).Include(g => g.Owner);
            return View(await _context.Vehicle.ToListAsync());
        }

        // GET: VehiclesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(g => g.Manufacturer)
                .Include(g => g.Owner)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Manufacturer, "Id", "Id");
            ViewData["OwnerId"] = new SelectList(_context.Owner, "OwnerId", "OwnerId");
            return View();
        }

        // POST: VehiclesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId, VehicleName, VehicleModel, VehiclePrice, VehicleType, VehicleImageURL, Id, OwnerId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Manufacturer, "Id", "Id", vehicle.Id);
            ViewData["OwnerId"] = new SelectList(_context.Owner, "OwnerId", "OwnerId", vehicle.OwnerId);
            return View(vehicle);
        }

        // GET: VehiclesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Manufacturer, "Id", "Id", vehicle.Id);
            ViewData["OwnerId"] = new SelectList(_context.Owner, "OwnerId", "OwnerId", vehicle.OwnerId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId, VehicleName, VehicleModel, VehiclePrice, VehicleType, VehicleImageURL, Id, OwnerId")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
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
            ViewData["Id"] = new SelectList(_context.Manufacturer, "Id", "Id", vehicle.Id);
            ViewData["OwnerId"] = new SelectList(_context.Owner, "OwnerId", "OwnerId", vehicle.OwnerId);
            return View(vehicle);
        }

        // GET: Vehicle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(g => g.Manufacturer)
                .Include(g => g.Owner)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.VehicleId == id );
        }
    }
}
