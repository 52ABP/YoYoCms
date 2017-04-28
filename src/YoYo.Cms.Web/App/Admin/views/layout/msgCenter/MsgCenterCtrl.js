 
(function () {
  'use strict';

 galaxyModule
      .controller('MsgCenterCtrl', msgCenterCtrl);

  /** @ngInject */
 function msgCenterCtrl($scope, $sce, appSession) {
     console.log(appSession);
     var vm = this;
     vm.languages = abp.localization.languages;
     vm.currentLanguage = abp.localization.currentLanguage;




     function init(parameters) {
         
     }


  }
})();