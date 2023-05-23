/*
 * auth by zhanglitao 
 * 2014/09/22
 * */
seajs.use(["jquery/jquery/1.10.2/jquery"], function($){
	initEventFn();
	function initEventFn(){
		headHoverFn();
		navTwoHover();
		fixedFooterFn();
	}
	//header二级导航显示
	function headHoverFn(){
		$(".sub-cell>li").hover(function(e){
			$(this).find(".spriteUpDown").addClass("spriteDown");
			$(this).children('ul').stop(true,true).slideDown(200).parent().css("background","#FBFBFB");
		},function(e){
			$(this).find(".spriteUpDown").removeClass("spriteDown");
			$(this).children('ul').stop(true,true).slideUp(200).parent().css("background","none");
		})
	}
	//修复页面footer因为doc文档高度低于浏览器可视高度时，底部留空白的问题(暂时只处理以门户改版后的前端结构页面)
	function fixedFooterFn(){
		if($("div.paf-container").length > 0){
			if( jQuery.support.leadingWhitespace && $(document).height() === $(window).height() ){
				var mT = parseInt( $("div.paf-container").css("marginTop") ),
				mB = parseInt( $("div.paf-container").css("marginBottom") );
				var h = parseInt($(window).height()) - 240 - mT - mB ;
				$("div.paf-container").css("minHeight",h + "px");
			}else if(!jQuery.support.leadingWhitespace && $(document).height() === ( $(window).height() + 4 ) ){
				var mT = parseInt( $("div.paf-container").css("marginTop") ),
				mB = parseInt( $("div.paf-container").css("marginBottom") );
				var h = parseInt($(window).height()) - 244 - mT - mB ;
				$("div.paf-container").css("minHeight",h + "px");
			}
		}
	}
	function navTwoHover(){
		var $appCenter = $('li#appCenter'), 
			$appCenterDiv = $('#appCenter #appCenterDiv');
		
		$appCenter.hover(function(){
			$appCenterDiv.stop(true,true);
			$appCenterDiv.slideDown();
		},function(){
			$appCenterDiv.stop(true,true);
			$appCenterDiv.slideUp();
		});
	}
});