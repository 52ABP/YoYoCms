/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

yoyocmsModule
      .directive('baSidebar', baSidebar);

  /** @ngInject */
  function baSidebar($timeout, baSidebarService, baUtil, layoutSizes) {
    var jqWindow = $(window);
    return {
      restrict: 'E',
      templateUrl: '/app/admin/config/baSidebar/ba-sidebar.cshtml',
      controller: 'BaSidebarCtrl',
      link: function(scope, el) {

        scope.menuHeight = el[0].childNodes[0].clientHeight - 84;
        jqWindow.on('click', _onWindowClick);
        jqWindow.on('resize', _onWindowResize);

        scope.$on('$destroy', function() {
          jqWindow.off('click', _onWindowClick);
          jqWindow.off('resize', _onWindowResize);
        });

        function _onWindowClick($evt) {
          if (!baUtil.isDescendant(el[0], $evt.target) &&
              !$evt.originalEvent.$sidebarEventProcessed &&
              !baSidebarService.isMenuCollapsed() &&
              baSidebarService.canSidebarBeHidden()) {
            $evt.originalEvent.$sidebarEventProcessed = true;
            $timeout(function () {
              baSidebarService.setMenuCollapsed(true);
            }, 10);
          }
        }

        // watch window resize to change menu collapsed state if needed
        function _onWindowResize() {
          var newMenuCollapsed = baSidebarService.shouldMenuBeCollapsed();
          var newMenuHeight = _calculateMenuHeight();
          if (newMenuCollapsed != baSidebarService.isMenuCollapsed() || scope.menuHeight != newMenuHeight) {
            scope.$apply(function () {
              scope.menuHeight = newMenuHeight;
              baSidebarService.setMenuCollapsed(newMenuCollapsed)
            });
          }
        }

        function _calculateMenuHeight() {
          return el[0].childNodes[0].clientHeight - 84;
        }
      }
    };
  }

})();
(function() {
  'use strict';

  yoyocmsModule
      .provider('baSidebarService', [function () {
          var staticMenuItems = [];

          this.addStaticItem = function () {
              staticMenuItems.push.apply(staticMenuItems, arguments);
          };

          /** @ngInject */
          this.$get = function ($state, layoutSizes) {
              return new factory();

              function factory() {
                  var isMenuCollapsed = shouldMenuBeCollapsed();

                  this.getMenuItems = function () {
                      var states = defineMenuItemStates();
                      var menuItems = states.filter(function (item) {
                          return item.level == 0;
                      });

                      menuItems.forEach(function (item) {
                          var children = states.filter(function (child) {
                              return child.level == 1 && child.name.indexOf(item.name) === 0;
                          });
                          item.subMenu = children.length ? children : null;
                      });

                      return menuItems.concat(staticMenuItems);
                  };

                  this.shouldMenuBeCollapsed = shouldMenuBeCollapsed;
                  this.canSidebarBeHidden = canSidebarBeHidden;

                  this.setMenuCollapsed = function (isCollapsed) {
                      isMenuCollapsed = isCollapsed;
                  };

                  this.isMenuCollapsed = function () {
                      return isMenuCollapsed;
                  };

                  this.toggleMenuCollapsed = function () {
                      isMenuCollapsed = !isMenuCollapsed;
                  };

                  this.getAllStateRefsRecursive = function (item) {
                      var result = [];
                      _iterateSubItems(item);
                      return result;

                      function _iterateSubItems(currentItem) {
                          currentItem.items && currentItem.items.forEach(function (subItem) {
                              subItem.url && result.push(subItem.url);
                              _iterateSubItems(subItem);
                          });
                      }
                  };

                  function defineMenuItemStates() {
                      return $state.get()
                          .filter(function (s) {
                              return s.sidebarMeta;
                          })
                          .map(function (s) {
                              var meta = s.sidebarMeta;
                              return {
                                  name: s.name,
                                  title: s.title,
                                  level: (s.name.match(/\./g) || []).length,
                                  order: meta.order,
                                  icon: meta.icon,
                                  stateRef: s.name,
                              };
                          })
                          .sort(function (a, b) {
                              return (a.level - b.level) * 100 + a.order - b.order;
                          });
                  }

                  function shouldMenuBeCollapsed() {
                      return window.innerWidth <= layoutSizes.resWidthCollapseSidebar;
                  }

                  function canSidebarBeHidden() {
                      return window.innerWidth <= layoutSizes.resWidthHideSidebar;
                  }
              }

          };

      }]);

 
})();

/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

