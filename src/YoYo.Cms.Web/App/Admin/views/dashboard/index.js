(function() {

    yoyocmsModule.controller('app.views.dashboard.index',
        ['$scope', '$uibModal', 'abp.services.app.user',
            function ($scope, $uibModal, userService) {

                var vm = this;

                vm.users = [];

                $scope.$on('$viewContentLoaded', function () {
                    console.log("abp");
              //     App.initAjax();//模板页面的初始化
                });

              
                

             

        }
    ]);

})();