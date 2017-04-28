/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

 yoyocmsModule
      .directive('backTop', backTop);

  /** @ngInject */
  function backTop() {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/app/admin/views/layout/backTop/backTop.cshtml',
      controller: function () {
        $('#backTop').backTop({
          'position': 200,
          'speed': 100
        });
      }
    };
  }

})();