using Microsoft.AspNetCore.Identity;
using System;

namespace UserRegistrationSystem.Infrastructure.RelationalDatabase.DBEntities
{
    public class User : IdentityUser<Guid>
    {
        public string PersonalId { get; set; }
        public bool IsMarried { get; set; }
        public bool IsEmployed { get; set; }
        public double Salary { get; set; }
        public virtual Address Address { get; set; }
    }
}
