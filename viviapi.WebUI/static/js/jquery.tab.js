


(function($){

	$.fn.tab = function(options) {  



		var options = $.extend({}, $.fn.tab.defaults, options);

		if (options.event=="hover"){
			options.event = 'mouseover'
		}

		if (!options.id){
			options.id = window.location.href;
		}



		return this.each(function(){ 

			var that = $(this);
			var id = that.attr("id");

			//alert(id);

			that.find("a").bind(options.event, function(){

				var a = $(this);
				var a_id = a.attr("id");

				$.cookie(options.id + "_tabs_" + id, a_id);


				that.find("a").each(function(){

					if ($(this).attr("id")!=a_id){

						$(this).removeClass("selected");
						$($(this).attr("id")).hide();

					}else{

						$(this).addClass("selected");
						$($(this).attr("id")).show();

					}

				});

			});


			if(options.cookie){
				var cookie_id = $.cookie(options.id + "_tabs_" + id);
				if (cookie_id){
					$("#" + id).find("[id='" + cookie_id + "']").click();
				}

			}


　　　　});



	};



	$.fn.tab.defaults = {  
	  event: 'click',
	  current: '',
	  cookie: false,
	  id: ''
	};



})(jQuery);