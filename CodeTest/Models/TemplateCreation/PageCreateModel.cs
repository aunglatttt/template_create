using CodeTest.Models.Template;

namespace CodeTest.Models.TemplateCreation
{
    public class PageCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // Page name
        public List<TemplateModel> Templates { get; set; } = new List<TemplateModel>(); // Selected templates
    }
}
