


$(".name").click(function(){

	var serverid = $(this).parent().parent().parent().attr("id");

	reName(serverid);

});






$("input[type='checkbox'][name='id']").change(function(){

	if ($("input[name='id']:checked").length>0){
		$("#restart_check").removeClass("Background_fei");
	}else{
		$("#restart_check").addClass("Background_fei");
	}

});




$("#restart_check").click(function(){

    var arr = new Array();
    var num = 0;
	$("input[name='id']").each(function(){
		if ($(this).attr("checked")) {
			arr.push($(this).attr('value'));
			num++;
		}
	});

	var ids = arr.join(",")


	if(num==0){
		core.tips("请选择要重启的服务器", 1, "alert")
		return false;
	}


	if(num>5){
		core.alert("一次无法同时重启超过5台服务器");
		return false;
	}


	$.dialog({
		title: "确认？",
		content: "确认重新启动所选中的 " + num + " 台服务器？",
		icon: 'confirm',
		ok: function(){

			core.ajaxload({
				tips:'正在发送命令，请稍后……',
				type:'POST',
				data:{
					'a':'batchRestart',
					'id':ids,
					'rnd':Math.random
				},
				url:'/user/serverRestart/'
			});

		},
		cancel: true
	});


});











$("#btn_stop").click(function(){

    var arr = new Array();
    var num = 0;
	$("input[name='id']").each(function(){
		if ($(this).attr("checked")) {
			arr.push($(this).attr('value'));
			num++;
		}
	});

	var ids = arr.join(",")


	if(num==0){
		core.tips("请选择要关机的服务器", 1, "alert")
		return false;
	}


	if(num>5){
		core.alert("一次无法同时关机超过5台服务器");
		return false;
	}


	$.dialog({
		title: "确认？",
		content: "确认将所选中的 " + num + " 台服务器关机？",
		icon: 'confirm',
		ok: function(){

			core.ajaxload({
				tips:'正在发送命令，请稍后……',
				type:'POST',
				data:{
					'a':'batchStop',
					'id':ids,
					'rnd':Math.random
				},
				url:'/user/serverStop/'
			});

		},
		cancel: true
	});


});






$("#btn_start").click(function(){

    var arr = new Array();
    var num = 0;
	$("input[name='id']").each(function(){
		if ($(this).attr("checked")) {
			arr.push($(this).attr('value'));
			num++;
		}
	});


	var ids = arr.join(",")


	if(num==0){
		core.tips("请选择要启动的服务器", 1, "alert")
		return false;
	}


	if(num>5){
		core.alert("一次无法同时启动超过5台服务器");
		return false;
	}


	$.dialog({
		title: "确认？",
		content: "确认启动所选中的 " + num + " 台服务器？",
		icon: 'confirm',
		ok: function(){

			core.ajaxload({
				tips:'正在发送命令，请稍后……',
				type:'POST',
				data:{
					'a':'batchStart',
					'id':ids,
					'rnd':Math.random
				},
				url:'/user/serverStart/'
			});

		},
		cancel: true
	});


});
















$(".restartOne").live("click", function(){


	var serverid = $(this).parent().parent().attr("id");

	var orderStatus = $(this).parent().parent().attr("orderStatus");
	var openStatus = $(this).parent().parent().attr("openStatus");
	var upgradeStatus = $(this).parent().parent().attr("upgradeStatus");
	var installStatus = $(this).parent().parent().attr("installStatus");



	if (orderStatus==-1){
		core.alert("服务器已过期，请续费！");
		return false;
	}

	if (openStatus==0){
		core.alert("服务器尚未开通，暂时无法重启！");
		return false;
	}

	if (upgradeStatus==1){
		core.alert("服务器正在升级，暂时无法重启！");
		return false;
	}

	if (installStatus==1){
		core.alert("服务器正在重装，暂时无法重启！");
		return false;
	}


	$.dialog({
		title: "确认？",
		content: "确认重新启动服务器？",
		icon: 'confirm',
		ok: function(){

			core.ajaxload({
				tips:'正在发送命令，请稍后……',
				type:'POST',
				data:{
					'a':'restart',
					'id':serverid,
					'rnd':Math.random
				},
				url:'/user/serverRestart/'
			});

		},
		cancel: true
	})
});













$(".reNameOne").live("click", function(){

	var serverid = $(this).parent().parent().attr("id");

	reName(serverid);

});












var reName = function(serverID){

	$.dialog.open('/user/serverName/?id=' + serverID, {
		title:'自定义云服务器名称',
		width:500,
		height:200,
		okVal:'修改',
		ok:function(){
			var that = this;
			var iframe = this.iframe.contentWindow;
			var form = $(iframe.document).find("form");				


			form.ajaxform({
				button: null
			});

			return false;

		},
		cancel:true
	});

};




$("#search_area").change(function(){
	var val = $(this).val();
	window.location = "/user/serverManage/?line=" + val;
});



