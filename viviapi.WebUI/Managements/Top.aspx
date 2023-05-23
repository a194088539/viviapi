<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.Top" Codebehind="top.aspx.cs" %>

<html xmlns="">
<head>
    <title>�����̨</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/left1a.css" rel="stylesheet" type="text/css" />
    <link href="style/left1b.css" rel="stylesheet" type="text/css" />
    <script src="style/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="style/jquery.messager.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            color: #FF3300;
        }
    </style>
    <script type="text/javascript" src="http://cdn.bootcss.com/jplayer/2.9.2/jplayer/jquery.jplayer.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#jplayer").jPlayer({
                ready: function () {
                    $(this).jPlayer("setMedia", {
                        mp3: "/sound/tixian.mp3"
                    });
                },
                swfPath: "http://cdn.bootcss.com/jplayer/2.9.2/jplayer/jquery.jplayer.swf",
                supplied: "mp3"
            });
        });


        function ajaxSubmit() {
            var sSource = '/Merchant/Ajax/tixiannotice.ashx?t=' + Math.random();
            var postData = '';

            $.ajax({
                type: "post",
                dataType: "json",
                timeout: 10000,
                url: sSource,
                data: postData,
                success: function (json) {
                    if (json.result == 'ok') {
                        if (json.count > 0) {
                            var str = "<a style='color: Red; font-weight: bold;' href='Withdraw/Audits.aspx' target='rightframe'>����" + json.count + "���µ���������δ����!</a>";
                            $('#tixian').html(str);
                            $("#jplayer").jPlayer('play');
                        }
                        else {
                            $('#tixian').html("");
                        }
                    }
                }
            })
        }
        setInterval("ajaxSubmit()", 30000);
    </script>
</head>
<body style="margin-top: 0px;">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td rowspan="2" align="center" valign="middle" style="height: 40px; width: 150px;">
                <a href="index.aspx" target="rightframe">
                    <img src="style/images/logo.gif" alt="��̨����ϵͳ" border="0" /></a></td>
            <td colspan="2" style="padding-right: 10px; margin-top: 0px; line-height: 28px; height: 28px;text-align: right;">
                &nbsp;���ã�<strong><%=username %></strong>����ӭʹ�ú�̨����ϵͳ</td>
        </tr>
        <tr style="background-image:url(style/images/bg_top.gif)">
            <td >
                <div class="toptitle" id="navigation">
                    <a href="#" onclick="parent.left.disp(1);return false;">��������</a>| 
                    <a href="#" onclick="parent.left.disp(2);return false;">��������</a>| 
                    <a href="#" onclick="parent.left.disp(6);return false;">ͳ�Ʒ���</a>| 
                    <a href="#" onclick="parent.left.disp(3);return false;">�ӿڹ���</a>|
                    <a href="#" onclick="parent.left.disp(4);return false;">�̻������</a>| 
                    <a href="#" onclick="parent.left.disp(5);return false;">�������</a>| 
                    <a href="#" onclick="parent.left.disp(7);return false;">��˽����</a>
                </div>
            </td>
            <td align="right">
                <div class="toptitle_r">
                    <div id="jplayer"></div>
                    <span id="tixian"></span>
                    <a href="index.aspx" target="rightframe">ϵͳ���</a>|
                    <a href="ChangePwd.aspx" target="rightframe">�޸�����</a>|
                    <a href="Logout.aspx" onclick="return confirm('��ȷ��Ҫ�˳���')">�˳�</a></div>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="topline">
            </td>
        </tr>
    </table>
</body>
</html>
