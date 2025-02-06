using CodeTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeTest.Controllers
{
    public class TemplateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TemplateController(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<IActionResult> TemplateList()
        {
            return View(await _context.Templates.AsNoTracking().Include(x => x.Fields).ToListAsync());
        }
    }
}
