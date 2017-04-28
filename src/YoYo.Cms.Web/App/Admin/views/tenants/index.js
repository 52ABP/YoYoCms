(function() {
    yoyocmsModule.controller('app.views.tenants.index', [
        '$scope', '$uibModal', 'abp.services.app.tenant',
        function ($scope, $uibModal, tenantService) {
            var vm = this;

            vm.tenants = [];

            function getTenants() {
                tenantService.getTenants({}).then(function (result) {
                    vm.tenants = result.data.items;
                });
            }

            vm.openTenantCreationModal = function() {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/tenants/createModal.cshtml',
                    controller: 'app.views.tenants.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    getTenants();
                });
            };

            getTenants();
        }
    ]);
})();