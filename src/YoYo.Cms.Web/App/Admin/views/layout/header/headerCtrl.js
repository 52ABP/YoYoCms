(function() {

    'use strict';
    yoyocmsModule.controller('app.layout.header', ["$scope", "appSession", function ($scope, appSession) {

        var vm = this;




        vm.languages = abp.localization.languages;
        vm.currentLanguage = abp.localization.currentLanguage;
        vm.changeLanguage = function (languageName) {
            console.log($scope.languages);
            location.href = abp.appPath + 'AbpLocalization/ChangeCulture?cultureName=' + languageName + '&returnUrl=' + window.location.pathname + window.location.hash;
        };

        vm.getUserName = appSession.user.userName;

 //       sidebar - toggle - menu
        vm.toggleSidebarMenu= function() {
            if ($("body > main").hasClass("menu-collapsed")) {
                $("body > main").removeClass("menu-collapsed");
            } else {

                $("body > main").addClass("menu-collapsed");


            }
         



        }

        }
    ]);
 

})();