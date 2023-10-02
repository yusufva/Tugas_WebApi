using WebApi.Models;

namespace WebApi.DTOs.AccountRoles
{
    public class AccountRolesDto : GeneralDto
    {
        public Guid AccountGuid { get; set; } //deklarasi property
        public Guid RoleGuid { get; set; } //deklarasi property

        public static explicit operator AccountRolesDto(AccountRole accountRole) //implementasi explicit Operator
        {
            return new AccountRolesDto 
            { 
                Guid = accountRole.Guid, 
                AccountGuid = accountRole.AccountGuid, 
                RoleGuid = accountRole.RoleGuid 
            };
        }

        public static implicit operator AccountRole(AccountRolesDto accountRolesDto) //implementasi implicit Operator
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
