using CodeTest.Models.TemplateCreation;
using System.ComponentModel.DataAnnotations;

namespace CodeTest.Models.Template
{
    public class TemplateModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } // Template name
        public List<ContentModel> Contents { get; set; } = new List<ContentModel>(); // List of content items
        //public int? PageCreateModelId { get; set; }
        //public virtual PageCreateModel? PageModels { get; set; }
        //public List<PageCreateModel> PageModels { get; set; }
    }
}
