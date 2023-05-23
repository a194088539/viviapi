/**
 * @author: Rock
 * @
 */
define(function (require, exports, module) {
	var PAF = {};
	/*	图片轮播插件 defaults 配置项
	 *new PAF.Focus(obj)
	 *obj = 
	 *{
	 *	gap: 5000
	 *	box: '#imgDiv',
	 *	imgs:[{imageUrl:'',forwardUrl:'',isNewPage: true}]
	 *}
	 *图片来源，链接地址，是否打开为新的窗口
	 * */
	(function($){
		PAF.Focus = function(options){
			this.opts = $.extend(true,{}, PAF.Focus.defaults, options );
			this.gap = this.opts.gap ;
			this.box = this.opts.box;
			this.imgs = this.opts.imgs;
			this.init();
		};
		PAF.Focus.prototype = {
				init: function(){
					var $this = this ;
					var imgsBox = $this.imgs,totalnum = imgsBox.length;
				    var slideBtnBox = document.createElement("div");
				    slideBtnBox.id = 'SwitchNav';
				    slideBtnBox.className = "SwitchNav";
				    var imgHtmlsBox = '';
				    for(var i = 0;i < totalnum; i++){
				    	var _target = imgsBox[i].isNewPage && (imgsBox[i].isNewPage == true) ? '_blank':'_self';
				    	imgHtmlsBox += '<a href="'+imgsBox[i].forwardUrl+'" target="'+_target+'"><img src="'+imgsBox[i].imageUrl+'" alt="" /></a>';
				    	if(totalnum > 1){
				    		slideBtnBox.innerHTML += '<span class="nocurrent">'+(i+1)+'</span>';
				    	}
				    }
				    $($this.box).html(imgHtmlsBox + slideBtnBox.outerHTML);
				    var index = 0;
				    $($this.box +' a img:eq(0)').css('display','inline');
				    $("#SwitchNav span").addClass("nocurrent").eq(0).addClass("current");
				    var MyTime = setInterval(function () {
				        index++;
				        if (index == totalnum) { index = 0; }
				        $this.showImg(index,$this.box);
				    }, $this.gap);
				    $("#SwitchNav span").hover(function (){
				    	index = $("#SwitchNav span").index(this);
				        $this.showImg(index, $this.box);
				        if (MyTime) {
				            clearInterval(MyTime);
				        }
				    },
				    function () {
				        MyTime = setInterval(function () {
				            index++;
				            if (index == totalnum) { index = 0; }
				            $this.showImg(index,$this.box)
				        }, $this.gap);
				    });
				},
				showImg: function(i,box){
					$(box+' img')
						.parent().siblings().find("img").fadeOut(500);
					$(box+' img')
							.eq(i).stop(true, true).fadeIn(500);
			        $("#SwitchNav span")
			        	.eq(i).addClass("current")
			        		.siblings().removeClass("current");
			    }
				
		};
		PAF.Focus.defaults = {
				gap: 5000,
				box: '#imgDiv',
				imgs:[]
		}
	}(jQuery));
	module.exports = PAF.Focus;
});