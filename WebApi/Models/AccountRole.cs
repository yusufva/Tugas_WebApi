﻿namespace WebApi.Models
{
    public class AccountRole : DateChange
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }
    }
}
