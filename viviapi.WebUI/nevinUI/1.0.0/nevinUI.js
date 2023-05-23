/*NEVINUI.JS*/


$(document).ready(function(){
		//tab
	$(".tab").next().children().not(".showOne").hide();
	$(".tab li a").click(function() {
		$(this).parent().parent().children().removeClass("current");
		$(this).parent().addClass("current");
		var tabindex = ($(this).parent().parent().find("li a").index($(this)));
		var nextdiv = $(this).parent().parent().next();
		$(nextdiv.children()).hide();
		$(nextdiv.children().get(tabindex)).show();
	});

	//table
	if ($(".tablelist").length>0){
	$(".tablelist tr:even").addClass("even");
	$(".tablelist tr th:last-child").addClass("last");
	$(".tablelist tr td:last-child").addClass("last");
	$(".tablelist tr").hover(function() {
		$(this).addClass("hover");
	},function(){
		$(this).removeClass("hover");
	});
	} else {

	}
	
	//input hint
	//input hint
	$("input.hint").addClass("grey_1")
		.blur(function(){$(this).addClass("grey_2");});
	$("input.hint").focus(function(){
		if($(this).val()==$(this).attr('oldval'))
		$(this).attr("value","");
		$(this).removeClass("grey_1");
	})

	$("input.hintpass").addClass("grey_1").focus(function(){
		$(this).attr("value","");
		$(this).replaceWith("<input type='password' class='input_p' name='"+$(this).attr('name')+"' id='"+$(this).attr('id')+"'/>")
		})
		.blur(function(){$(this).addClass("grey_2");});
	
	
	
	/*index Common slideshow*/
	var currentIndex = 0;
		var DEMO;
		var currentID = 0;
		var pictureID = 0;
		$("#ifocus_piclist li").eq(0).show();
		autoScroll();
		$("#ifocus_btn ul li").hover(function() {
		    StopScrolll();
		    $("#ifocus_btn ul li").removeClass("selected");
		    $(this).addClass("selected");
		    currentID = $(this).attr("id");
		    pictureID = currentID.substring(currentID.length - 1);
		    $("#ifocus_piclist li").eq(pictureID).fadeIn("slow");
		    $("#ifocus_piclist li").not($("#ifocus_piclist li")[pictureID]).hide();
		
		}, function() {
		    currentID = $(this).attr("id");
		    pictureID = currentID.substring(currentID.length - 1);
		    currentIndex = pictureID;
		    autoScroll();
		});
		function autoScroll() {
			var tempn = $("#ifocus_btn ul li").size();
		    $("#ifocus_btn li:last").removeClass("selected");
		    $("#ifocus_tx li:last").hide();
		    $("#ifocus_btn li").eq(currentIndex).addClass("selected");
		    $("#ifocus_btn li").eq(currentIndex - 1).removeClass("selected");
		    $("#ifocus_tx li").eq(currentIndex).show();
		    $("#ifocus_tx li").eq(currentIndex - 1).hide();
		    $("#ifocus_piclist li").eq(currentIndex).fadeIn("slow");
		    $("#ifocus_piclist li").eq(currentIndex - 1).hide();
		    currentIndex++; currentIndex = currentIndex >= tempn ? 0 : currentIndex;
		    DEMO = setTimeout(autoScroll, 3000);
		}
		function StopScrolll() {
		    clearTimeout(DEMO);
		}
		/*index Common slideshow*/
	
		
})