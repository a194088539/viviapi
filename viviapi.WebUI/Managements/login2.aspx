<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.login2" Codebehind="login2.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
     <%--<link href="style/union.css" type="text/css" rel="stylesheet" />--%>
    <style type="text/css">
    <!--
    body
    {
	    font-size: 12px;
	    
	    font-family: 宋体,fantasy;
	    margin-top: 0px;
	    margin: 0px;
    }
    .STYLE1 {color: #0000FF}
    .STYLE2 {
	    color: #FFFFFF;
	    font-weight: bold;
    }
    -->
    </style>     
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="100%" border="0">
        <tr>
            <td align="center">
                <br />
                <br />
                <br />
                <br />
                <table width="389" height="105" border="0" cellpadding="0" cellspacing="1" style="background-image:url(style/images/bg_top.gif)">
                    <tr>
                        <td align="left" style="height: 16px"><span class="STYLE2" >请输入二级密码：</span></td>
                    </tr>
                    <tr>
                        <td height="63" align="center" bgcolor="#FFFFFF" class="zy2" >          
                            <table width="67%" border="0">
                                <tr>
                                    <td width="32%" height="23" align="right"><span class="STYLE1">二级密码：</span></td>
                                    <td width="68%" align="center">
                                        <input id="txtPsec" type="password" class="inputbox" runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">&nbsp;</td>
                                    <td align="center">
                                        <input id="btnOk" type="button" class="btn1-1" value="确定" runat="server" onserverclick="btnOk_ServerClick"/>
                                        <input id="btnReset" type="button" class="btn2" value="重置" onclick="javascript:document.getElementById('txtPsec').value='';"/>                                        
                                    </td>
                                </tr>             
                            </table>                     
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center; height:20px"><span id="lblMessage" style="color:white; font-weight:bold" runat="server"></span></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
