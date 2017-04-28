var app = function() {

    var findMyPassword = function () {

        $('.findpwd-text').html("你正在找回" + GetQueryString("user"))

        $('#findPwdByPhone').click(function () {
        });

        $('#findPwdByEmail').click(function () {
            abp.ui.setBusy(null,
               abp.services.app.findMyPassword.resetPasswordByEmail({ "userName": GetQueryString("user") })
               .then(function (result) {
                   abp.message.success("邮件已发送至邮箱：" + result + "请进入邮箱进行验证");
               })
           );
        });

        function GetQueryString(name) {
            var url = location.search; 
            var theRequest = new Object();
            if (url.indexOf("?") != -1) {
                var str = url.substr(1);
                strs = str.split("&");
                for (var i = 0; i < strs.length; i++) {
                    theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
                }
            }
            return theRequest[name];
        }

    }

    return {
        init: function () { findMyPassword() }
    };
}();