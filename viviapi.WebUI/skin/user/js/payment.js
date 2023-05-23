$(function(){

	$(".payButton").click(function(){
		var val = parseFloat($("input[name='WIDtotal_fee']").val());
		var paymin = parseFloat($("input[name='WIDtotal_min']").val());

		if (val>0 && val<1000000){

			val = parseFloat(val);

			if(paymin>0 && val<paymin){
				$.dialog({
					title:'信息提示',
					content:'最低起充金额为 ' + paymin + ' 元',
					icon:'error',
					ok:true
				});
				return false;	
			}else{
				$("#payform").submit();
			}


		}else{

			$.dialog({
				title:'信息提示',
				content:'请填写正确的充值金额。',
				icon:'error',
				ok:true
			});
			return false;
		}



		
		
	});


	$(".paytypes").find("a.ico").click(function(){
		var radio = $(this).parent().parent().find("input[type='radio']").click();
	});

});