$(document).ready(function(){
    $('body').pngFix();
	//png透明
		$('.bk').fadeOut(0);//背景消失
		$('#bk1').fadeIn(1000);//当前背景进入
		$('#bk1 .bText').show().animate({left: '0px'},500);//当前文字进入
        $('#bk1 .bPic').animate( { left: '0'}, 400 );//当前前景图案进入
	$('.banner').hover(
	  function () {
		  window.clearInterval(iID);
	  },
	  function (event) {
		if( $(event.target).attr('class') != 'banner' ){
			 iID = window.setInterval(autoBigBanner,7000);
		}
	  }
	);
	
	
	$('.bNav li').click(function(){
//		$('.bNav li').removeClass('now');
//		$(this).addClass('now');
		$('.bNav li').attr('id','');
		$(this).attr('id','now');
		var bNav = $(this).attr('class');
		$('.bk').fadeOut(900);//背景消失
		$('.bText').attr('style','left:-50px;').hide();//文字消失
		$('#'+bNav).fadeIn(1000);//当前背景进入
		$('#'+ bNav + ' .bText').show().animate({left: '0px'},500);//当前文字进入
        $('.bPic').animate( { left: '20px'}, 300 );//前景图案消失
        $('#'+bNav + ' .bPic').animate( { left: '0'}, 400 );//当前前景图案进入
	});
	//鼠标点击导航
			 iID = window.setInterval(autoBigBanner,7000);

function autoBigBanner()
{
	for(i=1;i<6;i++){
		var bNavNow = $('.bNav #now').attr('class');
		var bkI = 'bk'+i;
		if(bkI == bNavNow ){
		  if( i<5){
	        $('.bNav li').attr('id','');
		    i++;
		    $('.bNav li.bk'+i).attr('id','now');
			$('.bk').fadeOut(900);//背景消失
			$('.bText').attr('style','left:-50px;').hide();//文字消失
			$('#bk'+i).fadeIn(1000);//当前背景进入
			$('#bk'+i + ' .bText').show().animate({left: '0px'},500);//当前文字进入
			$('.bPic').animate( { left: '20px'}, 300 );//前景图案消失
			$('#bk'+i + ' .bPic').animate( { left: '0'}, 400 );//当前前景图案进入
		  }
		  else{
	        $('.bNav li').attr('id','');
		    i = 1;
		    $('.bNav li.bk'+i).attr('id','now');
			$('.bk').fadeOut(900);//背景消失
			$('.bText').attr('style','left:-50px;').hide();//文字消失
			$('#bk1').fadeIn(1000);//当前背景进入
			$('#bk1 .bText').show().animate({left: '0px'},500);//当前文字进入
			$('.bPic').animate( { left: '20px'}, 300 );//前景图案消失
			$('#bk1 .bPic').animate( { left: '0'}, 400 );//当前前景图案进入
		  };
		};
	}
}


		$('.pt_b').removeClass('now');
	$('.pt_a').click(function(){
		$(this).addClass('now');
		$('.pt_b').removeClass('now');
		$('#w1,#w2').hide();
		$('#w2').css({'visibility': 'hidden'});
		$('#w1').show().css({'visibility': 'visible'});
	});
	$('.pt_b').click(function(){
		$(this).addClass('now');
		$('.pt_a').removeClass('now');
		$('#w1,#w2').hide();
		$('#w1').css({'visibility': 'hidden'});
		$('#w2').show().css({'visibility': 'visible'});
	});
	
	$('.pro li').click(function(){
		var iNow = $(this).attr('id');
		$('.p1').fadeOut('fast');
		$('.p2').fadeOut('fast');
		$('.p3').fadeOut('fast');
		$('.p4').fadeOut('fast');
		$('.'+iNow).fadeIn('fast');
		$('.'+iNow).hoverIntent(
			function (){
			},
			function(){
				$(this).fadeOut('fast');
		});
	});
	
	$('.prev').click(function(){
	  $('.prev').removeClass('prev2');
	  $('.next').addClass('next2');
	  $('.solShow ul').animate( { left: '0'}, 500 )
	});
	$('.next').click(function(){
	  $('.prev').addClass('prev2');
	  $('.next').removeClass('next2');
	  $('.solShow ul').animate( { left: '-200px'}, 500 )
	});
	
	$('#qiye').hoverIntent(
		function (){
			$('.nav_qiye').addClass('open').slideDown(400);
		},
		function(){
			$('.nav_qiye').removeClass('open').slideUp(200);
		}
	);
	$('#chanpin').hoverIntent(
		function (){
			$('.nav_chanpin').addClass('open').slideDown(400);
		},
		function(){
			$('.nav_chanpin').removeClass('open').slideUp(200);
		}
	);
	$('#anquan').hoverIntent(
		function (){
			$('.nav_anquan').addClass('open').slideDown(400);
		},
		function(){
			$('.nav_anquan').removeClass('open').slideUp(200);
		}
	);
	
	
});

(function($){$.fn.hoverIntent=function(f,g){var cfg={sensitivity:7,interval:100,timeout:0};cfg=$.extend(cfg,g?{over:f,out:g}:f);var cX,cY,pX,pY;var track=function(ev){cX=ev.pageX;cY=ev.pageY;};var compare=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);if((Math.abs(pX-cX)+Math.abs(pY-cY))<cfg.sensitivity){$(ob).unbind("mousemove",track);ob.hoverIntent_s=1;return cfg.over.apply(ob,[ev]);}else{pX=cX;pY=cY;ob.hoverIntent_t=setTimeout(function(){compare(ev,ob);},cfg.interval);}};var delay=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);ob.hoverIntent_s=0;return cfg.out.apply(ob,[ev]);};var handleHover=function(e){var p=(e.type=="mouseover"?e.fromElement:e.toElement)||e.relatedTarget;while(p&&p!=this){try{p=p.parentNode;}catch(e){p=this;}}if(p==this){return false;}var ev=jQuery.extend({},e);var ob=this;if(ob.hoverIntent_t){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);}if(e.type=="mouseover"){pX=ev.pageX;pY=ev.pageY;$(ob).bind("mousemove",track);if(ob.hoverIntent_s!=1){ob.hoverIntent_t=setTimeout(function(){compare(ev,ob);},cfg.interval);}}else{$(ob).unbind("mousemove",track);if(ob.hoverIntent_s==1){ob.hoverIntent_t=setTimeout(function(){delay(ev,ob);},cfg.timeout);}}};return this.mouseover(handleHover).mouseout(handleHover);};})(jQuery);
//导航延迟触发

