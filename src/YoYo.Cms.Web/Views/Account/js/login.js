!(function($){
    var App=function(){
        return {
            //初始化表单验证
            initForm:function(){
                 $('.loginForm').validate({
                    rules:{
                       
                    },
                    messages:{
                       account:{
                           required:'请输入登录账号'
                       }
                    }
                });
                $('.registerButton').on('click',function(){                           
                    if($('.loginForm').valid()){
                        
                    }else{                        
                        $('.form-errorMessage').show();
                    }
                });
                $('.icon-squarecheckfill').on('click',function(){
                    var $this=$(this);
                    if($this.hasClass('checked')){
                        $this.removeClass('checked');
                       
                    }else{
                        $this.addClass('checked');
                        
                    }
                })
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