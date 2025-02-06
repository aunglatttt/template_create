using CodeTest.Data;
using CodeTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeTest.Controllers
{
    public class PagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PagesController(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CMSPages.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new CMSPage());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CMSPage page)
        {
            //if (ModelState.IsValid)
            if (page.Title != null && page.MetaDescription != null)
                {
                page.CreatedAt = DateTime.Now;
                page.UpdatedAt = DateTime.Now;
                _context.CMSPages.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(page);
        }

        public IActionResult Edit(int id)
        {
            var page = _context.CMSPages.Find(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        [HttpPost]
        public IActionResult Edit(int id, CMSPage page)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                page.UpdatedAt = DateTime.Now;
                _context.Update(page);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // Delete a page
        public IActionResult Delete(int id)
        {
            var page = _context.CMSPages.Find(id);
            if (page == null)
            {
                return NotFound();
            }
            _context.CMSPages.Remove(page);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // View a single page
        public IActionResult ViewPage(int id)
        {
            var page = _context.CMSPages.Find(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
    }
}
