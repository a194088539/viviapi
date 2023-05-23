<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JuBao.aspx.cs" Inherits="viviapi.gateway.JuBao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<head runat="server">
    <title>Υ���Ͳ�����Ϣ�ٱ�����</title>
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
            font-family: "����";
            font-size: 18px;
            line-height: 30px;
            font-weight: bold;
            text-decoration: none;
        }
        .a11
        {
            font-family: "����";
            font-size: 12px;
            line-height: 18px;
            color: #666666;
            text-decoration: none;
        }
        .a2
        {
            font-family: "����";
            font-size: 12px;
            line-height: 18px;
            color: #389545;
            text-decoration: none;
        }
        .a6
        {
            font-family: "����";
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
            font-family: "����";
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
            font-family: "����";
            font-size: 14px;
            line-height: 20px;
            font-weight: bold;
            color: #CA0002;
            text-decoration: none;
        }
        .cc12
        {
            font-family: "����";
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
        <img src="images/top.gif" alt="Υ���Ͳ�����Ϣ�ٱ�����" style="width: 910px" /></div>
        <div class="intro">
        <strong>֪ͨ��</strong>Ϊ�˹�����һ����ٱ�Υ���벻����Ϣ��Ӫ����Ӵ������������绷��������Ϊ�ٱ���׼���˷��Ľ�Ʒ��������ľٱ���Ϣ���˲�׼ȷ����ʵ��������ȡ���Ƕ����ɷ��ľ�����Ʒ�����ǻ������д�����и��������ϸ��ܡ�</div>
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
                                ����ʵ��д�û���Ϣ<font color="#ff0000">����*��Ϊ������������Ͼ����ϸ��ܣ�</font>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px; width: 30%;">
                                ������
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
                                �����ʼ���
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <div align="left">
                                    <input name="txtEmail" type="text" id="txtEmail" runat="server" maxlength="100" size="30" style="border: 1px solid #A4ABB1;" tabindex="1" />
                                    <font color="red">*</font></div>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="right" height="30" style="padding-right: 8px; padding-left: 8px">
                                ��ϵ�绰��
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <div align="left">
                                    <input name="txtMoblie" type="text" id="txtMoblie" runat="server" maxlength="20" style="border: 1px solid #A4ABB1;" tabindex="1" />
                                    <font color="red">*</font></div>
                            </td>
                        </tr>
                        <tr bgcolor="#ffffff">
                            <td align="right" height="14" style="padding-right: 8px; padding-left: 8px">
                                &nbsp;��Ϣ������ϸ��ַ<br />
                                (url)��
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <textarea name="txtUrl" id="txtUrl" runat="server" cols="35" rows="6" style="border: 1px solid #A4ABB1;" tabindex="1"></textarea>
                                <font color="red">*<br />
                                    *�����ַ��ʹ�ûس��ֿ�</font>
                            </td>
                        </tr>
                        <tr bgcolor="#f5f5f5">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px">
                                ���ٱ���Ϣ���ͣ�
                            </td>
                            <td style="padding-right: 8px; padding-left: 8px">
                                <div align="left">                                    
                                    <select name="ddlType" id="ddlType" runat="server">
	<option selected="selected" value="0">-��ѡ��-</option>
	<option value="1">����ɫ��</option>
	<option value="2">թƭ</option>
	<option value="3">����</option>
	<option value="4">����Υ���Ͳ�����Ϣ</option>
 
</select>
                                    <font color="red">*</font></div>
                            </td>
                        </tr>
                        <tr bgcolor="#ffffff">
                            <td align="right" height="32" style="padding-right: 8px; padding-left: 8px">
                                ���ٱ���ϸ���ݣ�
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
                                <asp:Button ID="btnSub" runat="server" Text="�ٱ�" Width="40px" 
                                    onclick="btnSub_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="��ѯ" Width="40px" 
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
                                <span class="dbt">Υ���벻����Ϣ�ٱ�����</span><br />
                                <span class="ct3">�������绷�� ������г����</span></p>
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
