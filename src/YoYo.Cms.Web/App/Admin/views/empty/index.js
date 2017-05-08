(function () {
    yoyocmsModule.controller('app.views.layout.empty',

        ['$rootScope', '$scope', '$state', 'appSession', function ($rootScope, $scope, $state, appSession) {
            var vm = this;
            $state.current.title = $state.current.displayName;



            }
        ]);
})();