namespace WebApi.Models
{
    public class Role : DateChange
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
    }
}
