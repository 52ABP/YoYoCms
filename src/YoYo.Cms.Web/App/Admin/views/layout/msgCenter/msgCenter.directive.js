/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

 galaxyModule
      .directive('msgCenter', msgCenter);

  /** @ngInject */
  function msgCenter() {
    return {
      restrict: 'E',
      templateUrl: '/app/admin/views/layout/msgCenter/msgCenter.cshtml',
      controller: 'MsgCenterCtrl'
    };
  }

})();