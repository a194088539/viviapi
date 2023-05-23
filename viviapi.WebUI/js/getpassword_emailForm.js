$(function() { 


	var sendtime = $("#sendtime").val();

	$("#sendcode").ajaxsend({
	url: '/webservice/sendMailCode.ashx',
		data:function(){
			return {
				email:$("#email").val()
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



});