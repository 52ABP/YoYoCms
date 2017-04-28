var app = function() {

    var findMyPassword = function () {

        //#region 数据校验提交
        $('#findMypasswordForm').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'text-danger', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                UserName: {
                    required: true
                }
            },

            submitHandler: function (form) {
                window.location.href = "/Account/FindMyPasswordTypes?user=" + $('#UserName').val();
            }
        });

        //#endregion

        //#region 获取验证码
        $('#getValidCodeButton').on("click", function (e) {
            e.preventDefault();
            var phoneNumber = $("#tslinkphone");
            var $this = $(this);

            var smsMessageDto = {
                PhoneNumber: phoneNumber.val()
            }

            //if ($('#findMypasswordForm').valid()) {
                abp.ajax({
                    url: abp.appPath + 'Account/FindPasswordVailCodeForPhone',
                    type: 'Post',
                    data: smsMessageDto,
                    success: function () {
                        abp.message.success("获取验证码成功（123）");
                    }
                });

                $this.addClass("disabled");
                $this.attr("disabled", "disabled");
                $this.attr("data-second", 60);
                countDown($(this),
                    function (arr, obj) {
                        var second = arr[2];

                        second == "00" ? second = "60" : second;
                        obj[0].innerText = second + "秒后可重新发送";
                    },
                    function (obj) {
                        obj.removeClass("disabled");
                        obj.removeAttr("disabled");
                        obj[0].innerText = "获取验证码";
                    });
            //}
        });
        //#endregion

        //#region 倒计时
        function countDown(emment, dom, callback) {
            $(emment).each(function () {
                var me = $(this);
                var timeArr = [];
                function time() {
                    var seconds = me.attr("data-second");
                    if (isNaN(seconds)) return "";
                    if (seconds == 0) {
                        clearInterval(timeInter);
                        if (typeof callback != undefined) {
                            callback(me);
                        }
                        return;
                    }
                    var h, m, s;
                    h = Math.floor(seconds / 3600);
                    h < 10 ? h = "0" + h : h;
                    m = Math.floor((seconds - h * 3600) / 60);
                    m < 10 ? m = "0" + m : m;
                    s = Math.floor(seconds - h * 3600 - m * 60);
                    s < 10 ? s = "0" + s : s;
                    me.attr("data-second", seconds - 1);
                    timeArr[0] = h;
                    timeArr[1] = m;
                    timeArr[2] = s;
                    dom(timeArr, me);
                }
                var timeInter = setInterval(function () {
                    time();
                }, 1000);

            });

        }
        //#endregion
    }

    return {
        init: function () { findMyPassword() }
    };
}();