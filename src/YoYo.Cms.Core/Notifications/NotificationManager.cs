using System;
using System.Threading.Tasks;
using Abp.Notifications;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.Notifications
{
    public class NotificationManager : CmsDomainServiceBase, INotificationManager
    {

        private readonly INotificationPublisher _notificationPublisher;

        public NotificationManager(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }

        public async Task WelcomeToCmsAsync(User user)
        {

            await _notificationPublisher.PublishAsync(
                CmsConsts.NotificationConstNames.WelcomeToCms,
                new MessageNotificationData(L("WelcomeToCms")),severity:NotificationSeverity.Success,userIds:new []{user.ToUserIdentifier()}
                
                
                
                );

        }

       
 
    }
}