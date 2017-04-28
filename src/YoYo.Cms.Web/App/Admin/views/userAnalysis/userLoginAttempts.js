(function () {
    yoyocmsModule.controller("app.views.userAnalysis.userLoginAttempts.index", [
        "$scope", "$uibModalInstance", "abp.services.app.userAnalysis",
        function ($scope, $uibModalInstance, userAnalysisService) {
        var vm = this;
        vm.loginAttemptRecords = [];
    
        vm.getUserLoginAttemptsRecords = function () {
            userAnalysisService.getRecentUserLoginAttemptsTenRecordsAsync({}).then(function (result) {
              //  console.log(result.data.items);
                vm.loginAttemptRecords = result.data.items;

                
            });
        };

        vm.getLoginAttemptClass = function (loginAttempt) {
            return loginAttempt.result === 'Success' ? 'label-success' :
                'label-danger';
        }

        vm.getCreationTime = function (loginAttempt) {
            return moment(loginAttempt.creationTime).fromNow() + ' (' + moment(loginAttempt.creationTime).format('YYYY-MM-DD hh:mm:ss') + ')';
        };


        vm.formatLoginAttemptResult = function (loginAttemt) {
            return loginAttemt.result === 'Success' ? app.localize('Success') :
                app.localize('Failed');
        }

        //关闭模态框
        vm.close = function () {
            $uibModalInstance.close();
        };

            vm.getUserLoginAttemptsRecords();

        }
    ]);

})();