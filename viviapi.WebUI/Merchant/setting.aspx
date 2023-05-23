<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="setting.aspx.cs" Inherits="viviapi.WebUI.Merchant.setting2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            代发选项&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width:30%">
                    接口代发是否需要确认:
                </td>
                <td align="left" class="line_01" style="width:70%">
                    <asp:RadioButtonList ID="rbl_set" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">需要确认</asp:ListItem>
                        <asp:ListItem Value="0">不需要确认</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
             <tr>
                <td height="39" align="left" class="line_01">
                   
                </td>
                <td align="left" class="line_01">
                   <asp:Button ID="btnupdate" runat="server" Text="保存" CssClass="btn btn-primary" 
                        onclick="btnupdate_Click"/>
                </td>
            </tr>
          
        </table>
        
    </div>
</asp:Content>
