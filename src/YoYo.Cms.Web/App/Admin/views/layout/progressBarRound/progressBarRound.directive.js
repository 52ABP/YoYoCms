﻿/**
 * Created by n.poltoratsky
 * on 28.06.2016.
 */
(function () {
    'use strict';

   galaxyModule
        .directive('progressBarRound', progressBarRound);

    /** @ngInject */
    function progressBarRound(baProgressModal) {
        return {
            restrict: 'E',
            templateUrl: '/app/admin/views/layout/progressBarRound/progressBarRound.cshtml',
            link:function($scope, element, attrs) {
                $scope.baProgressDialog = baProgressModal;
                $scope.$watch(function () {
                    return baProgressModal.getProgress();
                }, animateBar);

                function animateBar() {
                    var circle = element.find('#loader')[0];
                    circle.setAttribute("stroke-dasharray", baProgressModal.getProgress() * 180 * Math.PI / 100 + ", 20000");
                    $scope.progress = baProgressModal.getProgress();
                }
            }
        }
    }
})();