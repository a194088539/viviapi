<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="viviapi.gateway.links.payment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css"> 
body{
	font-size:12px;
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
.STYLE1 {color: #2179DD}
.div1{ width: 30%; float: left; border: 1px #eee solid; margin: 2px 2px 0px 0px;}
.txt {border: 1px #eee solid;}
.tdItem{height:25; text-align:right; background-color:#FFFFFF}
</style>
 
<script language="javascript"> 
function Check(form)
{
	if(form.UserId.value=="")
	{
		alert("商户ID不合法！");return false;
	}
	var channel=parseInt(form.ChannelId.value);
	if(channel==0)
	{
		alert("请选择支付类型");
		form.ChannelId.focus();
		return false;
	}
	
	var isBank=false;
	var isCheck=false;
	
	var rCount=0;
	
	for(i=0;i<form.length;i++)
	{
		if(form[i].type=="radio"){rCount++;}
	}
	
	if(rCount==1)
	{
		if(form.ChannelId.checked==true)
		{
			isCheck=true;
			if(form.ChannelId.kaType=="bank")
			{
				isBank=true;
			}
		}
	}
	else if(rCount>1)
	{
		for(i=0;i<form.ChannelId.length;i++)
		{
			if(form.ChannelId[i].checked==true)
			{
				if(form.ChannelId[i].kaType=="bank")
				{
					isBank=true;
				}
				isCheck=true;
				break;
			}
		}
	}
	if(!isCheck){alert("请选择支付方式！");return false;}
	
	if(!isBank)
	{
		if(form.CardId.value=="")
		{
			alert("请填写充值卡卡号");form.CardId.focus();return false;
		}
		if(form.CardPass.value=="")
		{
			alert("请填写充值卡密码");form.CardPass.focus();return false;
		}
	}
	
	var p=/^\d+(\.\d+)?$/;
	if(!p.test(form.FaceValue.value))
	{
		alert("请正确填写充值金额");
		form.FaceValue.focus();
		form.FaceValue.select();
		return false;
	}
	
	if(isBank)
	{
		return confirm("您填写的充值金额是否正确？如果完全正确请点“确定”进行支付，否则点“取消”更改");
	}
	else
	{
		return confirm("您填写的 [卡号],[卡密]及[充值卡面值] 是否完全正确？如果完全正确请点“确定”进行支付，否则点“取消”更改");
	}
}
 
function show(redo){
    var typeId = redo.value;
    if (typeId == "100" || typeId == "101" || typeId == "102") {
		document.getElementById("tr_cardId").style.display="none";
		document.getElementById("tr_cardPass").style.display="none";
		//document.getElementById("lbmoney").innerHTML="*充值金额";
	}
	else{
		document.getElementById("tr_cardId").style.display="block";
		document.getElementById("tr_cardPass").style.display="block";
		//document.getElementById("lbmoney").innerHTML="*充值卡面值";
	}
}
</script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="34" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="34">
                <img src="images/pic_1.gif" width="69" height="60" />
            </td>
            <td width="100%" background="img/pic_3.gif" bgcolor="#2179DD">
                <img src="images/pic_4.gif" width="40" height="40" />
                快速充值
            </td>
            <td width="13" height="34">
                <img src="images/pic_2.gif" width="69" height="60" />
            </td>
        </tr>
    </table>
    <br />
    <table width="864" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#5c9acf" class="mytable">
        <tr>
            <td width="100%" height="88" bgcolor="#FFFFFF">
                <br />
                <table width="737" border="0" align="center" cellpadding="1" cellspacing="1" class="table_main">                    
                    <tr>
                        <td width="173" height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">商户ID：</span>
                        </td>
                        <td width="557" bgcolor="#FFFFFF">
                            <asp:TextBox ID="txtUserId" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">充值类别：</span>
                        </td>
                        <td bgcolor="#FFFFFF">
                            <div style="width: 100%">
                                <asp:Repeater ID="rptChannlType" runat="server" 
                                    onitemdatabound="rptChannlType_ItemDataBound">
                                    <ItemTemplate>
                                         <div class="div1">
                                            <input id="rd_<%#Eval("typeId")%>" name="rdtypeId" type="radio"  value="<%#Eval("typeId")%>" onclick="show(this)" />
                                            <label for="rd_<%#Eval("typeId")%>"><%#Eval("modetypename")%></label>
                                         </div>
                                    </ItemTemplate>
                                </asp:Repeater>                             
                            </div>
                        </td>
                    </tr>
                    <tr id="tr_cardId" style="display: none;">
                        <td class="tdItem">
                            <span class="STYLE1">卡号：</span>
                        </td>
                        <td bgcolor="#FFFFFF">
                            <asp:TextBox ID="txtCardId" runat="server" CssClass="txt" MaxLength="20"></asp:TextBox>&nbsp;
                            <span style="color: red">*充值卡的卡号</span></td>
                    </tr>
                    <tr id="tr_cardPass" style="display: none;">
                        <td class="tdItem">
                            <span class="STYLE1">卡密：</span>
                        </td>
                        <td bgcolor="#FFFFFF">
                             <asp:TextBox ID="txtCardPass" runat="server" CssClass="txt" MaxLength="20"></asp:TextBox>&nbsp;
                             <span style="color: red">*充值卡的卡密</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdItem">
                            <span class="STYLE1">支付金额：</span>
                        </td>
                        <td bgcolor="#FFFFFF">
                             <asp:TextBox ID="txtAmt" runat="server" CssClass="txt" MaxLength="5"></asp:TextBox>&nbsp;
                             <span style="color: red">*充值金额</span>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="center" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td height="25" bgcolor="#FFFFFF">
                            <asp:Button ID="btnCmmit" runat="server" Text="下一步" CssClass="btn2" 
                                onclick="btnCmmit_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
