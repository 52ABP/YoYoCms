!(function($){
    var App=function(){
        return {
            //初始化表单验证
            initForm:function(){
                 $('.emailResetForm').validate({
                    rules:{
                       
                    },
                    messages:{
                       
                    }
                });
                $('.resetPasswordButton').on('click',function(){                           
                    if($('.emailResetForm').valid()){
                        $('.state1').hide();
                        $('.state2').show();
                    }else{                        
                        
                    }
                });
                
                
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