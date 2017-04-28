var app = function () {

    var register = function () {
        $('.register-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'text-danger', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                UserName: {
                    required: true
                }, PasswordRepeat: {
                    equalTo: "#Password"
                },
                Password: {
                    required: true,
                    rangelength: [8, 16],
                    regex: {
                        pattern: /[0-9]+[a-zA-Z]+[0-9a-zA-Z]*|[a-zA-Z]+[0-9]+[0-9a-zA-Z]*/,
                    }
                }
            },

            messages: {
                EmailAddress: {
                    required: "这是必填字段"
                },
                Password: {
                    required:"这是必填字段",
                    regex: "密码必须包含数字和字母（区分大小写）！",
                    rangelength: "请设置8-16位密码"
                },
                PasswordRepeat: "密码输入不一致，请重新填写"
            },

            highlight: function (element) {
                $(element).closest('.form-group').addClass('has-error');
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            submitHandler: function (form) {
                form.submit();
            }
        });
    }


    return {
        init: function () { register() }
    };
}();
 

       


