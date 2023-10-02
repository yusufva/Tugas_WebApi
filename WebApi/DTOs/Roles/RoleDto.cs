using WebApi.Models;

namespace WebApi.DTOs.Roles
{
    public class RoleDto : GeneralDto
    {
        public string Name; //deklarasi property

        public static explicit operator RoleDto(Role role) //implementasi explicit Operator
        {
            return new RoleDto
            {
                Guid = role.Guid,
                Name = role.Name,
            };
        }

        public static implicit operator Role(RoleDto roleDto) //implementasi implicit Operator
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
