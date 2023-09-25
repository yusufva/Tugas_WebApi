namespace WebApi.Models
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
    }
}
