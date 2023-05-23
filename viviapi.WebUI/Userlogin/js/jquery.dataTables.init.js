$.fn.dataTables = function (init) {
    var oInit={
        "bDestroy":true,
        "sDom": 'rt<"bottom"lip>', // 元素布局
        "bPaginate":true,          // 翻页功能
        "bLengthChange":true,      // 改变每页显示数据数量
        "bFilter": true,           // 过滤功能
        "bSort": true,             // 排序功能
        "bInfo":true,              // 页脚信息
        "bAutoWidth":false,        // 自动宽度
        "bStateSave": false,       // 保存条件等状态在cookie里
        "oLanguage": {
            "sLengthMenu": "每页显示 _MENU_ 行",
            "sZeroRecords": "没有符合条件的记录",
            "sProcessing": '<img src="/images/ajax-loader-snake.gif"/>正在查询...',
            "sInfo": "当前第 _START_ - _END_ 行　共计 _TOTAL_ 行",
            "sInfoEmpty": "",
            "sInfoFiltered": "(从 _MAX_ 条记录中过滤)",
            "sSearch": "搜索：",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "上一页",
                "sNext": "下一页",
                "sLast": "尾页"
            }
        },
        "sPaginationType": "full_numbers",
        "aLengthMenu": [[10, 20, 30, 50], [10, 20, 30, 50]],
        "bProcessing": true,
        "bServerSide": true
    };
    $.extend(true, oInit, init);
    $(this).dataTable(oInit);
};
// 刷新 到第一页
$.fn.refreshData=function() {
    var oTable = $(this).dataTable();
    oTable.fnPageChange("first");
};

// 刷新 到当前页
$.fn.refreshCurrent=function() {
    var oTable = $(this).dataTable();
    oTable.fnPageChange(Number($("a[class=paginate_active]").text())-1);
};

// 控制dataTable列的显示和隐藏
$.fn.columnManager = function(init) {
    var oTable = $(this).dataTable();
    var tableId = $(this).attr("id");
    var showid = init.listTargetID;
    var excludeList = init.excludeList;
    var colList = '<ul>';
    var settings = oTable.fnSettings().aoColumns;
    $(settings).each(function(index, element) {
        if($.inArray(index, excludeList) == -1) {
            if (element.bVisible) {
                colList += '<li><input type="checkbox" id="colum_'+index +'" checked="checked" onclick="fnShowHide(\''+tableId+'\','+index+')"/> <label for="colum_'+ index +'" class="inline-label">' + element.sTitle + '</label></li>';
            } else {
                colList += '<li><input type="checkbox" id="colum_'+index+'" onclick="fnShowHide(\''+tableId+'\','+index+')"/> <label for="colum_'+ index +'" class="inline-label">' + element.sTitle + '</label></li>';
            }
        }
    });
    colList +='</ul>';
    if ($('#'+showid)) {
        $('#'+showid).append(colList);
    }
};

// dataTable列的显示和隐藏切换实现
var fnShowHide = function (tableId, iCol ) {
    var oTable = $('#'+tableId).dataTable();
    var bVis = oTable.fnSettings().aoColumns[iCol].bVisible;
    oTable.fnSetColumnVis( iCol, bVis ? false : true ,false);
};