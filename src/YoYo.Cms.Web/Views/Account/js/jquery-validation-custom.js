(function ($) {
    if (!$.validator)
    {
        return;
    }
    // 匹配字母、数字和下划线
    $.validator.addMethod("isAccount", function (value, element) {
        return this.optional(element) || /^[A-Za-z0-9_]+$/.test(value);
    }, "只能包含字母数字和下划线");
    //验证以字母开头
    $.validator.addMethod("beginWithLetter", function (value, param, element) {
        return /^[a-zA-Z]/.test(value);
    }, "用户名必须以字母开头");

    //判断空格
    $.validator.addMethod("noSpace", function (value, param, element) {
        return /\s/.test(value);
    }, "不能输入空格");

    //验证手机号码
    $.validator.addMethod("phoneNumber", function (value, param, element) {
        return /^1[34578]\d{9}$/.test(value);
    }, "请输入正确的手机号码");

    //用户名验证
    $.validator.addMethod("userName", function (value, param, element) {
        return /^[A-Za-z0-9\u4e00-\u9fa5]+$/.test(value);
    }, "用户名只能由字母数字及汉字组成");

    //中文验证
    $.validator.addMethod("chinese", function (value, param, element) {
        return /^[\u4e00-\u9fa5]+$/.test(value);
    }, "联系人只能输入中文");

    /*
     * @function 自定义正则表达式验证，如果一个对象有多个正则表达式的话，则增加一个名字为regexTwo的方法即可
     * @param value 当前元素的值
     * @param element 当前元素对象
     * @param param 自定义参数，格式：{pattern:/\d{4}/}
     */
    $.validator.addMethod("regex", function (value, element, param) {
        var pattern = param.pattern; //正则表达式
        return this.optional(element) || (pattern.test(value));
    }, "不符合正则表达式规则，请重新填写！");

    /*
     * @function 自定义服务器请求验证
     * @param value 当前元素的值
     * @param element 当前元素对象
     * @param param 自定义参数，格式：{url:""}
     */
    $.validator.addMethod("remoteUrl", function (value, element, param) {
        //请求服务器地址
        var requestUrl = param.url;
        //请求服务器的数据
        var requestData = param.data;
        //验证的定义
        if (param.isJsonAutoValueKey.length === 0) return false;
        if (param.url.length === 0) return false;
        //处理请求的数据对象
        requestData[param.isJsonAutoValueKey] = value;
        //是否验证成功状态
        var isValid = false;
        $.ajax({
            url: requestUrl,
            type: "POST",
            async: false, //取消异步
            dataType: "JSON",
            data: requestData,
            success: function (data) {
                isValid = param.analysis(data);
            },
            error: function () {
                //这里自己去根据调试
                console.log("调用服务器处理发生错误信息...");
            }
        });
        //$.extend(); //覆盖对象
        return this.optional(element) || isValid;
    }, "不符合后台服务器验证规则，请重新填写！");



    $.validator.setDefaults({
        errorElement: 'span',
        errorClass: 'help-block help-block-validation-error',
        focusInvalid: false,
        //失去焦点的时候执行的函数
        onfocusout: function (element) {
            $(element).valid();
        },
        submitOnKeyPress: true,
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },

        unhighlight: function (element) {
         
            $(element).closest('.form-group').removeClass('has-error');
        },

        errorPlacement: function (error, element) {
            if (element.closest('.input-icon').size() === 1)
            {   console.log("d");
                error.insertAfter(element.closest('.input-icon'));
            } else
            {
                error.insertAfter(element);
            }
        },

        success: function (label) {
            label.closest('.form-group').removeClass('has-error');
            label.remove();
        },

        submitHandler: function (form) {
            $(form).find('.alert-danger').hide();
        }
    });
})(jQuery);