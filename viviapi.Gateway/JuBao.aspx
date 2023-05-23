<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JuBao.aspx.cs" Inherits="viviapi.gateway.JuBao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<head runat="server">
    <title>违法和不良信息举报中心</title>
    <style type="text/css">
        BODY
        {
            margin: 0px;
            background: url(/images/topbg.gif) repeat-x top center;
        }
        .zwl12
        {
            font-size: 12px;
            color: #000000;
            line-height: 21px;
            text-decoration: none;
        }
        .swsy12
        {
            font-size: 12px;
            color: #000000;
            line-height: 18px;
            padding-top: 6px;
            text-decoration: none;
        }
        .gd12
        {
            font-size: 12px;
            left: 6px;
            color: #000000;
            line-height: 18px;
            text-decoration: none;
        }
        .dbt
        {
            font-weight: bold;
            font-size: 14px;
            color: #000000;
            line-height: 21px;
            text-decoration: none;
        }
        .db12
        {
            font-size: 12px;
            color: #ffffff;
            line-height: 18px;
            text-decoration: none;
        }
        .srk
        {
            border-right: #89919d 1px solid;
            border-top: #89919d 1px solid;
            border-left: #89919d 1px solid;
            width: 120px;
            line-height: 18px;
            border-bottom: #89919d 1px solid;
            height: 15px;
            background-color: #e9e9e9;
        }
        .b2
        {
            background-image: url(/images/c1.gif);
            background-repeat: no-repeat;
            background-position: top;
        }
        .a1
        {
            font-family: "宋体";
            font-size: 18px;
            line-height: 30px;
            font-weight: bold;
            text-decoration: none;
        }
        .a11
        {
            font-family: "宋体";
            font-size: 12px;
            line-height: 18px;
            color: #666666;
            text-decoration: none;
        }
        .a2
        {
            font-family: "宋体";
            font-size: 12px;
            line-height: 18px;
            color: #389545;
            text-decoration: none;
        }
        .a6
        {
            font-family: "宋体";
            font-size: 12px;
            line-height: 18px;
            color: #666666;
            text-decoration: none;
        }
        
        #photocontent
        {
            height: 165px;
            width: 290px;
        }
        #photolists
        {
            display: none;
            height: 30px;
        }
        #focus_photo_title
        {
            text-decoration: none;
            width: 290px;
            text-align: center;
            clear: both;
        }
        .b12, .b12 a
        {
            font-size: 12px;
            line-height: 20px;
            color: #000000;
            text-decoration: none;
        }
        .b12 a:hover
        {
            text-decoration: underline;
        }
        .h12c, .h12c a
        {
            font-size: 12px;
            line-height: 20px;
            color: #E70103;
            text-decoration: none;
            font-weight: bold;
        }
        .h12c a:hover
        {
            text-decoration: underline;
        }
        #focus_photo_title a
        {
            font-size: 12px;
            color: #824100;
            text-decoration: none;
            line-height: 20px;
            font-family: "宋体";
        }
        #focus_photo_nav
        {
            height: 20px;
            line-height: 20px;
        }
        .axx
        {
            padding: 3px 7px;
            border-left: #cccccc 1px solid;
            font-size: 12px;
            line-height: 18px;
            text-decoration: none;
        }
        a.axx:link, a.axx:visited
        {
            text-decoration: none;
            color: #fff;
            font-size: 12px;
            background-color: #666;
        }
        a.axx:active, a.axx:hover
        {
            text-decoration: none;
            color: #fff;
            font-size: 12px;
            background-color: #999;
        }
        .bxx
        {
            padding: 3px 7px;
            border-left: #cccccc 1px solid;
            font-size: 12px;
            line-height: 18px;
            text-decoration: none;
        }
        a.bxx:link, a.bxx:visited
        {
            text-decoration: none;
            color: #fff;
            font-size: 12px;
            background-color: #CE0609;
        }
        a.bxx:active, a.bxx:hover
        {
            text-decoration: none;
            color: #fff;
            font-size: 12px;
            background-color: #CE0609;
        }
        .bt11
        {
            background: url(/images/na_14.gif) no-repeat;
            background-position: left top;
            height: 24px;
            width: 405px;
            text-align: left;
            padding-top: 2px;
            margin: 8px 0;
        }
        
        .ct1
        {
            font-size: 14px;
            color: #FFFFFF;
            text-decoration: none;
            font-weight: bold;
        }
        .ct2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #FFFFFF;
            text-decoration: none;
        }
        .ct3
        {
            font-size: 12px;
            color: #666666;
            text-decoration: none;
        }
        .cc11
        {
            font-family: "宋体";
            font-size: 14px;
            line-height: 20px;
            font-weight: bold;
            color: #CA0002;
            text-decoration: none;
        }
        .cc12
        {
            font-family: "宋体";
            font-size: 14px;
            line-height: 20px;
            color: #824100;
            text-decoration: none;
        }
        .STYLE1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #FFFFFF;
            text-decoration: none;
            font-weight: bold;
        }
        .top
        {
            text-align: center;
        }
        .intro
        {
            width: 890px;
            background: #c00;
            color: #FFC;
            margin: 5px auto;
            border: #F60 1px solid;
            padding: 10px;
            font-size: 12px;
        }
        .intro strong
        {
            font-size: 14px;
            color: #f60;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="top">
        <img src="images/top.gif" alt="违法和不良信息举报中心" style="width: 910px" /></div>
        <div class="intro">
        <strong>通知：</strong>为了鼓励大家积极举报违法与不良信息，营造更加纯净健康的网络环境，我们为举报人准备了丰厚的奖品。如果您的举报信息经核查准确、真实，就能领取我们定期派发的精美礼品。我们会对您填写的所有个人资料严格保密。</div>
        <table width="910" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#ffffff">
        <tbody>
            <tr>
                <td>
                    <img alt="" height="32" src="images/jbrk.gif" width="530" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table align="center" bgcolor="#cccccc" border="0" cellpadding="3" cellspacing="1"
                        width="100%">
                        <tr bgcolor="#ecf9e6">
                            <td colspan="2" height="23" style="padding-right: 8px; padding-left: 20px">
                                请如实填写用户信息<font color="#ff0000">（带*号为必填项，所有资料均会严格保密）</font>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px; width: 30%;">
                                姓名：
                            </td>
                            <td style="padding-left: 8px" width="316">
                                <div align="left">
                                    <input name="txtUserName" type="text" id="txtUserName" runat="server" maxlength="20" style="border: 1px solid #A4ABB1;" tabindex="1" />
                                    <font color="red">*</font>
                                </div>
                            </td>
                        </tr>
                        <tr bgcolor="#ffffff">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px">
                                电子邮件：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <div align="left">
                                    <input name="txtEmail" type="text" id="txtEmail" runat="server" maxlength="100" size="30" style="border: 1px solid #A4ABB1;" tabindex="1" />
                                    <font color="red">*</font></div>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="right" height="30" style="padding-right: 8px; padding-left: 8px">
                                联系电话：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <div align="left">
                                    <input name="txtMoblie" type="text" id="txtMoblie" runat="server" maxlength="20" style="border: 1px solid #A4ABB1;" tabindex="1" />
                                    <font color="red">*</font></div>
                            </td>
                        </tr>
                        <tr bgcolor="#ffffff">
                            <td align="right" height="14" style="padding-right: 8px; padding-left: 8px">
                                &nbsp;信息所在详细网址<br />
                                (url)：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <textarea name="txtUrl" id="txtUrl" runat="server" cols="35" rows="6" style="border: 1px solid #A4ABB1;" tabindex="1"></textarea>
                                <font color="red">*<br />
                                    *多个地址请使用回车分开</font>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px">
                                被举报信息类型：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <div align="left">                                    
                                    <select name="ddlType" id="ddlType" runat="server">
	<option selected="selected" value="0">-请选择-</option>
	<option value="1">淫秽色情</option>
	<option value="2">诈骗</option>
	<option value="3">病毒</option>
	<option value="4">其他违法和不良信息</option>
 
</select>
                                    <font color="red">*</font></div>
                            </td>
                        </tr>
                        <tr bgcolor="#ffffff">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px">
                                被举报详细内容：
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <textarea name="txtReason" id="txtReason" runat="server" cols="35" rows="8" style="border: 1px solid #A4ABB1;" tabindex="2" wrap="virtual"></textarea>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="center" height="32" style="padding-right: 8px; padding-left: 8px" colspan="2">
                                <span id="lblInfo" runat="server" style="color:Red;"></span>
                            </td>
                        </tr>
                        <tr align="middle" bgcolor="#ffffff">
                            <td colspan="2" height="30">
                                <asp:Button ID="btnSub" runat="server" Text="举报" Width="40px" 
                                    onclick="btnSub_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="查询" Width="40px" 
                                    onclick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
     <br />
    <table width="910" border="0" align="center" cellpadding="0" cellspacing="0" style="border-top: 1px solid #ccc">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="27%" align="center">
                            <img src="images/wjlogo1.gif" width="41" height="51" />
                        </td>
                        <td width="48%" align="center">
                            <p>
                                <span class="dbt">违法与不良信息举报中心</span><br />
                                <span class="ct3">净化网络环境 共建和谐网络</span></p>
                        </td>
                        <td width="25%" align="center">
                            <img src="images/wjlogo2.gif" width="42" height="51" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
