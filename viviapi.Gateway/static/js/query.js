function stopflag() {
    $("#btnquery").attr("disabled", true);

    var postData = "oid=" + $("#hforderid").val() + "&rnd=" + Math.random();
    
    $.ajax({
        type: "get",
        dataType: "json",
        timeout: 10000,
        url: '/ws/queryorder.ashx',
        data: postData,
        success: function(a) {
        $("#btnquery").removeAttr("disabled");
            if (a.url == "") {
                $.dialog({
                    title: "翁贝",
                    content: a.msg,
                    lock: true,
                    fixed: true,
                    ok: function() {
                        window.location.reload();
                    },
                    icon: 'warning',
                    width: '250px',
                    height: '90px'
                });
            } else {
                window.location.href = a.url;
            }
        },
        error: function(a, b) {
            $("#btnquery").removeAttr("disabled");
            $.dialog({
                title: "翁贝",
                content: '结果获取失败,请稍等重试' + b,
                lock: true,
                fixed: true,
                ok: function() {
                    window.location.reload();
                },
                icon: 'warning',
                width: '250px',
                height: '90px'
            });
        }
    });
}