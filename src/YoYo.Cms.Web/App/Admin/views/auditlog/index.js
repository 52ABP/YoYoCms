(function () {
    yoyocmsModule.controller('app.views.layout.auditlog',

        ['$scope', '$state', 'appSession', '$uibModal', 'uiGridConstants', 'abp.services.app.user',

            function ($scope, $state, appSession, $uibModal, uiGridConstants, auditLogService) {
            var vm = this;
            $state.current.title = $state.current.displayName;



        }
        ]);
})();