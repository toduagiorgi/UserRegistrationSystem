using System;
using System.Collections.Generic;
using System.Text;

namespace UserRegistrationSystem.Core.Models.Models
{
    public class AddUserRequest
    {
        public string PersonalId { get; set; }
        public bool IsMarried { get; set; }
        public bool IsEmployed { get; set; }
        public double Salary { get; set; }       
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
