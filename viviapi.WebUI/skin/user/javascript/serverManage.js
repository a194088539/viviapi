/* JavaScript Document*/
$(function(){
	/*美化下拉*/
	Js_Dropdown(".select");
	/*复选框全选*/
	$("input[type='checkbox'][name='vid']").each(function(){
		$(this).click(function(){
			if($(this).attr("checked") == "checked"){
				$("input[type='checkbox'][name='id']").attr("checked",true).change();
			}else{
				$("input[type='checkbox'][name='id']").attr("checked",false).change();
			}
		});
	});
	JStx_Djtcsbwzkj(".Theeditor", ".Admin_sxk1", -5, -5,60);
})