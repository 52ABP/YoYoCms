(function() {

    'use strict';
    yoyocmsModule.controller('app.pageTopCtrl', ['$scope', 'appSession',
        function ($scope, appSession) {
    
        $scope.languages = abp.localization.languages;
        $scope.currentLanguage = abp.localization.currentLanguage;

        $scope.changeLanguage=function(languageName) {
           console.log($scope.languages);

            location.href = abp.appPath + 'AbpLocalization/ChangeCulture?cultureName=' + languageName + '&returnUrl=' + window.location.pathname + window.location.hash;
        };
   

        $scope.con = function () {
            console.log("d");
        }


        }
    ]);
 

})();