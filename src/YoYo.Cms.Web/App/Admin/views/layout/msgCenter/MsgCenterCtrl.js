 
(function () {
  'use strict';

 yoyocmsModule
      .controller('MsgCenterCtrl', msgCenterCtrl);

  /** @ngInject */
 function msgCenterCtrl($scope, $sce, appSession) {
     console.log(appSession);
     var vm = this;
     vm.languages = abp.localization.languages;
     vm.currentLanguage = abp.localization.currentLanguage;

     vm.ddd= function() {
         console.log("dsfdsa");
     }
     $scope.users = {
         0: {
             name: 'Vlad',
         },
         1: {
             name: 'Kostya',
         },
         2: {
             name: 'Andrey',
         },
         3: {
             name: 'Nasta',
         }
     };

     $scope.notifications = [
         {
             userId: 0,
             template: '&name posted a new article.',
             time: '1 min ago'
         },
         {
             userId: 1,
             template: '&name changed his contact information.',
             time: '2 hrs ago'
         },
         {
             image: 'app/assets/img/shopping-cart.svg',
             template: 'New orders received.',
             time: '5 hrs ago'
         },
         {
             userId: 2,
             template: '&name replied to your comment.',
             time: '1 day ago'
         },
         {
             userId: 3,
             template: 'Today is &name\'s birthday.',
             time: '2 days ago'
         },
         {
             image: 'app/assets/img/comments.svg',
             template: 'New comments on your post.',
             time: '3 days ago'
         },
         {
             userId: 1,
             template: '&name invited you to join the event.',
             time: '1 week ago'
         }
     ];


     function init(parameters) {
         
     }


  }
})();