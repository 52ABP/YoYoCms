using Abp.Auditing;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.Auditing.Dto
{
    /// <summary>
    /// A helper class to store an <see cref="AuditLog"/> and a <see cref="User"/> object.
    /// </summary>
    public class AuditLogAndUser
    {/// <summary>
    /// 设计日志实体
    /// </summary>
        public AuditLog AuditLogInfo{ get; set; }
        /// <summary>
        /// 用户实体
        /// </summary>
        public User UserInfo { get; set; }
    }
}