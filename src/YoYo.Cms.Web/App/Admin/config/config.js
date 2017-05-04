 
(function () {
    'use strict';

    yoyocmsModule
        .config(config);

    /** @ngInject */
    function config(baConfigProvider, colorHelper, $provide) {
        $provide.decorator('$uiViewScroll', uiViewScrollDecorator);
        //  baConfigProvider.changeTheme({blur: true});
        //
        //baConfigProvider.changeColors({
        //  default: 'rgba(#000000, 0.2)',
        //  defaultText: '#ffffff',
        //  dashboard: {
        //    white: '#ffffff',
        //  },
        //});
    }

    /** @ngInject */
    function uiViewScrollDecorator($delegate, $anchorScroll, baUtil) {
        return function (uiViewElement) {
            if (baUtil.hasAttr(uiViewElement, "autoscroll-body-top")) {
                $anchorScroll();
            } else {
                $delegate(uiViewElement);
            }
        };
    }
})();
