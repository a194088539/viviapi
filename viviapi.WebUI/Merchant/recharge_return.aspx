<%@ Page Title="" Language="C#" MasterPageFile="~/merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="recharge_return.aspx.cs" Inherits="viviapi.WebUI.LongBao.merchant.recharge_return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td height="41" align="left" valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td width="16" height="41" align="left" valign="top">
                                </td>
                                <td width="904" height="41" align="left" valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td width="35" height="41" align="left" background="images/houtai_36.jpg" valign="top">
                                                    &nbsp;
                                                </td>
                                                <td width="840" height="41" align="left" background="images/houtai_38.jpg" valign="top">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td height="15" align="left" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="26" align="left" class="zi17" valign="top">
                                                                   <a href="recharge.aspx"><span class="zi23">账户充值</span></a>&nbsp; 
                                                                    | &nbsp;<a href="rechargelist.aspx"><span class="zi17">充值记录</span></a>&nbsp;  
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td width="29" height="41" align="left" background="images/houtai_39.jpg" valign="top">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td width="16" height="41" align="left" valign="top">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="17" align="left" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td height="119" align="left" valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td width="52" height="119" align="left" valign="top">
                                                &nbsp;
                                            </td>
                                            <td width="830" height="119" align="center" class="biankuang1" valign="middle" bgcolor="#edfed1">
                                                <table width="75%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="2%" valign="top">
                                                            <img src="/images/icon.png" width="46" height="41" />
                                                        </td>
                                                        <td width="98%" align="left" valign="top">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 8px;">
                                                                <tr>
                                                                    <td style="font-family: '微软雅黑'; font-size: 17px;">
                                                                        恭喜您，充值成功！<span style="color: #85b919;">充值金额<em style="font-size: 22px;"><%=successAmt%></em>元</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="color: #6b6b6b;">
                                                                        请稍后几秒钟系统将自动为您跳转到商户用心页面，如长时间无响应请点击下面的返回商户中心按钮返回！
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <a href="/recharge.aspx">继续充值</a> <a href="/shou_transfer.aspx">转账功能</a> <a href="/shanghuzhongxin.aspx">返回商户中心</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="52" height="119" align="left" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="14" align="left" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
