$(function(){

	$("#create").click(function(){

		var price = $("#price").val();

		$.dialog({
			title: "购买备案券",
			content: "<div style='font-size:14px;padding:5px 0'>价格<strong style='color:#f30'>" + price + "元</strong>，可备案3个网站；</div><div style='font-size:14px;padding:5px 0'>凭备案券前往 <a href='/icp.niaoyun.com' target='_blank' style='color:#0a8dca'>/icp.niaoyun.com/</a> 进行备案，确定后立即扣款购买，是否确定？</div>",
			icon: 'confirm',
			ok: function(){

				core.ajaxload({
					tips:'正在发送命令，请稍后……',
					type:'POST',
					data:{
						'a':'create',
						'rnd':Math.random
					},
					url:'/user/icpcode/'
				});

			},
			cancel: true
		})



	});

});