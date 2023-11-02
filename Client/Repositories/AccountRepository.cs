using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApi.DTOs.Account;
using WebApi.DTOs.Employees;
using WebApi.Utilities.Handler;

namespace Client.Repositories
{
    public class AccountRepository : GeneralRepository<AccountsDto, NewAccountsDto, Guid>, IAccountRepository
    {
        public AccountRepository(string request = "Accounts/") : base(request)
        {

        }

        public async Task<ResponseOkHandler<TokenDto>> Login(EmployeeLoginDto login)
        {
            string jsonEntity = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{request}login", content))
            {
                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();
                var entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<TokenDto>>(apiResponse);
                return entityVM;
            }
        }

    }
}
