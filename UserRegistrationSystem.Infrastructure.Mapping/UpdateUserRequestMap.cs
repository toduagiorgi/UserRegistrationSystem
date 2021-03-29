using FluentMapping;
using UserRegistrationSystem.Core.Models.Models;

namespace UserRegistrationSystem.Infrastructure.Mapping
{
    public static class UpdateUserRequestMap
    {
        public static User ToUserMap(this UpdateRequest user)
        {
            var mapper = FluentMapper.ThatMaps<User>().From<UpdateRequest>()
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
                                     .IgnoringTargetProperty(z => z.Email)
                                     .IgnoringTargetProperty(z => z.Password)
                                     .IgnoringTargetProperty(z => z.PersonalId)
                                     .Create();
            return mapper.Map(user);
        }
    }
}
