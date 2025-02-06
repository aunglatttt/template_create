namespace CodeTest.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DynamicField> Fields { get; set; }
    }
}
