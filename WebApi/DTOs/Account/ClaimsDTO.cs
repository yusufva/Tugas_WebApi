namespace WebApi.DTOs.Account
{
    public class ClaimsDTO
    {
        public string NameIdentifier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Role { get; set; }
    }
}
