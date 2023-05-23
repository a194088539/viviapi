var slide =  {
	options : {
		cur			: 0,
		speed		: 5500,
		duration	: 800,
		box			: "#slide",
		pic			: "#slidePic"
	},

	// 初始化
	init : function () {

		// 创建小点点
		$(this.options.box).prepend('<ul class="slideItem"></ul>');

		if (slideData.length > 0) {
			$.each(slideData, function(i, n){
				var s = slide.options.cur==i ? ' class=\"on\"' : "";
				$(".slideItem").append('<li'+ s +'>'+(i+1)+'</li>');
			});

			var $item = $(slide.options.box).find("LI");		
			$item.each(function(i){
				$(this).click(function(){
					slide.start.smallPicShow(i);
				});
			});
			
			this.start.smallPicShow(this.options.cur);	
			
			$(this.options.box)
								.hover(	function(){clearInterval(slideStart);},
										function(){	slideStart = setInterval("slide.start.loop()", slide.options.speed);});
		
			var slideStart = setInterval("slide.start.loop()", slide.options.speed);			
		}
	},
		
	// 开始
	start : {		
		// 小点点
		smallPicShow : function (n) {			
			var $item = $(slide.options.box).find("LI");			
			$item.removeClass("on").eq(n).addClass("on");			
			$(slide.options.pic)
								.attr("title", slideData[n].title)
								.hide()
								.css("background-image","url("+ slideData[n].pic +")")
								.fadeIn(slide.options.duration);								
			
			
			if (slideData[n].link != undefined){ // 有链接
				$(slide.options.pic).html("<a href="+ slideData[n].link +" target=\"_blank\" title="+ slideData[n].title +"></a>");
			}
			slide.options.cur = n;		
		},
		// 循环
		loop : function () {
			var $item = $(slide.options.box).find("LI");
			slide.options.cur ++;
			if (slide.options.cur > $item.length - 1) slide.options.cur = 0; 
			slide.start.smallPicShow(slide.options.cur);
		}
	}
};

slide.init();

//txtSearch
 $("#txtInput").focus(function() {//文本框获取焦点事件
	var vtxt = $("#txtInput").val(); 
	if(vtxt == this.defaultValue){
	 $(this).val("");
	}; 
});
// 失去焦点		
 $("#txtInput").blur(function() {//文本框获取焦点事件
	   var vtxt = $("#txtInput").val(); 
	   if(vtxt == ""){
		  $(this).val("请输入手机号码");
	   }; 
});

setTimeout('plogin()', 1000);
function plogin(){
    document.getElementById('plogin').innerHTML = '<iframe allowTransparency="true" id="plogin" name="plogin" marginwidth="0" marginheight="0" src="/inlogin.aspx" frameborder="0" width="286" scrolling="no" height="296"></iframe>';
}