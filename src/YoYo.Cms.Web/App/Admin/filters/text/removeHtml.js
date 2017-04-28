/**
 * @author a.demeshko
 * created on 23.12.2015
 */
(function () {
  'use strict';

  galaxyModule
    .filter('plainText', plainText);

  /** @ngInject */
  function plainText() {
    return function(text) {
      return  text ? String(text).replace(/<[^>]+>/gm, '') : '';
    };
  }

})();
