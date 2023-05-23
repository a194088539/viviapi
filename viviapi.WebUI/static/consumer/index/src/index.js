define(function (require, exports, module) {
	var subModules = {
			indexLoginInit: require('./indexLoginInit'),
			indexBanner: require('./indexBanner'),
			indexAd: require('./indexAd'),
			indexCooMerchant: require('./indexCooMerchant'),
			indexCooOrg : require('./indexCooOrg'),
			sendoptmessage:require('./sendoptmessage')
	};
	exports.init = function(){
		for(var module in subModules) {
			try{
				subModules[module].init();
			} catch(e) {
				window.console && console.log("userindex 子模块初始化有误：" + module);
			}
		}
	};

})