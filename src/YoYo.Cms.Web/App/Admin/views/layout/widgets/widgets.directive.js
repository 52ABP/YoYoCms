/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

 yoyocmsModule
      .directive('widgets', widgets);

  /** @ngInject */
  function widgets() {
    return {
      restrict: 'EA',
      scope: {
        ngModel: '='
      },
      templateUrl: '/app/admin/views/layout/widgets/widgets.cshtml',
      replace: true
    };
  }

})();