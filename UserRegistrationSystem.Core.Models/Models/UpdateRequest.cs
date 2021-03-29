namespace UserRegistrationSystem.Core.Models.Models
{
    public class UpdateRequest
    {
        public string UserName { get; set; }
        public bool IsMarried { get; set; }
        public bool IsEmployed { get; set; }
        public double Salary { get; set; }
        public Address Address { get; set; }
    }
}
