(function() {

    'use strict';
    yoyocmsModule.controller('app.layout.header',header);

    function header($scope, appSession) {
        var vm = this;
        vm.languages = abp.localization.languages;
        vm.currentLanguage = abp.localization.currentLanguage;

        vm.changeLanguage = function (languageName) {
            console.log($scope.languages);

            location.href = abp.appPath + 'AbpLocalization/ChangeCulture?cultureName=' + languageName + '&returnUrl=' + window.location.pathname + window.location.hash;
        };


        vm.con = function () {
            console.log("d");
        }

    }

})();