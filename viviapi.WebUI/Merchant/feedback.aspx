<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="feedback.aspx.cs" Inherits="viviapi.WebUI.Merchant.feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            留言反馈&nbsp;<img id="loading" width="0" height="0" src="/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    类型:
                </td>
                <td align="left" class="line_01">
                     &nbsp;<select id="ddltypeid" runat="server"><option value="1">BUG反馈</option>
                        <option value="2">意见建议</option>
                        <option value="3">产品咨询</option>
                        <option value="4">其他</option>
                    </select>
                </td>
                <td height="39" align="left" class="line_01">
                   
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    问题或建议:
                </td>
                <td align="left" class="line_01">                    
                      &nbsp;<input id="txttitle" runat="server" type="text"  class="txt_02" maxlength="50" />
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    具体描述:
                </td>
                <td align="left" class="line_01">
                      &nbsp;<textarea id="txtcontent" runat="server" cols="50" rows="10" ></textarea>                    
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td height="22" align="left" class="font8">
                    <asp:Button ID="b_save" runat="server" Text="保存" CssClass="search_button_01" 
                        onclick="b_save_Click"/>
                        </td>
                &nbsp;
                <td align="right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
