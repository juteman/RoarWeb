using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Index()
        {
            return View();
        }

        // GET: ReforgeDev/Details/5
        public IActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReforgeDev/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
    }
}