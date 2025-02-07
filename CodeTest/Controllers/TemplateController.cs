using CodeTest.Data;
using CodeTest.Models.Template;
using CodeTest.Models.TemplateCreation;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection.PortableExecutable;
using static System.Net.Mime.MediaTypeNames;

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

        public async Task<IActionResult> CreatingPage()
        {
            //var templates = await _context.TemplateModels.AsNoTracking().Select(x => new { x.Id, x.Name }).ToListAsync();
            //if (templates == null || !templates.Any())
            //{
            //    ViewData["TemplateList"] = new SelectList(new List<object>(), "Id", "Name");
            //}
            //else
            //{
            //    ViewData["TemplateList"] = new SelectList(templates, "Id", "Name");
            //}
            //return View(await _context.TemplateModels.AsNoTracking().Distinct().ToListAsync());
            return View(await _context.TemplateModels
    .AsNoTracking()
    .GroupBy(t => t.Name)
    .Select(group => group.First()) // Select the first TemplateModel in each group
    .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplateByName(string name)
        {
            var template = await _context.TemplateModels.AsNoTracking().Include(x => x.Contents).Where(x => x.Name == name).FirstOrDefaultAsync();
            return Json(template);
        }

        [HttpPost]
        public async Task<IActionResult> SavePage([FromBody] PageCreateModel page)
        {
            if (page == null)
            {
                return BadRequest("Invalid data.");
            }
            //var templates = page.Templates;
            //page.Templates = null;

            foreach (var item in page.Templates)
            {
                var contentModel = await _context.TemplateModels.Where(x => x.Name == item.Name).Include(x => x.Contents).FirstOrDefaultAsync();
                if (contentModel != null && contentModel.Contents.Any())
                {
                    var newContentList = new List<ContentModel>();
                    foreach (var item2 in contentModel.Contents)
                    {
                        var newContent = new ContentModel
                        {
                            Type = item2.Type,
                            Level = item2.Level,
                            Text = item2.Text,
                            Color = item2.Color,
                            Header = item2.Header,
                            Footer = item2.Footer,
                            BorderColor = item2.BorderColor,
                            Image = item2.Image,
                            ImageType = item2.ImageType,
                            HeadingColor = item2.HeadingColor,
                            HeadingText = item2.HeadingText,
                            ParagraphColor = item2.ParagraphColor,
                            ParagraphText = item2.ParagraphText,
                            Width = item2.Width,
                            Height = item2.Height,
                            LogoName = item2.LogoName,
                            Body = item2.Body,
                        };
                        newContentList.Add(newContent);
                    }
                    item.Contents = newContentList;
                }
            }

            _context.PageModels.Add(page);
            await _context.SaveChangesAsync();

            //foreach (var model in page.Templates)
            //{
            //    var  tempplateObj = await _context.TemplateModels.OrderByDescending(x => x.Id).Where(x => x.Name == model.Name).FirstOrDefaultAsync();
            //    if (tempplateObj != null)
            //    {
            //        tempplateObj.PageCreateModelId = page.Id;
            //        _context.TemplateModels.Update(tempplateObj);
            //    }
            //}
            //await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public async Task<IActionResult> PageList()
        {
            return View(await _context.PageModels.AsNoTracking().Include(p => p.Templates).ToListAsync());
        }

        public async Task<IActionResult> PublishedPage(int id)
        {
            var page = await _context.PageModels
                .Include(p => p.Templates)
                .ThenInclude(t => t.Contents)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (page == null)
            {
                return NotFound();
            }
            ViewData["Layout"] = null;
            return View(page);
        }
    }
}
