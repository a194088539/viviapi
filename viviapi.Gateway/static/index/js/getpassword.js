$(function() { 

	

	$("input.submit").click(function(){

		var button = $(this);
		var form = $(this).parents("form");

		if (form.length>0){

			form.ajaxform({
				"button": button
			});
		}
	});





	


});