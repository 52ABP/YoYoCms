(function() {

    'use strict';
    yoyocmsModule.controller('app.layout.header', [
        "$scope", "appSession","abp.services.app.notification",
        function ($scope, appSession,notificationService) {

        var vm = this;
        vm.languages = abp.localization.languages;
        vm.currentLanguage = abp.localization.currentLanguage;
        vm.changeLanguage = function (languageName) {
         //   console.log($scope.languages);
            location.href = abp.appPath + 'AbpLocalization/ChangeCulture?cultureName=' + languageName + '&returnUrl=' + window.location.pathname + window.location.hash;
        };

        vm.getUserName = appSession.user.userName;

 //  菜单伸缩管理
        vm.toggleSidebarMenu= function() {
            if ($("body > main").hasClass("menu-collapsed")) {
                $("body > main").removeClass("menu-collapsed");
            } else {
                $("body > main").addClass("menu-collapsed");
            }
         }
        var randomId = Math.floor((Math.random() * 10) + 1);
            vm.userAvatarUrl = "/App/assets/yoyocms/avatar/"+randomId+".png";

          

            vm.unReadUserNotificationCount = 0;
            vm.userNotifications = [];


            //格式化消息
            var formattedMessage = function (item) {

                var message = {
                    id: item.id,
                    text: abp.notifications.getFormattedMessageFromUserNotification(item),
                    time: item.notification.creationTime,
                    image: getNotificationImgBySeverity(item.notification.severity),
                    state: abp.notifications.getUserNotificationStateAsString(item.state),
                }

                return message;

            }

            //获取图片路径
            function getNotificationImgBySeverity(severity) {
                switch (severity) {
                    case abp.notifications.severity.SUCCESS:
                        return '/App/assets/yoyocms/notification/1.png';
                    case abp.notifications.severity.WARN:
                        return '/App/assets/yoyocms/notification/2.png';
                    case abp.notifications.severity.ERROR:
                        return '/App/assets/yoyocms/notification/3.png';
                    case abp.notifications.severity.FATAL:
                        return '/App/assets/yoyocms/notification/4.png';
                    case abp.notifications.severity.INFO:
                    default:
                          return '/App/assets/yoyocms/notification/0.png';
                }
            }







            vm.getUserNotificationsAsync= function() {

                notificationService.getPagedUserNotificationsAsync({
                    maxResultCount: 10
                }).then(function(result) {
                    vm.unReadUserNotificationCount = result.data.unreadCount;
                    vm.userNotifications = [];
                    $.each(result.data.items,
                        function (index, item) {
                            console.log(item);
                            vm.userNotifications.push(formattedMessage(item));
                        });

                    console.log(vm.userNotifications);
                    console.log(vm.userNotifications.length);
                    console.log(vm.userNotifications.count);
                });

            }
        




            //初始化方法
            function init() {
                vm.getUserNotificationsAsync();
             
            }

            init();

            abp.event.on('abp.notifications.received', function (userNotification) {
                abp.notifications.showUiNotifyForUserNotification(userNotification);
            });
        }

    ]);
 

})();