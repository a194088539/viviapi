//if (top.location == self.location) { 
//	top.location='http://www.xiaoniaoyun.com/';
//}


$(function(){
	
	var tabs = $("#tabs");
	var verifytype = '';
	

	var sendtime = $("#sendtime").val();


	tabs.tab({cookie:true,  disabled:true})


	tabs.find("a").click(function(){

		verifytype = $(this).attr('name');
		
		$("#verifytype").val(verifytype);

		switch(verifytype){
			
			case 'email':

				break;
	
			case 'mobile':

				break;
	
			case 'protection':
				
				break;
	
			default:
				
				break;
			
		}

	}).eq(0).click();




	$("#email_send").ajaxsend({
	    url: '/user/Service/sendtixianemail.ashx',
		time:60,
		restTime:sendtime,
		text:'再次发送效验码',
		success:function(json){
			$.dialog.tips(json.text, 2, (json.result ? 'success' : 'error'));
		}
	});



	$("#sms_send").ajaxsend({
	url: '/user/Service/sendtixianCode.ashx',
		time:60,
		restTime:sendtime,
		text:'再次发送效验码',
		success:function(json){
			$.dialog.tips(json.text, 2, (json.result ? 'success' : 'error'));
		}
	});








});