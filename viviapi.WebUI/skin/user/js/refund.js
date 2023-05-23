$(function(){


	$("a.cancel").click(function(){
		var tr = $(this).parent().parent();
		var id = tr.attr("id");

		if (id.length>0){
			$.dialog({
				title: "确认？",
				content: "取消该退款申请？",
				icon: 'confirm',
				ok: function(){
					core.ajaxload({url:'/user/refund/cancel.html?id=' + id});
				},
				cancel: true
			});
		}
	});
	

});