
$(function(){


	$(".next").click(function(){

		var orderidArr = [];

		$("[name='orderid']").each(function(){
			if($(this).attr("checked")){ 
				orderidArr.push($(this).val());
			}
		});

		if (orderidArr.length==0){
			core.error("请选择订单");
			return false;
		}

		var orderidStr = orderidArr.join();


		core.tips("请稍后……", 1, "loading", function(){

			window.location = "/user/invoice/apply.html?orderid=" + orderidStr;

		})


	});


});

