/**
 * @author v.lugovsky
 * created on 17.12.2015
 */
(function () {
  'use strict';

  galaxyModule
      .filter('appImage', appImage);

  /** @ngInject */
  function appImage(layoutPaths) {
    return function(input) {
      return layoutPaths.images.root + input;
    };
  }

})();
