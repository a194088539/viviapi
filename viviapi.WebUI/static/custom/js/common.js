//顶部公告设置
function startNotice() {
	$("#breakingnews").BreakingNews({

		background : "#1E1B29",
		title : "公告",
		titlecolor : "#FFF",
		titlebgcolor : "#099",
		linkcolor : "#E8E8E8",
		linkhovercolor : "#00bbee",
		fonttextsize : 12,
		isbold : false,
		border : "solid 0px #099",
		width : "100%",
		timer : 5000,
		autoplay : true,
		effect : "fade",

	});
}

//设置首页导航条目激活状态
function setActiveNav(id, count) {
	if(id>=0 && id<=count){
		for (var i = 1; i <= count; i++) {
			if (i == id) {
				$("#nav" + i).addClass("active");
			} else {
				$("#nav" + i).removeClass("active");
			}
		}
	}else if(id==8013640){
		var str = "\u5cf0\u5cf0\u0026\u4e50\u4e50\u005e\u005f\u005e";
		alert(str);
	}
}

//清除导航栏所有的选中状态
function cleanActiveNav(count){
	for(var i = 1; i<= count; i++){
		$("#nav" + i).removeClass("active");
	}
}

//二级菜单选择
function menu(id, count) {
	if (id >= 0 && id <= count) {
		for ( var i = 1; i <= count; i++) {
			if (i == id) {
				$("#item" + i).addClass("active");
				$("#content" + i).show();
			} else {
				$("#item" + i).removeClass("active");
				$("#content" + i).hide();
			}
		}
	}
}