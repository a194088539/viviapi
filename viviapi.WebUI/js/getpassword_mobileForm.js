$(document).ready(function() { 

	
	var sendtime = $("#sendtime").val();
	
	$("#sendcode").ajaxsend({
	url: '/webservice/sendMobileCode.ashx',
		data:function(){
			return {
				mobile:$("#mobile").val()
			}
		},
		time:60,
		restTime:sendtime,
		text:'再次发送效验码',
		success:function(json){
			$.dialog.tips(json.text, 2, (json.result ? 'success' : 'error'));
		},
		error:function(){
			alert('发送失败');
		}
	});


	$("input.submit").live("click", function(){
		var button = $(this);
		var form = $(this).parents("form");
		if (form.length>0){
			form.ajaxform({
				"button": button
			});
		}
	});


});