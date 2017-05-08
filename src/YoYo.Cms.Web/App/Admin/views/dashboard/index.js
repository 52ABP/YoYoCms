(function() {

    yoyocmsModule.controller('app.views.dashboard.index',
        ['$scope','$state', '$uibModal', 'abp.services.app.user',
            function ($scope, $state, $uibModal, userService) {

                var vm = this;
                $state.current.title = $state.current.displayName;

                vm.users = [];

                $scope.$on('$viewContentLoaded', function () {
                    console.log("abp");
              //     App.initAjax();//模板页面的初始化
                });

              
                

             

        }
    ]);

})();