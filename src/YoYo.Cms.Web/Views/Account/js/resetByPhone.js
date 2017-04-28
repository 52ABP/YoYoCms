!(function($){
    var App=function(){
        return {
            //初始化表单验证
            initForm:function(){
                 $('.phoneResetForm').validate({
                    rules:{
                       
                    },
                    messages:{
                       phoneNumber:{
                           required:'请输入已绑定手机号'
                       }
                    }
                });
                $('.resetPasswordButton').on('click',function(){                           
                    if($('.phoneResetForm').valid()){
                        
                    }else{                        
                        $('.form-errorMessage').show();
                    }
                });
                
                $('.icon-guanbi').on('click',function(){
                   $('.form-errorMessage').slideUp();
                })
            },
            init:function(){
                this.initForm();
            }
        }
    }();
    $(function(){
        App.init();
    })
}(jQuery));