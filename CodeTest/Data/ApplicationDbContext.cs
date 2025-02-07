using CodeTest.Models;
using CodeTest.Models.Template;
using CodeTest.Models.TemplateCreation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CodeTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TemplateNotused> Templates { get; set; }
        public DbSet<DynamicField> DynamicFields { get; set; }
        public DbSet<Payload> Payloads { get; set; }

        public DbSet<CMSPage> CMSPages { get; set; }


        public DbSet<ContentModel> ContentModels { get; set; }
        public DbSet<TemplateModel> TemplateModels { get; set; }
        public DbSet<PageCreateModel> PageModels { get; set; }

    }
}
