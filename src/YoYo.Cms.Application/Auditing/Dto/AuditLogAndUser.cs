using Abp.Auditing;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.Auditing.Dto
{
    /// <summary>
    /// A helper class to store an <see cref="AuditLog"/> and a <see cref="User"/> object.
    /// </summary>
    public class AuditLogAndUser
    {/// <summary>
    /// �����־ʵ��
    /// </summary>
        public AuditLog AuditLogInfo{ get; set; }
        /// <summary>
        /// �û�ʵ��
        /// </summary>
        public User UserInfo { get; set; }
    }
}