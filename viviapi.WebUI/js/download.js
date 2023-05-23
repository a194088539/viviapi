function setNavActive(language) {
    $("."+language).addClass("active");
}
function loadLog(language) {
    $.getScript("log/"+language +".js", function() {

        $("#last-version").text(versionLog.currentVersion);
        $("#sdk-size").text(versionLog.currentSize);
        $("#sdk-link").attr("href",versionLog.currentLink);
        $("#demo-size").text(versionLog.demoSize);
        $("#demo-link").attr("href",versionLog.demoLink);
        $("#all-size").text(versionLog.demoAllSize);
        $("#all-link").attr("href",versionLog.demoAllLink);
        $("#spay-demo-link").attr("href", versionLog.spayDemoLink);
        $("#pay-demo-size").text(versionLog.spayDemoSize);
        $("#pay-demo-size-eclipse").text(versionLog.spayDemoSizeEclipse);
        $("#spay-manual-link").attr("href", versionLog.spayManualLink);
        $("#spay-manual-size").text(versionLog.spayManualSize);
        $("#download-size").text(versionLog.base);
		var eleH = null;
		var eleDiv = null;
		var eleLi = null;

        for(var i in versionLog.log) {
			eleH = $("<h4 style='margin-bottom: 0px;'>"+versionLog.log[i].title+"</h4>");
			eleDiv = $("<div class='bc-updated-version'>")
				.append(eleH);
            
            for(var j in versionLog.log[i].content) {
				eleLi = $("<li style='padding-left:2em;'>"+versionLog.log[i].content[j]+"</li>");
				eleDiv.append(eleLi);
            }
			$("#bee-changelog-info").append(eleDiv);
        }
        //$(".checkbox input").click(function() {
        //    var total = versionLog.base;
        //    $("input[name='download']:checkbox").each(function() {
        //        if($(this).is(":checked")) {
        //            var value = $(this).val();
        //            total += parseFloat(versionLog[value]);
        //        }
        //    });
        //    $("#download-size").text(total.toFixed(2));
        //});

        $(".download-option").click(function() {
            var that = $(this);
            var input = that.parent().children("input");
            input.click();
        });

        $("#download").click(function() {
            var downloadList = download("checkbox");
            location.href="data/getSelectedFile"+language.substring(0,1).toUpperCase()+language.substring(1) +".php?token="+ window.bcAPIToken + "&fileList="+ $.toJSON(downloadList);
        });

    });
}
function download(item) {
    var downloadList = new Array();
    $("."+item+" input:checkbox:checked").each(function(i) {
        downloadList[i] = $(this).val();
    });
    return downloadList;
}


