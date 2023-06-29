using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTH626.Models;
using MvcMovie.Data;

using BTH626.Models.Process;

namespace BTH626.Controllers
{
    public class LophocController : Controller
    {
        private readonly MvcMovieContext _context;
        private ExcelProcess _excelProcess=new ExcelProcess();
        private StringProcess strPro = new StringProcess();
        
        public LophocController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Lophoc
        public async Task<IActionResult> Index()
        {
              return _context.Lophoc != null ? 
                          View(await _context.Lophoc.ToListAsync()) :
                          Problem("Entity set 'MvcMovieContext.Lophoc'  is null.");
        }

        // GET: Lophoc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Lophoc == null)
            {
                return NotFound();
            }

            var lophoc = await _context.Lophoc
                .FirstOrDefaultAsync(m => m.tenlop == id);
            if (lophoc == null)
            {
                return NotFound();
            }

            return View(lophoc);
        }

        // GET: Lophoc/Create
        // public IActionResult Create()
        // {
        //     return View();
        // }

        public IActionResult Create()
        {
	            var IDdautien = "LOP01";
            var countAnh = _context.Lophoc.Count();
	            if (countAnh > 0)
	            {
	                var malop = _context.Lophoc.OrderByDescending(m => m.malop).First().malop;
	                IDdautien = strPro.AutoGenerateCode(malop);
	            }
	            ViewBag.newID = IDdautien;
	            return View();
	        }


        // POST: Lophoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("tenlop,malop")] Lophoc lophoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lophoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lophoc);
        }

        // GET: Lophoc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Lophoc == null)
            {
                return NotFound();
            }

            var lophoc = await _context.Lophoc.FindAsync(id);
            if (lophoc == null)
            {
                return NotFound();
            }
            return View(lophoc);
        }

        // POST: Lophoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("tenlop,malop")] Lophoc lophoc)
        {
            if (id != lophoc.tenlop)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lophoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LophocExists(lophoc.tenlop))
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
            return View(lophoc);
        }

        // GET: Lophoc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Lophoc == null)
            {
                return NotFound();
            }

            var lophoc = await _context.Lophoc
                .FirstOrDefaultAsync(m => m.tenlop == id);
            if (lophoc == null)
            {
                return NotFound();
            }

            return View(lophoc);
        }

        // POST: Lophoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Lophoc == null)
            {
                return Problem("Entity set 'MvcMovieContext.Lophoc'  is null.");
            }
            var lophoc = await _context.Lophoc.FindAsync(id);
            if (lophoc != null)
            {
                _context.Lophoc.Remove(lophoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LophocExists(string id)
        {
          return (_context.Lophoc?.Any(e => e.tenlop == id)).GetValueOrDefault();
        }
       
       
       
       
       
       
        public async Task<IActionResult> Upload()
        {
            return View();
        }
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    var FileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", FileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to sever
                        await file.CopyToAsync(stream);
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var lh = new Lophoc();

                            lh.tenlop = dt.Rows[i][0].ToString();
                            lh.malop = dt.Rows[i][1].ToString();
                          
                           
                         _context.Lophoc.Add(lh);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
    }
    }
}
