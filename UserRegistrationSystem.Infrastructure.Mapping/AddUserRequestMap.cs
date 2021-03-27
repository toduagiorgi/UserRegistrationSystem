using FluentMapping;
using System;
using System.Collections.Generic;
using System.Text;
using UserRegistrationSystem.Core.Models.Models;

namespace UserRegistrationSystem.Infrastructure.Mapping
{
    public static class AddUserRequestMap
    {
        public static User ToUserMap(this AddUserRequest user)
        {
            var mapper = FluentMapper.ThatMaps<User>().From<AddUserRequest>()
                                     .IgnoringTargetProperty(z => z.Id)
                                     .IgnoringTargetProperty(z => z.AccessFailedCount)
                                     .IgnoringTargetProperty(z => z.ConcurrencyStamp)
                                     .IgnoringTargetProperty(z => z.EmailConfirmed)
                                     .IgnoringTargetProperty(z => z.LockoutEnabled)
                                     .IgnoringTargetProperty(z => z.LockoutEnd)
                                     .IgnoringTargetProperty(z => z.NormalizedEmail)
                                     .IgnoringTargetProperty(z => z.NormalizedUserName)
                                     .IgnoringTargetProperty(z => z.PasswordHash)
                                     .IgnoringTargetProperty(z => z.PhoneNumber)
                                     .IgnoringTargetProperty(z => z.PhoneNumberConfirmed)
                                     .IgnoringTargetProperty(z => z.SecurityStamp)
                                     .IgnoringTargetProperty(z => z.TwoFactorEnabled)
                                     .Create();
            return mapper.Map(user);
        }
    }
}
