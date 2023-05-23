define(function(require,exports,module){
	function adInit(){
		var imgUrl = window.cportalPathRoot + '/cportal/recommended_products.jsonp';
		window.cportalPathRoot == "" ? imgUrl = '/static/consumer/index/examples/recommended_products.js' : '';
		$.ajax({
			url: imgUrl,
			dataType:'jsonp',
			jsonp: "callback",
			jsonpCallback:"cportalIndexRecommendProducts",
			type:'get',
			cache: false,
            data: {},
			success: function(json){
				if(json.length > 0){
					var objData = json, _html = '';
					for(var i=0 ;i < objData.length ; i++){
						_html += '<a href="'+objData[i].forwardUrl+'"><li><div class="media-object"><div class="adImgBox"><img src="'+objData[i].iconUrl+'"></div></div><div class="media-body"><h2>'+objData[i].title+'</h2><p>'+objData[i].description+'</p></div></li></a>';
					}
					_html = '<ul>'+_html+'</ul>';
					$("#merchantBody").html(_html).slideDown(800);
				}
			},
			error: function(e){
				window.console && console.log(e.toString())
			}
		});
	}
	exports.init = function(){
		adInit();
	}
})