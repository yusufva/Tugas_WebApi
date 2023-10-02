using WebApi.Models;

namespace WebApi.DTOs.AccountRoles
{
    public class NewAccountRolesDto
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static implicit operator AccountRole(NewAccountRolesDto newAccountRolesDto)
        {
            return new AccountRole
            {
                AccountGuid = newAccountRolesDto.AccountGuid,
                RoleGuid = newAccountRolesDto.RoleGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
