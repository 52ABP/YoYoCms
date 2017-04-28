using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using Microsoft.AspNet.Identity;

namespace YoYo.Cms.UserManagerment.Users
{


    /// <summary>
    /// 用户信息
    /// </summary>
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";
        public const int MaxPhoneNumberLength = 18;

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

     
        [Required(AllowEmptyStrings = true)]
        private new string Name { get; set; }
        [Obsolete("Surname属性已经不在使用，请使用UserName")]
        [NotMapped]
        [Required(AllowEmptyStrings = true)]
        private new string Surname { get; set; }

        [Required(AllowEmptyStrings = true)]
        public override string EmailAddress { get; set; }

        //public virtual Guid? ProfilePictureId { get; set; }
        //public virtual bool ShouldChangePasswordOnNextLogin { get; set; }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };
        }
    }
}