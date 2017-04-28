(function () {

    var controllerId = "app.views.layout";
    angular.module('YoYoCms').controller(controllerId,
    [
        '$rootScope',"$scope", "$state", "appSession", function($rootScope,$scope, $state, appSession) {
            var vm = this;
            //Layout logic...
            vm.currentMenuDisplayName = $state.current.displayName;


 


            $scope.$watch('$viewContentLoaded', function () {
         

            });  

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
          //    console.log(toState);//请求的路径
             //   console.log(toParams);
         //       console.log(fromState);//请求前的路径
       //         console.log(fromParams);
        
                vm.currentMenuDisplayName = toState.displayName;
            });
             
        }
    ]);
})();