using CodeTest.Data;
using CodeTest.Models;
using CodeTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace CodeTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Template()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Template(Template template)
        {
            if (ModelState.IsValid)
            {
                _context.Templates.Add(template);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home"); // Redirect to home or another appropriate page
            }
            return View(template);
        }

        public async Task<IActionResult> Preview(int? templateId)
        {
            var tempLatesObj = await _context.Templates.AsNoTracking()
                                       //.Include(x => x.Fields)
                                       .OrderByDescending(x => x.Id)
                                       .ToListAsync();
            if (tempLatesObj.Count == 0)
                return View();

            ViewData["TempLates"] = new SelectList(tempLatesObj, "Id", "Name");

            if (templateId.HasValue)
            {
                var template2 = await _context.Templates
               .Include(t => t.Fields)
               .FirstOrDefaultAsync(t => t.Id == templateId);

                if (template2 == null)
                {
                    return NotFound();
                }

                var dto2 = new TemplatePreviewViewModel();
                dto2.Name = template2.Name;

                var cloneCard2 = new List<ClonedCardViewModel>();
                foreach (var card in template2.Fields)
                {
                    var newCard = new ClonedCardViewModel
                    {
                        FieldName = card.FieldName,
                        Type = card.Type,
                        Options = (!string.IsNullOrEmpty(card.Options)) ? JsonSerializer.Deserialize<List<string>>(card.Options) : new List<string>()
                    };
                    cloneCard2.Add(newCard);
                }
                dto2.ClonedCards = cloneCard2;

                ViewBag.SelectedTemplateId = templateId.Value;

                return View(dto2);
            }


            var tempLate = await _context.Templates.AsNoTracking()
                                         .Include(x => x.Fields)
                                         .FirstOrDefaultAsync(x => x.Id == tempLatesObj[0].Id);

            if (tempLate == null)
                return View();

            var dto = new TemplatePreviewViewModel();
            dto.Name = tempLate.Name;

            var cloneCard = new List<ClonedCardViewModel>();
            foreach (var card in tempLate.Fields)
            {
                var newCard = new ClonedCardViewModel
                {
                    FieldName = card.FieldName,
                    Type = card.Type,
                    Options = (!string.IsNullOrEmpty(card.Options)) ? JsonSerializer.Deserialize<List<string>>(card.Options) : new List<string>()
                };
                cloneCard.Add(newCard);
            }
            dto.ClonedCards = cloneCard;
            //var modelJson = TempData["PreviewDataVM"] as string;
            //if (string.IsNullOrEmpty(modelJson))
            //    return View();


            //var model = JsonSerializer.Deserialize<TemplatePreviewViewModel>(modelJson);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Preview([FromBody] TemplatePreviewViewModel model)
        {
            var tempLate = new Template
            {
                Name = model.Name
            };

            var fields = new List<DynamicField>();
            foreach(var item in model.ClonedCards)
            {
                var newFiled = new DynamicField
                {
                    FieldName = item.FieldName,
                    Type = item.Type,
                    Options = item.Options.Count > 0 ? JsonSerializer.Serialize(item.Options) : ""
                };
                fields.Add(newFiled);
            }
            tempLate.Fields = fields;
            await _context.Templates.AddAsync(tempLate);
            await _context.SaveChangesAsync();

            //TempData["PreviewDataVM"] = JsonSerializer.Serialize<TemplatePreviewViewModel>(model);

            return Json(new { success = true });
        }

    }
}