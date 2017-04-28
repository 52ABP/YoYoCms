    'use strict';

    var galaxyModule = angular.module('ourGalaxy', [
        'ui.router',
        'ui.bootstrap',
        'ui.utils',
        'ui.jq',
        'ngAnimate',
        'ngSanitize',
        'angularMoment',
        'ui.slimscroll',
        'ngJsTree',
        'angular-progress-button-styles',
        'daterangepicker',
        'ui.grid',
        'ui.grid.pagination',
        'ui.grid.selection',
        'abp'
    ]);

    //Configuration for Angular UI routing.
    galaxyModule.config([
        '$stateProvider', '$urlRouterProvider', '$locationProvider', '$qProvider','baSidebarServiceProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider, $qProvider, baSidebarServiceProvider) {
            $locationProvider.hashPrefix('');

            $urlRouterProvider.otherwise('/dashboard');

            $qProvider.errorOnUnhandledRejections(false);

            if (abp.auth.hasPermission('Pages.Users')) {
                $stateProvider
                    .state('users', {
                        url: '/users',
                        templateUrl: '/App/Admin/views/users/index.cshtml',
                        menu: 'Users',
                        displayName: app.localize('Users')
                    });
               }

            if (abp.auth.hasPermission('Pages.Tenants')) {
                $stateProvider
                    .state('tenants', {
                        url: '/tenants',
                        templateUrl: '/App/Admin/views/tenants/index.cshtml',
                        menu: 'Tenants',//Matches to name of 'Tenants' menu in CmsNavigationProvider
                        displayName: app.localize('Tenants')
                    });
                 }

            //无，权限判断的路由设置,name需要两个进行对应
            $stateProvider
                .state('home',
                {
                    url: '/home',
                    templateUrl: '/App/Admin/views/home/home.cshtml',
                    menu: 'Home', //Matches to name of 'Home' menu in CmsNavigationProvider
                    displayName: app.localize('Home')
                }).state('dashboard',
                {
                    url: '/dashboard',
                    templateUrl: '/App/Admin/views/dashboard/index.cshtml',
                    menu: 'Dashboard',
                    displayName: app.localize('Dashboard')
                })
                .state('about',
                {
                    url: '/about',
                    templateUrl: '/App/Admin/views/about/about.cshtml',
                    menu: 'About', //Matches to name of 'About' menu in CmsNavigationProvider,
                    displayName: app.localize('About')
                }).state('notifications',//对应浏览器地址栏的地址信息
                {
                    url: '/notifications',
                    templateUrl: '/App/Admin/views/notifications/index.cshtml',
                    menu: 'Notifications',//此处对应CmsNavigationProvider中的name
                    displayName: app.localize('Notifications')
                }).state('smsmanage',//对应浏览器地址栏的地址信息
                {
                    url: '/smsmanage',
                    templateUrl: '/App/Admin/views/smsmanage/index.cshtml',
                    menu: 'SmsManage',//此处对应CmsNavigationProvider中的name
                    displayName: app.localize('SmsManage')
                }).state('orgmanage',//对应浏览器地址栏的地址信息
                {
                    url: '/orgmanage',
                    templateUrl: '/App/Admin/views/organizationUnits/index.cshtml',
                    menu: 'OrgManage',//此处对应CmsNavigationProvider中的name
                    displayName: app.localize('OrgManage')
                }).state('rolemanage',//对应浏览器地址栏的地址信息
                {
                    url: '/rolemanage',
                    templateUrl: '/App/Admin/views/roleManage/index.cshtml',
                    menu: 'RoleManage',//此处对应CmsNavigationProvider中的name
                    displayName: app.localize('RoleManage')
                }).state('auditlog',//对应浏览器地址栏的地址信息
                {
                    url: '/auditlog',
                    templateUrl: '/App/Admin/views/auditlog/index.cshtml',
                    menu: 'AuditLog',//此处对应CmsNavigationProvider中的name
                    displayName: app.localize('AuditLog')
                });



            baSidebarServiceProvider.addStaticItem({
                title: 'Pages',
                icon: 'ion-document',
                subMenu: [{
                    title: 'Sign In',
                    fixedHref: 'dashboard',
                    blank: true
                }, {
                    title: 'Sign Up',
                    fixedHref: 'reg.html',
                    blank: true
                }, {
                    title: 'User Profile',
                    stateRef: 'profile'
                }, {
                    title: '404 Page',
                    fixedHref: '404.html',
                    blank: true
                }]
            });
            baSidebarServiceProvider.addStaticItem({
                title: 'Menu Level 1',
                icon: 'ion-ios-more',
                subMenu: [{
                    title: 'Menu Level 1.1',
                    disabled: true
                }, {
                    title: 'Menu Level 1.2',
                    subMenu: [{
                        title: 'Menu Level 1.2.1',
                        disabled: true
                    }]
                }]
            });
        }
    ]);


galaxyModule.run([
    "$rootScope",  "$state", 'i18nService', '$uibModalStack',
    function($rootScope,  $state, i18nService, $uibModalStack) {
        $rootScope.$state = $state;
        $rootScope.$state.current.title = "sdfsdfsd";
         
        //Set Ui-Grid language
        if (i18nService.get(abp.localization.currentCulture.name)) {
            i18nService.setCurrentLang(abp.localization.currentCulture.name);
        } else {
            i18nService.setCurrentLang("en");
        }

        $rootScope.safeApply = function (fn) {
            var phase = this.$root.$$phase;
            if (phase == '$apply' || phase == '$digest') {
                if (fn && (typeof (fn) === 'function')) {
                    fn();
                }
            } else {
                this.$apply(fn);
            }
        };

}]);