using System;
using System.Threading.Tasks;

namespace UserRegistrationSystem.Infrastructure.RelationalDatabase.DBEntities
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public Guid UserId { get; set; }

        public static implicit operator Address(Task<Address> v)
        {
            throw new NotImplementedException();
        }
        //public virtual User User { get; set; }
    }
}
