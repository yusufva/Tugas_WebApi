using WebApi.Models;

namespace WebApi.DTOs.Roles
{
    public class NewRoleDto
    {
        public string Name { get; set; }

        public static implicit operator Role(NewRoleDto newRoleDto)
        {
            return new Role
            {
                Name = newRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
