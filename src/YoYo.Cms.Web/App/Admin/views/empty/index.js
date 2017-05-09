(function () {
    yoyocmsModule.controller('app.views.layout.empty',

        ['$scope', '$state', 'appSession', '$uibModal', 'uiGridConstants', 'abp.services.app.empty',

            function ($scope, $state, appSession, $uibModal, uiGridConstants, auditLogService) {
                var vm = this;
                $state.current.title = $state.current.displayName;
                vm.loading = false;
                vm.advancedFiltersAreShown = false;
                vm.requestParams = {
                    userName: '',
                    serviceName: '',
                    methodName: '',
                    browserInfo: '',
                    hasException: '',
                    skipCount: 0,
                    maxResultCount: app.consts.grid.defaultPageSize,
                    sorting: null
                };

                vm.dateRangeOptions = app.createDateRangePickerOptions();
                vm.dateRangeModel = {
                    startDate: moment().startOf('day'),
                    endDate: moment().endOf('day')
                };

            }
        ]);
})();