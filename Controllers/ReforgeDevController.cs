using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SilentRoar.Data;
using SilentRoar.Models;

namespace SilentRoar.Controllers
{
  
    public class ReforgeDevController : Controller
    {
        private readonly AppDbContext _context;

        public ReforgeDevController(AppDbContext context)
        {
            _context = context;
        }
      // GET: ReforgeDev
        public async Task<IActionResult> Index()
        {
            var reforgeDevs = from s in _context.ReforgeDevs select s;
            reforgeDevs = reforgeDevs.OrderByDescending(s => s.ID);
            return View(await reforgeDevs.AsNoTracking().ToListAsync());
        }

        // GET: ReforgeDev/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reforgeDev = await _context.ReforgeDevs
                .FirstOrDefaultAsync(m => m.ID == id);
            if(reforgeDev == null)
            {
                return NotFound();
            }

            return View(reforgeDev);
        }

        // GET: ReforgeDev/Create
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
        
            return View();
        }

        // POST: ReforgeDev/Create
        // 创建Reforge 开发日志页面
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ReforgeDev reforgeDev)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reforgeDev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reforgeDev);
        }

        // GET: ReforgeDev/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reforgeDevLog = await _context.ReforgeDevs.FindAsync(id);
            if (reforgeDevLog == null)
            {
                return NotFound();
            }
            return View(reforgeDevLog);
        }

        // POST: ReforgeDev/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, ReforgeDev reforgeDevLog)
        {
            if (id != reforgeDevLog.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reforgeDevLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReforgeDevExists(reforgeDevLog.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new {id = reforgeDevLog.ID });
            }
            return View(reforgeDevLog);
        }

        // GET: ReforgeDev/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReforgeDev/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool ReforgeDevExists(int id)
        {
            return _context.ReforgeDevs.Any(e => e.ID == id);
        }
    }
}