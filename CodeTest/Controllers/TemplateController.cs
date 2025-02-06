using CodeTest.Data;
using CodeTest.Models.Template;
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

        [HttpGet]
        public IActionResult CreateTemplate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveTemplate([FromBody] Models.Template.TemplateModel model)
        {
            try
            {
                _context.TemplateModels.Add(model);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpGet]
        public IActionResult PreviewTemplate()
        {

            return View(_context.TemplateModels.OrderByDescending(d => d.Id).Include(d => d.Contents).FirstOrDefault());
        }
    }
}
