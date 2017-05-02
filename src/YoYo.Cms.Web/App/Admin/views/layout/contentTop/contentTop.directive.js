/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

 yoyocmsModule
     .directive('contentTop', ['$state', function ($state) {

          return {
              restrict: 'E',
              templateUrl: '/app/admin/views/layout/contentTop/contentTop.cshtml',
              link: function ($scope) {
                  $scope.$watch(function () {
                      $scope.activePageTitle = $state.current.title;
                  });
              }
          };


     }]);

 
})();