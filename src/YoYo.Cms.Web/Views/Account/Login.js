var app = function() {

    var login = function() {

        $('#loginFormData').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'text-danger', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                EmailAddressInput: {
                    required: true,
                    isAccount:true
                },
                Password: {
                    required: true
                }
            },

            messages: {
                EmailAddressInput: {
                    required: "这是必填字段",
                },
                Password: "密码输入不一致，请重新填写"
            },

            submitHandler: function (e) {
                abp.ui.setBusy(null, abp.ajax({
                    url: abp.appPath + 'Account/Login',
                    type: 'Post',
                    data: JSON.stringify({
                        tenancyName: $('#TenancyName').val(),
                        usernameOrEmailAddress: $('#EmailAddressInput').val(),
                        password: $('#PasswordInput').val(),
                        rememberMe: $('#RememberMeInput').is(':checked'),
                        returnUrlHash: $('#ReturnUrlHash').val()
                    })

                })
                );

         
            }
        });
    }


    return {
        init: function() { login() }
    };
}();