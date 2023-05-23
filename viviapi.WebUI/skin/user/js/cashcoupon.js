$(function(){

	$("#active").click(function(){


		$.dialog.open('/user/cashcoupon/activeFrame.html', {
			title: '激活代金券',
			width:500,
			height:90,
			okVal:'确定激活',
			ok:function(){
				var iframe = this.iframe.contentWindow;				
				var body = $(iframe.document.body);
				var code = body.find("[name='code']").val();
				if (code.length==0){
					core.error("请填写代金券号");
				}else{

					core.ajaxload({
						url:'/user/cashcoupon/active.html?code=' + code
					});

				}

				return false;
			},
			cancel:true
		});


	});

});