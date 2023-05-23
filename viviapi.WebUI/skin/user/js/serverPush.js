$(function(){


	// 接收
	$("a.receive").click(function(){
		var tr = $(this).parent().parent();
		var serverid = tr.attr("serverid");
		var id = tr.attr("id");
		var price = tr.children("td").eq(3).html();
		if (serverid.length>0 && id.length>0){
			$.dialog({
				title: "确认？",
				content: "确认以 <font color='red'>" + price + "元</font> 的价格接收此服务器？",
				icon: 'confirm',
				ok: function(){
					core.ajaxload({url:'/user/serverPush/receive.html?id=' + id});
				},
				cancel: true
			});
		}
	});


	// 拒绝
	$("a.refuse").click(function(){
		var tr = $(this).parent().parent();
		var serverid = tr.attr("serverid");
		var id = tr.attr("id");
		if (serverid.length>0 && id.length>0){
			$.dialog({
				title: "确认？",
				content: "确认拒绝接收此服务器？",
				icon: 'confirm',
				ok: function(){
					core.ajaxload({url:'/user/serverPush/refuse.html?id=' + id});
				},
				cancel: true
			});
		}
	});


	// 取消
	$("a.cancel").click(function(){
		var tr = $(this).parent().parent();
		var serverid = tr.attr("serverid");
		var id = tr.attr("id");

		if (serverid.length>0 && id.length>0){
			$.dialog({
				title: "确认？",
				content: "确认取消PUSH该服务器？",
				icon: 'confirm',
				ok: function(){
					core.ajaxload({url:'/user/serverPush/cancel.html?serverid=' + serverid + '&id=' + id});
				},
				cancel: true
			});
		}
	});
	

});