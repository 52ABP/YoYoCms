(function () {
    angular.module('ourGalaxy').directive('autoFocus', function () {
          return {
              restrict: 'A',
              link: function ($scope, element) {
                  element[0].focus();
              }
          };
      });
})();