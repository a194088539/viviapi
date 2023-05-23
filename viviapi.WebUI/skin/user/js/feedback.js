$(function(){


	$("#mymobile").click(function(){
		var val = $("#mobile").val();
		if($(this).attr("checked")){
			$("input[name='mobile']").attr("disabled", true).val(val);
		}else{
			$("input[name='mobile']").attr("disabled", false).val("");
		}
	});


	$("#myqq").click(function(){
		var val = $("#qq").val();
		if($(this).attr("checked")){
			$("input[name='qq']").attr("disabled", true).val(val);
		}else{
			$("input[name='qq']").attr("disabled", false).val("");
		}
	});




});