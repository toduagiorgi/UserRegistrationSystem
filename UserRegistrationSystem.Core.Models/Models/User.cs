using Microsoft.AspNetCore.Identity;
using System;

namespace UserRegistrationSystem.Core.Models.Models
{
    public class User : IdentityUser<Guid>
    {
        public string PersonalId { get; set; }
        public bool IsMarried { get; set; }
        public bool IsEmployed { get; set; }
        public double Salary { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
    }
}
