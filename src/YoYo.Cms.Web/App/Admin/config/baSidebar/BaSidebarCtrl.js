/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

yoyocmsModule
    .controller('BaSidebarCtrl', ["$scope", "baSidebarService", function ($scope, baSidebarService) {


        $scope.MainMenu = abp.nav.menus.MainMenu;
        //$scope.defaultSidebarState = $scope.menuItems[0].stateRef ;
        //$scope.defaultSidebarState = "dashboard";

        //   console.log($scope.MainMenu);
        //  console.log(abp.nav.menus.MainMenu);

        $scope.con = function () {
            console.log("abp");
        }

        $scope.hoverItem = function ($event) {
            $scope.showHoverElem = true;
            $scope.hoverElemHeight = $event.currentTarget.clientHeight;
            var menuTopValue = 66;
            $scope.hoverElemTop = $event.currentTarget.getBoundingClientRect().top - menuTopValue;
        };

        $scope.$on('$stateChangeSuccess', function () {
            if (baSidebarService.canSidebarBeHidden()) {
                baSidebarService.setMenuCollapsed(true);
            }
        });
    }]);

  /** @ngInject */
  
})();