/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

 yoyocmsModule
      .directive('pageTop', pageTop);

  /** @ngInject */
  function pageTop() {
    return {
        restrict: 'AE',
        templateUrl: '/app/admin/views/layout/pageTop/pageTop.cshtml',
        controller: 'app.pageTopCtrl'
    };
  }

})();