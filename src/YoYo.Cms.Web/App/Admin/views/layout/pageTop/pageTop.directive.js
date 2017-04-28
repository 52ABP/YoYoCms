/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

 galaxyModule
      .directive('pageTop', pageTop);

  /** @ngInject */
  function pageTop() {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/app/admin/views/layout/pageTop/pageTop.cshtml'
    };
  }

})();