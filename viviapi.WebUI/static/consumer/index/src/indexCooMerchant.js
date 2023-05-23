define(function(require,exports,module){
	function cooperMerchant(){
		var imgUrl = window.cportalPathRoot + '/cportal/cooperation_merchant.jsonp';
		window.cportalPathRoot == "" ? imgUrl = '/static/consumer/index/examples/cooperation_merchant.js' : '';
		$.ajax({
			url: imgUrl,
			dataType:'jsonp',
			jsonp: "callback",
			jsonpCallback:"cportalIndexMerchant",
			type:'get',
			cache: false,
            data: {},
			success: function(json){
				var dataArr = json,_html = "";
				if(dataArr.length > 0){
					for(var i=0;i<dataArr.length;i++){
						_html += '<li><div class="liBusImgBox"><img src="'+dataArr[i].iconUrl+'"></div></li>'
					}
					$("#consumer-business-box .coopBusiness ul").html(_html);
				}
			},
			error: function(e){
				window.console && console.log(e.toString())
			}
		});
	}
	exports.init = function(){
		cooperMerchant()
	}
})