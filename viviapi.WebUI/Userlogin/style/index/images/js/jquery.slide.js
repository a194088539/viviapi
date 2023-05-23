var slide =  {
	options : {
		cur			: 0,
		speed		: 5500,
		duration	: 800,
		box			: "#slide",
		pic			: "#slidePic"
	},

	// ��ʼ��
	init : function () {

		// ����С���
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
		
	// ��ʼ
	start : {		
		// С���
		smallPicShow : function (n) {			
			var $item = $(slide.options.box).find("LI");			
			$item.removeClass("on").eq(n).addClass("on");			
			$(slide.options.pic)
								.attr("title", slideData[n].title)
								.hide()
								.css("background-image","url("+ slideData[n].pic +")")
								.fadeIn(slide.options.duration);								
			
			
			if (slideData[n].link != undefined){ // ������
				$(slide.options.pic).html("<a href="+ slideData[n].link +" target=\"_blank\" title="+ slideData[n].title +"></a>");
			}
			slide.options.cur = n;		
		},
		// ѭ��
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
 $("#txtInput").focus(function() {//�ı����ȡ�����¼�
	var vtxt = $("#txtInput").val(); 
	if(vtxt == this.defaultValue){
	 $(this).val("");
	}; 
});
// ʧȥ����		
 $("#txtInput").blur(function() {//�ı����ȡ�����¼�
	   var vtxt = $("#txtInput").val(); 
	   if(vtxt == ""){
		  $(this).val("�������ֻ�����");
	   }; 
});

setTimeout('plogin()', 1000);
function plogin(){
    document.getElementById('plogin').innerHTML = '<iframe allowTransparency="true" id="plogin" name="plogin" marginwidth="0" marginheight="0" src="/inlogin.aspx" frameborder="0" width="286" scrolling="no" height="296"></iframe>';
}