yoyocmsModule
    .controller('BaSidebarCtrl', ["$scope", "baSidebarService", function ($scope, baSidebarService) {


        $scope.MainMenu = abp.nav.menus.MainMenu;
        //$scope.defaultSidebarState = $scope.menuItems[0].stateRef ;
        //$scope.defaultSidebarState = "dashboard";

        //   console.log($scope.MainMenu);
        //  console.log(abp.nav.menus.MainMenu);

        $scope.con = function () {
            console.log("abp");
        }

        $scope.hoverItem = function ($event) {
            $scope.showHoverElem = true;
            $scope.hoverElemHeight = $event.currentTarget.clientHeight;
            var menuTopValue = 66;
            $scope.hoverElemTop = $event.currentTarget.getBoundingClientRect().top - menuTopValue;
        };

        $scope.$on('$stateChangeSuccess', function () {
            if (baSidebarService.canSidebarBeHidden()) {
                baSidebarService.setMenuCollapsed(true);
            }
        });
    }]);

  /** @ngInject */
  
})();
/**
 * @author v.lugovsky
 * created on 03.05.2016
 */
(function () {
  'use strict';

yoyocmsModule
    .directive('baSidebarToggleMenu', [
        "baSidebarService",
        function (baSidebarService) {
        return {
            restrict: 'A',
            link: function (scope, elem) {
                elem.on('click', function ($evt) {
                    $evt.originalEvent.$sidebarEventProcessed = true;
                    scope.$apply(function () {
                        baSidebarService.toggleMenuCollapsed();
                    });
                });
            }
        };
    }
])
    .directive('baSidebarCollapseMenu', ["baSidebarService", function (baSidebarService) {
        return {
            restrict: 'A',
            link: function (scope, elem) {
                elem.on('click', function ($evt) {
                    $evt.originalEvent.$sidebarEventProcessed = true;
                    if (!baSidebarService.isMenuCollapsed()) {
                        scope.$apply(function () {
                            baSidebarService.setMenuCollapsed(true);
                        });
                    }
                });
            }
        };
    }])
    .directive('baSidebarTogglingItem', ["baSidebarService", function () {
        return {
            restrict: 'A',
            controller: 'BaSidebarTogglingItemCtrl'
        };
    }
])
    .controller('BaSidebarTogglingItemCtrl', ["$scope", "$element", "$attrs", "$state", "baSidebarService", function ($scope, $element, $attrs, $state, baSidebarService) {
        var vm = this;
        var menuItem = vm.$$menuItem = $scope.$eval($attrs.baSidebarTogglingItem);
        if (menuItem && menuItem.items && menuItem.items.length) {
            vm.$$expandSubmenu = function () { console.warn('$$expandMenu should be overwritten by baUiSrefTogglingSubmenu') };
            vm.$$collapseSubmenu = function () { console.warn('$$collapseSubmenu should be overwritten by baUiSrefTogglingSubmenu') };

            var subItemsStateRefs = baSidebarService.getAllStateRefsRecursive(menuItem);

            vm.$expand = function () {
                vm.$$expandSubmenu();
                $element.addClass('ba-sidebar-item-expanded');
            };

            vm.$collapse = function () {
                vm.$$collapseSubmenu();
                $element.removeClass('ba-sidebar-item-expanded');
            };

            vm.$toggle = function () {
                $element.hasClass('ba-sidebar-item-expanded') ?
                    vm.$collapse() :
                    vm.$expand();
            };

            if (_isState($state.current)) {
                $element.addClass('ba-sidebar-item-expanded');
            }

            $scope.$on('$stateChangeStart', function (event, toState) {
                if (!_isState(toState) && $element.hasClass('ba-sidebar-item-expanded')) {
                    vm.$collapse();
                    $element.removeClass('ba-sidebar-item-expanded');
                }
            });

            $scope.$on('$stateChangeSuccess', function (event, toState) {
                if (_isState(toState) && !$element.hasClass('ba-sidebar-item-expanded')) {
                    vm.$expand();
                    $element.addClass('ba-sidebar-item-expanded');
                }
            });
        }

        function _isState(state) {
            return state && subItemsStateRefs.some(function (subItemState) {
                return state.name.indexOf(subItemState) == 0;
            });
        }
    }])
    .directive('baUiSrefTogglingSubmenu', ["$state", function ($state) {
        return {
            restrict: 'A',
            require: '^baSidebarTogglingItem',
            link: function (scope, el, attrs, baSidebarTogglingItem) {
                baSidebarTogglingItem.$$expandSubmenu = function () { el.slideDown(); };
                baSidebarTogglingItem.$$collapseSubmenu = function () { el.slideUp(); };
            }
        };
    }])
    .directive('baUiSrefToggler', ["baSidebarService", function (baSidebarService) {
        return {
            restrict: 'A',
            require: '^baSidebarTogglingItem',
            link: function (scope, el, attrs, baSidebarTogglingItem) {
                el.on('click', function () {


                    if (baSidebarService.isMenuCollapsed()) {
                        // If the whole sidebar is collapsed and this item has submenu. We need to open sidebar.
                        // This should not affect mobiles, because on mobiles sidebar should be hidden at all
                        scope.$apply(function () {
                            baSidebarService.setMenuCollapsed(false);
                        });
                        baSidebarTogglingItem.$expand();
                    } else {
                        baSidebarTogglingItem.$toggle();
                    }
                });
            }
        };
    }
]);

  

  /** @ngInject */
 

  /** @ngInject */

})();