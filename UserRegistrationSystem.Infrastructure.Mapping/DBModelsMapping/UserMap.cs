using FluentMapping;
using System;
using UserRegistrationSystem.Core.Models.Models;

namespace UserRegistrationSystem.Infrastructure.Mapping.DBModelsMapping
{
    public static class UserMap
    {
        public static RelationalDatabase.DBEntities.User ToDbObject(this User user)
        {

            var mapper = FluentMapper.ThatMaps<RelationalDatabase.DBEntities.User>().From<User>()
                                     .ThatSets(tgt => tgt.Id).From(src => src.Id == Guid.Empty ? Guid.NewGuid() : src.Id)
                                     .ThatSets(tgt => tgt.Address).From(src => src.Address.ToDbObject())
                                     .IgnoringSourceProperty(x => x.Id)
                                     .IgnoringSourceProperty(x => x.Password)
                                     .Create();

            return mapper.Map(user);
        }

        public static User ToObject(this RelationalDatabase.DBEntities.User user)
        {

            var mapper = FluentMapper.ThatMaps<User>().From<RelationalDatabase.DBEntities.User>()
                                     .ThatSets(tgt => tgt.Address).From(src => src.Address.ToObject())
                                     .IgnoringTargetProperty(x => x.Password)
                                     .Create();
            return mapper.Map(user);
        }
    }
}