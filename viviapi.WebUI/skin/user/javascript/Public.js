	/*JavaScript Document*/
	/*===============================================================================================
	功能：美化下拉
	===============================================================================================*/
	function Js_Dropdown(objs){
		$(objs).each(function(){
			var s=$(this);
			var z=parseInt(s.css("z-index"));
			var dt=$(this).children("dt");
			var dd=$(this).children("dd");
			var Inputv = $(this).children("input")
			var _show=function(){dd.slideDown(200);dt.addClass("cur");s.css("z-index",z+1);};
			var _hide=function(){dd.slideUp(200);dt.removeClass("cur");s.css("z-index",z);};
			s.mouseleave(function(){dd.hide();dt.removeAttr("class");});
			dt.click(function(){dd.is(":hidden")?_show():_hide();});
			var Enable_click = this.getAttribute("click")
			if(Enable_click == null){
				dd.find("a").click(function(){dt.html($(this).html());_hide();Inputv.val(this.getAttribute("value"));});
			}
			$(this).find("ul").eq(0).css({"width":(dt.width()+29)+"px"});
		})	
	}
	
	
	/*===============================================================================================
	功能：点击用户名弹出内容编辑
	===============================================================================================*/
	function JStx_Djtcsbwzkj(objs, Nriqi, BianJutop, BianJuleft,Frame) {
        var xOffset = BianJutop;
        var yOffset = BianJuleft;
        var w;
        var a_Height;
        var d_Height;

        $(objs).each(function () {
            $(this).click(function (e) {
                if (document.getElementById("JStx_Ckpreview")) {
                    $("#JStx_Ckpreview").remove();
                };
                var Duquhtml = null;
                $(this).find(Nriqi).each(function () {
                    Duquhtml = $(this).html();
                });
                w = $(window).width();
                a_Height = $(window).height();
                if (Duquhtml != null) {
                    $("body").append("<div id='JStx_Ckpreview'>" + Duquhtml + "</div>");
                    $("#JStx_Ckpreview").mouseleave(function () {
                        $("#JStx_Ckpreview").remove();
                    });
                    $("#JStx_Ckpreview").css({
                        position: "absolute",
                        zIndex: 1000
                    });
					$("#JStx_Ckpreview").css("top", (e.pageY + xOffset) + "px")
					$("#JStx_Ckpreview").css("left", (e.pageX + yOffset) + "px").css("right", "auto");
                };
            });
        });
    };
	
	
	
	
	
	
	