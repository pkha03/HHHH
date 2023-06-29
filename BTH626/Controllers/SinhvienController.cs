using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTH626.Models;
using MvcMovie.Data;

namespace BTH626.Controllers
{
    public class SinhvienController : Controller
    {
        private readonly MvcMovieContext _context;

        public SinhvienController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Sinhvien
        public async Task<IActionResult> Index()
        {
            var mvcMovieContext = _context.Sinhvien.Include(s => s.Lophoc);
            return View(await mvcMovieContext.ToListAsync());
        }

        // GET: Sinhvien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Sinhvien == null)
            {
                return NotFound();
            }

            var sinhvien = await _context.Sinhvien
                .Include(s => s.Lophoc)
                .FirstOrDefaultAsync(m => m.masinhvien == id);
            if (sinhvien == null)
            {
                return NotFound();
            }

            return View(sinhvien);
        }

        // GET: Sinhvien/Create
        public IActionResult Create()
        {
            ViewData["malop"] = new SelectList(_context.Set<Lophoc>(), "tenlop", "tenlop");
            return View();
        }

        // POST: Sinhvien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("masinhvien,tensinhvien,malop")] Sinhvien sinhvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["malop"] = new SelectList(_context.Set<Lophoc>(), "tenlop", "tenlop", sinhvien.malop);
            return View(sinhvien);
        }

        // GET: Sinhvien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Sinhvien == null)
            {
                return NotFound();
            }

            var sinhvien = await _context.Sinhvien.FindAsync(id);
            if (sinhvien == null)
            {
                return NotFound();
            }
            ViewData["malop"] = new SelectList(_context.Set<Lophoc>(), "tenlop", "tenlop", sinhvien.malop);
            return View(sinhvien);
        }

        // POST: Sinhvien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("masinhvien,tensinhvien,malop")] Sinhvien sinhvien)
        {
            if (id != sinhvien.masinhvien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhvienExists(sinhvien.masinhvien))
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
            ViewData["malop"] = new SelectList(_context.Set<Lophoc>(), "tenlop", "tenlop", sinhvien.malop);
            return View(sinhvien);
        }

        // GET: Sinhvien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Sinhvien == null)
            {
                return NotFound();
            }

            var sinhvien = await _context.Sinhvien
                .Include(s => s.Lophoc)
                .FirstOrDefaultAsync(m => m.masinhvien == id);
            if (sinhvien == null)
            {
                return NotFound();
            }

            return View(sinhvien);
        }

        // POST: Sinhvien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Sinhvien == null)
            {
                return Problem("Entity set 'MvcMovieContext.Sinhvien'  is null.");
            }
            var sinhvien = await _context.Sinhvien.FindAsync(id);
            if (sinhvien != null)
            {
                _context.Sinhvien.Remove(sinhvien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SinhvienExists(string id)
        {
          return (_context.Sinhvien?.Any(e => e.masinhvien == id)).GetValueOrDefault();
        }
    }
}
