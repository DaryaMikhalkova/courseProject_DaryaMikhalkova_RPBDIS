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
    public class AtelierDepartmentsController : Controller
    {
        private readonly AtelierContext _context;

        public AtelierDepartmentsController(AtelierContext context)
        {
            _context = context;
        }

        // GET: AtelierDepartments
        public async Task<IActionResult> Index()
        {
            return View(await _context.AtelierDepartments.ToListAsync());
        }

        // GET: AtelierDepartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atelierDepartment = await _context.AtelierDepartments
                .FirstOrDefaultAsync(m => m.IdDepartment == id);
            if (atelierDepartment == null)
            {
                return NotFound();
            }

            return View(atelierDepartment);
        }

        // GET: AtelierDepartments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AtelierDepartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDepartment,DepartmentName,AmountOfWorkers,DescriptionOfTheTypeOfWork")] AtelierDepartment atelierDepartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atelierDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(atelierDepartment);
        }

        // GET: AtelierDepartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atelierDepartment = await _context.AtelierDepartments.FindAsync(id);
            if (atelierDepartment == null)
            {
                return NotFound();
            }
            return View(atelierDepartment);
        }

        // POST: AtelierDepartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDepartment,DepartmentName,AmountOfWorkers,DescriptionOfTheTypeOfWork")] AtelierDepartment atelierDepartment)
        {
            if (id != atelierDepartment.IdDepartment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atelierDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtelierDepartmentExists(atelierDepartment.IdDepartment))
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
            return View(atelierDepartment);
        }

        // GET: AtelierDepartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atelierDepartment = await _context.AtelierDepartments
                .FirstOrDefaultAsync(m => m.IdDepartment == id);
            if (atelierDepartment == null)
            {
                return NotFound();
            }

            return View(atelierDepartment);
        }

        // POST: AtelierDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atelierDepartment = await _context.AtelierDepartments.FindAsync(id);
            _context.AtelierDepartments.Remove(atelierDepartment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtelierDepartmentExists(int id)
        {
            return _context.AtelierDepartments.Any(e => e.IdDepartment == id);
        }
    }
}
