﻿namespace WebApi.Models
{
    public class Accounts : BaseEntity
    {
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int Otp {  get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

    }
}
