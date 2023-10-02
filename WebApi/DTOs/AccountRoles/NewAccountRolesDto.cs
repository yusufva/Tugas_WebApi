using WebApi.Models;

namespace WebApi.DTOs.AccountRoles
{
    public class NewAccountRolesDto
    {
        public Guid AccountGuid { get; set; } //deklarasi property
        public Guid RoleGuid { get; set; } //deklarasi property

        public static implicit operator AccountRole(NewAccountRolesDto newAccountRolesDto) //implementasi implicit Operator
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
