(function($){

	$.fn.ajaxsend = function(opts){

		var defaults = {
			time:60,
			restTime:0,
			node:null,
			type:'mobile',
			before:function(){
				return true;
			}
		};


		var options = $.extend(defaults, opts);


		var start = function(){
			if(options.time > 0){
					options.node.attr("disabled", true);
					options.node.val((options.time--) + "秒后重新发送");
					var timer = setTimeout(function(){
						start();
					},1000);
	
			}else{
				options.node.attr("disabled", false);
				options.node.val(options.text);
				options.time = 60;
			}
		};



		this.each(function(){

			var button = $(this);
			
			options.node = button;


			if (options.restTime && options.restTime>0){
				options.time = options.restTime;
				start();
			}else{
				options.node.attr("disabled", false);
			}


			options.node.bind("click", function(){

				options.val = options.node.val();
				options.node.attr("disabled", true);
				options.node.val("请稍后..");

				var data = null;
				if(options.data){
					data = options.data();
				}


				var before = true;

				if (options.before){
					before = options.before();
				}

				var url = '';

				if(typeof(options.url)=="function"){
					url = options.url();
				}else{
					url = options.url;
				}

				if(before){

					$.ajax({
						dataType:'json',
						url:url,
						data:data,
						cache:false,
						success:function(data){
							if (!data.result){
								if (data.second){
									options.time = data.second;
									start();
								}else{
									options.node.attr("disabled", false);
									options.node.val(options.text);
									options.time = 60;
								}
							}else{
								start();
							}
							options.success.call(options.success, data);
						},
						error:options.error
					});
					
				}else{

					options.node.attr("disabled", false);
					options.node.val(options.val);
				}
	
			});







		
		})







	};




})(jQuery);
