using System.ComponentModel.DataAnnotations;

namespace CodeTest.Models.Template
{
    public class ContentModel
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } // Type of content: heading, paragraph, card, image
        public string? Level { get; set; } // Heading level (h1, h2, etc.)
        public string? Text { get; set; } // Text for heading, paragraph, or card body
        public string? Color { get; set; } // Text or background color
        public string? Header { get; set; } // Card header text
        public string? Body { get; set; } // Card Body text
        public string? Footer { get; set; } // Card footer text
        public string? BorderColor { get; set; } // Card border color
        public string? Image { get; set; } // Base64 string for image
        public string? ImageType { get; set; } // Image type: cover, rounded
    }
}
