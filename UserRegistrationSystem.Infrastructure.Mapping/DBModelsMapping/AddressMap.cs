using FluentMapping;
using System;
using UserRegistrationSystem.Core.Models.Models;

namespace UserRegistrationSystem.Infrastructure.Mapping.DBModelsMapping
{
    public static class AddressMap
    {
        public static RelationalDatabase.DBEntities.Address ToDbObject(this Address address)
        {
            var mapper = FluentMapper.ThatMaps<RelationalDatabase.DBEntities.Address>().From<Address>()
                                     .IgnoringTargetProperty(x => x.Id)
                                     .IgnoringTargetProperty(x => x.UserId)
                                     .WithNullSource()
                                     .ReturnNull()
                                     .Create();
            return mapper.Map(address);
        }

        public static Address ToObject(this RelationalDatabase.DBEntities.Address address)
        {
            var mapper = FluentMapper.ThatMaps<Address>().From<RelationalDatabase.DBEntities.Address>()
                                     .IgnoringSourceProperty(z => z.Id)
                                     .IgnoringSourceProperty(z => z.UserId)
                                     .WithNullSource()
                                     .ReturnNull()
                                     .Create();
            return mapper.Map(address);
        }
    }
}