<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Business.Top" Codebehind="top.aspx.cs" %>

<html xmlns="">
<head>
    <title>�����̨</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/left1a.css" rel="stylesheet" type="text/css" />
    <link href="style/left1b.css" rel="stylesheet" type="text/css" />
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
                   <%-- <a href="#" onclick="parent.left.disp(1);return false;">��������</a>| 
                    <a href="#" onclick="parent.left.disp(2);return false;">��������</a>| 
                    <a href="#" onclick="parent.left.disp(3);return false;">�ӿڹ���</a>|
                    <a href="#" onclick="parent.left.disp(4);return false;">�̻������</a>| 
                    <a href="#" onclick="parent.left.disp(5);return false;">�������</a>--%>
                </div>
            </td>
            <td align="right">
                <div class="toptitle_r">
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
