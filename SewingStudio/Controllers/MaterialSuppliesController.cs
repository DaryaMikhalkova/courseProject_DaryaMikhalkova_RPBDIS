using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SewingStudio.Models;

namespace SewingStudio.Controllers
{
    public class MaterialSuppliesController : Controller
    {
        private readonly AtelierContext _context;

        public MaterialSuppliesController(AtelierContext context)
        {
            _context = context;
        }

        // GET: MaterialSupplies
        public async Task<IActionResult> Index()
        {
            var atelierContext = _context.MaterialSupplies.Include(m => m.Material);
            return View(await atelierContext.ToListAsync());
        }

        // GET: MaterialSupplies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialSupply = await _context.MaterialSupplies
                .Include(m => m.Material)
                .FirstOrDefaultAsync(m => m.MaterialSuplyId == id);
            if (materialSupply == null)
            {
                return NotFound();
            }

            return View(materialSupply);
        }

        // GET: MaterialSupplies/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Materials, "IdMaterial", "MaterialName");
            return View();
        }

        // POST: MaterialSupplies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialSuplyId,Provider,MaterialId,PriceOfMaterials,AmountOfMaterial,DeliveryDate")] MaterialSupply materialSupply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialSupply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "IdMaterial", "MaterialName", materialSupply.MaterialId);
            return View(materialSupply);
        }

        // GET: MaterialSupplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialSupply = await _context.MaterialSupplies.FindAsync(id);
            if (materialSupply == null)
            {
                return NotFound();
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "IdMaterial", "MaterialName", materialSupply.MaterialId);
            return View(materialSupply);
        }

        // POST: MaterialSupplies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialSuplyId,Provider,MaterialId,PriceOfMaterials,AmountOfMaterial,DeliveryDate")] MaterialSupply materialSupply)
        {
            if (id != materialSupply.MaterialSuplyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialSupply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialSupplyExists(materialSupply.MaterialSuplyId))
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
            ViewData["MaterialId"] = new SelectList(_context.Materials, "IdMaterial", "MaterialName", materialSupply.MaterialId);
            return View(materialSupply);
        }

        // GET: MaterialSupplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialSupply = await _context.MaterialSupplies
                .Include(m => m.Material)
                .FirstOrDefaultAsync(m => m.MaterialSuplyId == id);
            if (materialSupply == null)
            {
                return NotFound();
            }

            return View(materialSupply);
        }

        // POST: MaterialSupplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materialSupply = await _context.MaterialSupplies.FindAsync(id);
            _context.MaterialSupplies.Remove(materialSupply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialSupplyExists(int id)
        {
            return _context.MaterialSupplies.Any(e => e.MaterialSuplyId == id);
        }
    }
}
