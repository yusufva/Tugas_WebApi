namespace WebApi.Models
{
    public class University : DateChange
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
