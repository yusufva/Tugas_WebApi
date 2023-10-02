using WebApi.Models;

namespace WebApi.DTOs.AccountRoles
{
    public class AccountRolesDto : GeneralDto
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static explicit operator AccountRolesDto(AccountRole accountRole)
        {
            return new AccountRolesDto 
            { 
                Guid = accountRole.Guid, 
                AccountGuid = accountRole.AccountGuid, 
                RoleGuid = accountRole.RoleGuid 
            };
        }

        public static implicit operator AccountRole(AccountRolesDto accountRolesDto)
        {
            return new AccountRole
            {
                Guid = accountRolesDto.Guid,
                AccountGuid = accountRolesDto.AccountGuid,
                RoleGuid = accountRolesDto.RoleGuid,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
