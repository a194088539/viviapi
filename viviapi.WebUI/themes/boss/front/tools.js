/*JS USE ONLY FOR THIS SITE*/
//�������
(function($){
$.fn.extend({
        Scroll:function(opt,callback){
                //������ʼ��
                if(!opt) var opt={};
                var _this=this.eq(0).find("ul:first");
                var        lineH=_this.find("li:first").height(), //��ȡ�и�
                        line=opt.line?parseInt(opt.line,10):parseInt(this.height()/lineH,10), //ÿ�ι�����������Ĭ��Ϊһ�������������߶�
                        speed=opt.speed?parseInt(opt.speed,10):500, //���ٶȣ���ֵԽ���ٶ�Խ�������룩
                        timer=opt.timer?parseInt(opt.timer,10):3000; //������ʱ���������룩
                if(line==0) line=1;
                var upHeight=0-line*lineH;
                //��������
                scrollUp=function(){
                        _this.animate({
                                marginTop:upHeight
                        },speed,function(){
                                for(i=1;i<=line;i++){
                                        _this.find("li:first").appendTo(_this);
                                }
                                _this.css({marginTop:0});
                        });
                }
                //����¼���
                _this.hover(function(){
                        clearInterval(timerID);
                },function(){
                        timerID=setInterval("scrollUp()",timer);
                }).mouseout();
        }        
})
})(jQuery);
$(document).ready(function(){
	
	//side service pop
	$("#service_box").hover(
	function(){
		$(this).removeClass("service_box").addClass("service_box_hover");
		$(this).children(".mbody").children("ul").removeClass("hide");},
	function(){
		$(this).removeClass("service_box_hover").addClass("service_box");
		$(this).children(".mbody").children("ul").addClass("hide");}
		);
	//fix index style by adding js
	$(".showModule01 .mbody ul li:last").addClass("last_item");
	
	//tuning product page style
	$(".product_nav .module ul li:odd").addClass("odd");
	$(".product_nav .module ul li:last").addClass("last_item");
	
	//about_commits
	
	$(".about_commits .module .mbody ul li").hover(
		function(){
		$(".about_commits .module .mbody ul li").removeClass("hovered");
		$(".about_commits .module .mbody ul li p").hide();
		$(this).addClass("hovered");
		$(this).stop("true","false").children(".libody").children("dl").children("dd").children("p").show();
		},
		function(){
		$(this).removeClass("hovered");
		$(this).stop("true","false").children(".libody").children("dl").children("dd").children("p").hide();
		}
	);
	
	//agreement
	$("a.agree_show").click(function(){
		$(".textcontent").toggle("fast");
		});
	
	
	//index scroll
	$("#scrollDiv").Scroll({line:1,speed:500,timer:3000});
		
})
