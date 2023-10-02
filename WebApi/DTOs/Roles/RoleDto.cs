using WebApi.Models;

namespace WebApi.DTOs.Roles
{
    public class RoleDto : GeneralDto
    {
        public string Name;

        public static explicit operator RoleDto(Role role)
        {
            return new RoleDto
            {
                Guid = role.Guid,
                Name = role.Name,
            };
        }

        public static implicit operator Role(RoleDto roleDto)
        {
            return new Role
            {
                Guid = roleDto.Guid,
                Name = roleDto.Name,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
