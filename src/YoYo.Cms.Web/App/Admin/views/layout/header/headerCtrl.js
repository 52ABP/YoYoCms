(function() {

    'use strict';
    yoyocmsModule.controller('app.layout.header', [
        "$scope", "appSession", "abp.services.app.notification","appUserNotificationService",
        function ($scope, appSession, notificationService, appUserNotificationService) {

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
         
         
            //获取所有的消息信息
            vm.getUserNotificationsAsync= function() {
                notificationService.getPagedUserNotificationsAsync({
                    maxResultCount: 5
                }).then(function(result) {
                    vm.unReadUserNotificationCount = result.data.unreadCount;
                    vm.userNotifications = [];
                    $.each(result.data.items,
                        function (index, item) {
                            vm.userNotifications.push(appUserNotificationService.formattedMessage(item));
                        });
                    //console.log(vm.userNotifications);
                });

            }
        //标记所有为已读
            vm.makeAllAsRead= function() {
                appUserNotificationService.makeAllAsReadService();
            }
            //设置消息为已读
            vm.makeNotificationAsRead = function (userNotification) {
                appUserNotificationService.makeNotificationAsReadService(userNotification);
            }


            //初始化方法
            function init() {
                vm.getUserNotificationsAsync();
              
            }

            init();

            //测试为领域事件的一个例子
            abp.event.on('abp.notifications.received', function (userNotification) {
                abp.notifications.showUiNotifyForUserNotification(userNotification);
                vm.getUserNotificationsAsync();
            });
            //定义个刷新事件
            abp.event.on('abp.notifications.refresh', function (){
                vm.getUserNotificationsAsync();
            });
            //定义一个阅读事件，目的是全局处理消息
            abp.event.on('app.notifications.read', function (userNotification) {
                for (var i = 0; i < vm.userNotifications.length; i++) {
                    if (vm.userNotifications[i].id == userNotification.id) {
                        vm.userNotifications[i].state = 'READ';
                    }
                }
                vm.unReadUserNotificationCount -= 1;
            });

        }

    ]);
 

})();