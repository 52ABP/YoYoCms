//app.views.changeUserPassword.changeUserPassword.index

(function () {
    yoyocmsModule.controller("app.views.changeUserPassword.changeUserPassword.index", [
        "$scope", "$uibModalInstance", "abp.services.app.userAnalysis",
        function ($scope, $uibModalInstance, userAnalysisService) {
            var vm = this;
            vm.saving = false;



            vm.userPasswordInfo = {};

            vm.save= function() {
                vm.saving = true;
                userAnalysisService.changeUserPasswordAsync(vm.userPasswordInfo).then(function() {
                    abp.notify.info(app.localize('YourPasswordHasChangedSuccessfully'));
                    $uibModalInstance.close();


                }).finally(function() {
                    vm.saving = false;
                });
            }

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };


        }

    ]);


})();