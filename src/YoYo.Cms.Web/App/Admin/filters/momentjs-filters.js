// need load the moment.js to use these filters. 

(function () {

    yoyocmsModule.filter('momentFormat', function () {
        return function (date, formatStr) {
            if (!date) {
                return '-';
            }
            
            return moment(date).format(formatStr);
        };
    })
    .filter('fromNow', function () {
        return function (date) {
            return moment(date).fromNow();
        };
    });

})();