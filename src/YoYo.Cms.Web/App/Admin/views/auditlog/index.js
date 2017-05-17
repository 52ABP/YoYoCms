(function () {
    yoyocmsModule.controller('app.views.layout.auditlog',

        ['$scope', '$state', 'appSession', '$uibModal', 'uiGridConstants', 'abp.services.app.auditLog',
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

            vm.dateRangeTime = {
                startDate: moment().startOf('day'),
                endDate: moment().endOf('day')
            };

           



            vm.auditlogGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                appScopeProvider: vm,
                columnDefs: [
                    {
                        name: 'Actions',
                        enableSorting: false,
                        width: 50,
                        headerCellTemplate: '<span></span>',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents text-center\">' +
                        '  <button class="btn btn-default btn-xs" ng-click="grid.appScope.showDetails(row.entity)"><i class="fa fa-search"></i></button>' +
                        '</div>'
                    },
                    {
                        field: 'exception',
                        enableSorting: false,
                        width: 30,
                        headerCellTemplate: '<span></span>',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents text-center\">' +
                        '  <i class="fa fa-check-circle font-green" ng-if="!row.entity.exception"></i>' +
                        '  <i class="fa fa-warning font-yellow-gold" ng-if="row.entity.exception"></i>' +
                        '</div>'
                    },
                    {
                        name: app.localize('Time'),
                        field: 'executionTime',
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm:ss\'',
                        minWidth: 150
                    },
                    {
                        name: app.localize('UserName'),
                        field: 'userName',
                        minWidth: 150
                    },
                    {
                        name: app.localize('Service'),
                        enableSorting: false,
                        field: 'serviceName',
                        minWidth: 200
                    },
                    {
                        name: app.localize('Action'),
                        enableSorting: false,
                        field: 'methodName',
                        minWidth: 200
                    },
                    {
                        name: app.localize('Duration'),
                        field: 'executionDuration',
                        width: 80,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        app.localize('Xms', '{{COL_FIELD CUSTOM_FILTERS}}') +
                        '</div>'
                    },
                    {
                        name: app.localize('IpAddress'),
                        field: 'clientIpAddress',
                        enableSorting: false,
                        minWidth: 120
                    },
                    {
                        name: app.localize('Client'),
                        field: 'clientName',
                        enableSorting: false,
                        minWidth: 140
                    },
                    {
                        name: app.localize('Browser'),
                        field: 'browserInfo',
                        enableSorting: false,
                        minWidth: 200
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (!sortColumns.length || !sortColumns[0].field) {
                            vm.requestParams.sorting = null;
                        } else {
                            vm.requestParams.sorting = sortColumns[0].field + ' ' + sortColumns[0].sort.direction;
                        }

                        vm.getAuditLogs();
                    });
                    gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                        vm.requestParams.skipCount = (pageNumber - 1) * pageSize;
                        vm.requestParams.maxResultCount = pageSize;

                        vm.getAuditLogs();
                    });
                },
                data: []
            };


            vm.getAuditLogs = function () {
                vm.loading = true;
                if ((typeof vm.dateRangeTime) =="number") {
                    vm.dateRangeTime = {
                        startDate: moment().startOf('day'),
                        endDate: moment().endOf('day')
                    };
                }
                
                
                auditLogService.getPagedAuditLogsAsync($.extend({}, vm.requestParams, vm.dateRangeTime))
                    .then(function (result) {
                        vm.auditlogGridOptions.totalItems = result.data.totalCount;
                        vm.auditlogGridOptions.data = result.data.items;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };


            vm.showDetails = function (auditLog) {
                $uibModal.open({
                    templateUrl: '~/App/Admin/views/auditlog/detailModal.cshtml',
                    controller: 'app.views.auditLogs.detailModal as vm',
                    backdrop: 'static',
                    resolve: {
                        auditLog: function () {
                            return auditLog;
                        }
                    }
                });
            };



            vm.getAuditLogs();







        }
        ]);
})();