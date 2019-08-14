<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="tables.aspx.cs" Inherits="NewSupersive.Admin.tables" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>议题管理</title>
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap-table.min.js"></script>
    <script src="../Scripts/bootstrap-table-zh-CN.min.js"></script>
    <script src="../Scripts/layui/layui.js"></script>
    <link href="../Scripts/layui/css/layui.css" rel="stylesheet" />
    <link href="../Content/bootstrap-table.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />


		<link rel="stylesheet" href="../assets/css/font-awesome.css" />

		<!-- page specific plugin styles -->
		<link rel="stylesheet" href="../assets/css/jquery-ui.css" />
		<link rel="stylesheet" href="../assets/css/ui.jqgrid.css" />

		<!-- text fonts -->
		<link rel="stylesheet" href="../assets/css/ace-fonts.css" />

		<!-- ace styles -->
		<link rel="stylesheet" href="../assets/css/ace.css" class="ace-main-stylesheet" />

		<!--[if lte IE 9]>
			<link rel="stylesheet" href="../assets/css/ace-part2.css" class="ace-main-stylesheet" />
		<![endif]-->

		<!--[if lte IE 9]>
		  <link rel="stylesheet" href="../assets/css/ace-ie.css" />
		<![endif]-->

		<!-- inline styles related to this page -->

		<!-- ace settings handler -->
		<script src="../assets/js/ace-extra.js"></script>

    <style>
      #UpdateReplyMemo,#UpdateMemo,#UploadFile{
          resize: none;
      }
         .gridView th{
          background-color:#f0f0f0;
          text-align:center;
        }
        #Search,#ModifyRID,#UserPower,#UserDept ,
        #IfAddFinishDate,#IfAddDepartment,
        #ModifyAssignID,#SetDeptCharge,
        #UpdateAssignID,#ModiyRID,#ModifyMxID,#ModifyMxID2,#SetDeptCharge2,#ModifySuperiseAssignID{
            display:none;
        }
       .layui-form-label {
    float: left;
    display: block;
    padding: 9px 15px;
    width: 87px;
    font-weight: 400;
    line-height: 20px;
    text-align: right;
}
       #AddTypeList, #AddUrgencyList,#ModifyUrgencyList,#ModifyTypeList,#UpdateBSate{
            width:428px;
            height:40px;
            
        }
      #DeptSearch,#UrgencySerach,#SuperviseTypeSearch,#BSateSearch{
          width:175px;
            height:40px;
      }
        #SetDepartment {
            width:360px;
        }
        #Select {
            height:38px;
        }
        .layui-input-block {
    margin-left: 87px;
    min-height: 36px;
}

        .mlength
  {
    height:200px;
    overflow: hidden;
	word-wrap:break-word;
	white-space:pre-wrap;
	text-overflow: ellipsis;
	display: -webkit-box;
	-webkit-box-orient: vertical;
	-webkit-line-clamp: 3;
  }
        /*.layui-layer-content{
            display:none;
        }*/
    </style>
      <script>
        function SetStyle() {
        layui.use('layer', function(){ //独立版的layer无需执行这一句
            var $ = layui.jquery, layer = layui.layer; //独立版的layer无需执行这一句
                //示范一个公告层
                layer.open({
                    type: 1
                    , offset: ['100px', '500px']
                  , title: "提示" //不显示标题栏
                  , closeBtn: false
                  , area: '300px;'
                  , shade: 0
                  , id: 'LAY_layuipro' //设定一个id，防止重复弹出
                 , btn: '关闭'
                  , btnAlign: 'c'
                  , moveType: 0 //拖拽模式，0或者1
                  , content: '<div style="padding: 50px; line-height: 22px; background-color: #fffff; color: #000000; font-weight: 300;">请填写完整信息再提交^_^</div>'
                  ,yes: function(index, layero){
                      layer.close(index);//关闭弹框
                      return
                  }
                });
        })
        }


        </script>
</head>
<body style="background-color:white;">
    <form id="form1" runat="server">
        <asp:TextBox ID="UserPower" runat="server"></asp:TextBox>
         <asp:TextBox ID="UserDept" runat="server"></asp:TextBox>
        <div class="layui-form-item" style="margin-left:104px;">
            <div class="layui-inline">
                <label class="layui-form-label">议题</label>
                <div class="layui-input-block">
                   <asp:TextBox type="text" id="SuperviseSearch"  cssClass="Search"   lay-verify="title" autocomplete="off" class="layui-input" runat="server"/>
                </div>
             </div>
            <div class="layui-inline">
                <label class="layui-form-label">任务</label>
                <div class="layui-input-block">
                   <asp:TextBox type="text" id="SuperviseMxSearch"  CssClass="Search" lay-verify="title" autocomplete="off" class="layui-input" runat="server"/>
                </div>
             </div>
            <div class="layui-inline">
                <label class="layui-form-label">部门</label>
                <div class="layui-input-block">
                  <asp:DropDownList ID="DeptSearch" runat="server"  CssClass="Search">
                       <asp:ListItem Value="999" Text="全部"></asp:ListItem>
                  </asp:DropDownList>
                </div>
             </div>
        </div>
         <div class="layui-form-item" style="margin-left:104px;">
             <div class="layui-inline">
                <label class="layui-form-label">紧急程度</label>
                <div class="layui-input-block">
                   <asp:DropDownList ID="UrgencySerach" runat="server"  CssClass="Search">
                        <asp:ListItem Value="999" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="0" Text="特急"></asp:ListItem>
                        <asp:ListItem Value="1" Text="急"></asp:ListItem>
                        <asp:ListItem Value="2" Text="平"></asp:ListItem>
                    </asp:DropDownList>
                </div>
             </div>
            <div class="layui-inline">
                <label class="layui-form-label">类型</label>
                <div class="layui-input-block">
                   <asp:DropDownList ID="SuperviseTypeSearch" runat="server" CssClass="Search">
                       <asp:ListItem Value="999" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="0" Text="事项"></asp:ListItem>
                        <asp:ListItem Value="1" Text="项目"></asp:ListItem>
                    </asp:DropDownList>
                </div>
             </div>
            <div class="layui-inline">
                <label class="layui-form-label">办理状态</label>
                <div class="layui-input-block">
                    <asp:DropDownList ID="BSateSearch" runat="server" CssClass="Search">
                         <asp:ListItem Value="999" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="0" Text="跟进中"></asp:ListItem>
                        <asp:ListItem Value="1" Text="需协调"></asp:ListItem>
                        <asp:ListItem Value="2" Text="已完成"></asp:ListItem>
                    </asp:DropDownList>
                </div>
             </div>
        </div>
        <div class="layui-form-item">
            <div class="layui-inline">
                <asp:Button type="button" class="btn btn-primary" Text="搜索" id="Search" runat="server" OnClick="SuperviseSearch_Click"></asp:Button>
            </div>
            <div class="layui-inline">
                <button type="button" class="layui-btn layui-btn-normal layui-btn-radius"  onclick="OpenSuperviseModal()" id="AddSupersive">添加议题</button>
            </div>
             <div class="layui-inline">
                  <asp:Button type="button" class="btn btn-primary" Text="导出Excel" runat="server" OnClick="OutAsExcel"></asp:Button>
            </div>-
        </div>
         <%--列表--%>    
        <div>
    <table class="layui-table">   
        <tr>
            <td>
                <asp:GridView ID="gridView"  CssClass="gridView" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" AllowSorting="True" OnRowDataBound="gridView_RowDataBound" OnPageIndexChanging="gridView_PageIndexChanging"
                    CellPadding="5" BorderWidth="1px" PageSize="5"   OnRowCommand="GridView_RowCommand" >
                    <Columns>
                        <asp:BoundField HeaderText="序号" ></asp:BoundField> 
                        <asp:BoundField DataField="BigTitle" HeaderText="议题" ItemStyle-Width="100"/>
                        <asp:TemplateField HeaderText="议题操作" ShowHeader="False" ItemStyle-Width="100">
                            <ItemTemplate>
                                <asp:LinkButton  ID="ModifyBigTitle" CommandName="openModifyBigTitleModal"  CommandArgument='<%#Eval("RID")+","+Eval("BigTitle")+","+Eval("MxID")%>' Text="编辑议题" runat="server"></asp:LinkButton>
                                <hr />
                                <asp:LinkButton  ID="DeleteBigTitle" CommandName="DeleteBigTitle_Click"  CommandArgument='<%#Eval("RID")%>' Text="删除议题" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:BoundField DataField="RID" HeaderText="议题ID"  ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="AssignID" HeaderText="分派ID" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Urgency" HeaderText="紧急程度" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="SuperviseType" HeaderText="类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80"/>
                                <asp:TemplateField HeaderText="序号" InsertVisible="False" ItemStyle-HorizontalAlign="Center"> 
                                    <ItemTemplate> <%#Container.DataItemIndex+1%></ItemTemplate> 
                                </asp:TemplateField> 
                                <asp:BoundField DataField="SmallTitle" HeaderText="跟进步骤" ItemStyle-HorizontalAlign="Center"/>
                                <asp:TemplateField HeaderText="步骤操作" ShowHeader="False" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                                    <ItemTemplate>
                                         <asp:LinkButton  ID="Modify" CommandName="openModifySuperviseMxModal"  CommandArgument='<%#Eval("AssignID")+","+Eval("RID")+","+Eval("BigTitle")%>' Text="添加步骤" runat="server"></asp:LinkButton>
                                         <hr />
                                         <asp:LinkButton  CommandName="openModifySmallTitleModal"  CommandArgument='<%#Eval("MxID")+","+Eval("BigTitle")+","+Eval("SmallTitle")+","+Eval("RID")+","+Eval("AssignID")%>' Text="编辑步骤" runat="server"></asp:LinkButton>
                                         <hr />
                                        <asp:LinkButton  CommandName="DeleteSmallTitle_Click"  CommandArgument='<%#Eval("MxID")+","+Eval("AssignID")%>' Text="删除步骤" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FinishDate" HeaderText="完成时限" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="ReplyMemo" HeaderText="跟进情况" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="350" ItemStyle-CssClass="mlength"/>
                                <asp:TemplateField HeaderText="处理操作" ShowHeader="False" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:LinkButton  ID="DealSupervise" CommandName="openDealSuperviseMxModal"  CommandArgument='<%#Eval("AssignID")+","+Eval("BigTitle")+","+Eval("SmallTitle")+","+Eval("ReplyMemo")+","+Eval("Memo")%>' Text="更新跟进情况" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="bing" HeaderText="跟进科部" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80"/>
                                 <asp:BoundField DataField="AssignNo" HeaderText="跟进人员" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Memo" HeaderText="存在问题及推进思路" ItemStyle-HorizontalAlign="Center"/>
                                 <asp:BoundField DataField="bSate" HeaderText="办理状态" ItemStyle-HorizontalAlign="Center"/>
                                 <asp:BoundField DataField="MxID" HeaderText="任务ID" ItemStyle-HorizontalAlign="Center"/>
                                 <asp:TemplateField HeaderText="附件" ShowHeader="False" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:LinkButton  ID="SearchFiles" CommandName="openSearchFiles"  CommandArgument='<%#Eval("AssignID")%>' Text="查看附件" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:GridView>
                        <asp:Label ID="Label1" runat="server" Visible="False" ForeColor="Red">没有数据！！</asp:Label></td>
                </tr>
            </table> 
    </div>
        <%--添加议题的弹窗--%>
        <div class="modal fade" id="AddSuperviseModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">添加议题</h4>
                    </div>
                    <div class="modal-body">
                        <div class="layui-form-item">
                            <label class="layui-form-label">议题类型</label>
                            <div class="layui-input-block">
                              <asp:DropDownList ID="AddTypeList" runat="server">
                                  <asp:ListItem Value="0" Text="事项"></asp:ListItem>
                                  <asp:ListItem Value="1" Text="项目"></asp:ListItem>
                              </asp:DropDownList>
                            </div>
                        </div> 
                        <div class="layui-form-item">
                            <label class="layui-form-label">议题名称</label>
                            <div class="layui-input-block">
                                <asp:TextBox type="text" id="AddBigTitle"   name="AddBigTitle" lay-verify="required"  autocomplete="off" placeholder="请输入议题名称，必填" class="layui-input" runat="server"/>
                            </div>
                        </div>
                        <div class="layui-form-item">
                            <label class="layui-form-label">紧急程度</label>
                            <div class="layui-input-block">
                                <asp:DropDownList ID="AddUrgencyList" runat="server">
                                      <asp:ListItem Value="0" Text="特急"></asp:ListItem>
                                      <asp:ListItem Value="1" Text="急"></asp:ListItem>
                                      <asp:ListItem Value="2" Text="平"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>  
                         <div class="layui-form-item">
                            <label class="layui-form-label">分派部门</label>
                            <div class="layui-input-block">
                                <span class="span1" style="float: left;display: inline-block;">
                                    <asp:TextBox type="text" name="SetDepartment" id="SetDepartment"   autocomplete="off" class="layui-input" runat="server" readonly="true"  />
                                    <asp:TextBox type="text" name="SetDeptCharge" id="SetDeptCharge"   autocomplete="off" class="layui-input" runat="server" readonly="true"/>
                                </span>
                                <span class="span2" style="float: left;display: inline-block;">
                                     <Button  type="button" class="btn btn-primary"  OnClick="SetDepartment_Click()" id="Select">选择</Button>
                                </span>
                            </div>
                        </div>
                         <div class="layui-form-item" >
                            <label class="layui-form-label">跟进步骤</label>
                            <div class="layui-input-block">
                                <asp:TextBox type="text" id="AddSmallTitle"   name="AddTitle" lay-verify="required" autocomplete="off" placeholder="请输入任务内容，可不填" class="layui-input" runat="server"/>
                            </div>
                        </div> 
                         <div class="layui-form-item"  id="IfAddFinishDate">
                            <label class="layui-form-label">完成时限</label>
                            <div class="layui-input-block">
                                <asp:TextBox type="text" name="AddFinishDate" id="AddFinishDate" lay-verify="AddFinishDate" placeholder="yyyy-MM-dd"  autocomplete="off" class="layui-input" runat="server"/>
                            </div>
                        </div> 
                    </div>
                    <div class="modal-footer">
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="关闭" runat="server"></asp:Button>
                        <asp:Button type="button" class="btn btn-primary" Text="确认" runat="server" OnClick="AddSupervise_Click"></asp:Button>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal -->
        </div>
         <%--修改议题的弹窗--%>
        <div class="modal fade" id="ModifyBigTitleModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">修改议题</h4>
                    </div>
                    <div class="modal-body">
                        <div class="layui-form-item">
                            <label class="layui-form-label">议题类型</label>
                            <div class="layui-input-block">
                              <asp:DropDownList ID="ModifyTypeList" runat="server">
                                  <asp:ListItem Value="0" Text="事项"></asp:ListItem>
                                  <asp:ListItem Value="1" Text="项目"></asp:ListItem>
                              </asp:DropDownList>
                            </div>
                        </div> 
                        <div class="layui-form-item">
                            <label class="layui-form-label">议题名称</label>
                            <div class="layui-input-block">
                                 <asp:TextBox type="text" id="ModiyRID"   lay-verify="required"  autocomplete="off" class="layui-input" runat="server"/>
                                  <asp:TextBox type="text" id="ModifyMxID2"   lay-verify="required" lay-reqtext="议题不能为空" autocomplete="off" class="layui-input" runat="server"/>
                                <asp:TextBox type="text" id="ModifyBigTitle2"   name="ModifyBigTitle2" lay-verify="required" lay-reqtext="议题不能为空" autocomplete="off" placeholder="请输入议题名称，必填" class="layui-input" runat="server"/>
                            </div>
                        </div>
                        <div class="layui-form-item">
                            <label class="layui-form-label">紧急程度</label>
                            <div class="layui-input-block">
                                <asp:DropDownList ID="ModifyUrgencyList" runat="server">
                                      <asp:ListItem Value="0" Text="特急"></asp:ListItem>
                                      <asp:ListItem Value="1" Text="急"></asp:ListItem>
                                      <asp:ListItem Value="2" Text="平"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>  
                         <div class="layui-form-item">
                            <label class="layui-form-label">分派部门</label>
                            <div class="layui-input-block">
                                <span class="span1" style="float: left;display: inline-block;">
                                    <asp:TextBox type="text" name="SetDepartment2" id="SetDepartment2"   autocomplete="off" class="layui-input" runat="server" readonly="true"  />
                                    <asp:TextBox type="text" name="SetDeptCharge2" id="SetDeptCharge2"   autocomplete="off" class="layui-input" runat="server"  readonly="true"/>
                                </span>
                                <span class="span2" style="float: left;display: inline-block;">
                                     <Button  type="button" class="btn btn-primary"  OnClick="SetDepartment_Click()" id="Select2">选择</Button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="关闭" runat="server"></asp:Button>
                        <asp:Button type="button" class="btn btn-primary" Text="保存" runat="server" OnClick="SaveBigTitle_Click"></asp:Button>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal -->
        </div>
         <%--部门列表弹窗--%>
        <div class="modal fade" id="SetDepartmentModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">部门列表</h4>
                    </div>
                    <div class="modal-body">
                         <table class="layui-table">                
                <tr>
                    <td>
                      <%-- 行单击事件 OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound"--%>
                                    <asp:GridView ID="gridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="True" AllowSorting="True"  
                            CellPadding="5" BorderWidth="1px" PageSize="15"   
                            DataKeyNames="DeptID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                          <asp:CheckBox ID="CheckBox1" runat="server"/>
                                    </ItemTemplate>
                                     <HeaderTemplate>
                                       <input id="checkedAllBox" type="checkbox" /> 
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DeptID" HeaderText="编号" SortExpression="OID" />
                                <asp:BoundField DataField="DepartmentName" HeaderText="部门"/>
                                <asp:BoundField DataField="UserID" HeaderText="主管"/>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:GridView>
                        <asp:Label ID="Label2" runat="server" Visible="False" ForeColor="Red">没有数据！！</asp:Label>           
                    </td>
                        </tr>
            </table>
                        </div>
                    <div class="modal-footer">
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="关闭" runat="server"></asp:Button>
                        <Button type="button" class="btn btn-primary"  OnClick="SureSetDepartment_Click()">确认</Button>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal -->
       </div>
        
         <%--添加任务的弹窗--%>
        <div class="modal fade" id="ModifySuperviseMxModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel2">添加任务</h4>
                    </div>
                        <div class="modal-body">
                             <div class="layui-form-item">
                            <label class="layui-form-label">议题</label>
                            <div class="layui-input-block">
                                <asp:TextBox type="text" id="ModifyBigTitle"  disabled="disabled"  name="ModifyBigTitle" lay-verify="required"  autocomplete="off" class="layui-input" runat="server"/>
                            </div>
                        </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">任务内容</label>
                                <div class="layui-input-block">
                                      <asp:TextBox type="text" id="ModifyRID"  name="ModifyRID" lay-verify="required" class="layui-input" runat="server" />
                                     <asp:TextBox type="text" id="ModifyAssignID"  name="ModifyAssignID" lay-verify="required" class="layui-input" runat="server" />
                                    <asp:TextBox type="text" id="ModifyTitle"   name="ModifyTitle" lay-verify="required" lay-reqtext="类别名称不能为空" autocomplete="off" placeholder="请输入类别名称" class="layui-input" runat="server"/>
                                </div>
                            </div>
                             <div class="layui-form-item">
                                <label class="layui-form-label">完成时限</label>
                                <div class="layui-input-block">
                                  <asp:TextBox type="text" name="ModifyFinishDate" id="ModifyFinishDate" lay-verify="ModifyFinishDate"  autocomplete="off" class="layui-input" runat="server"/>
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">分派员工</label>
                                <div class="layui-input-block">
                                      <asp:TextBox type="text" name="ModifySetStaff" id="ModifySetStaff"  autocomplete="off" class="layui-input" runat="server" readonly="true" />
                                </div>
                                <Button  type="button" class="btn btn-primary" OnClick="openSetStaffModal()">选择</Button>
                             </div>
                        </div>
                    <div class="modal-footer">
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="关闭" runat="server"></asp:Button>
                        <asp:Button type="button" class="btn btn-primary" Text="保存" runat="server" OnClick="SaveSuperviseAssign_Click"></asp:Button>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal -->
        </div>
        <%--修改任务内容的弹窗（更新任务）--%>
        <div class="modal fade" id="ModifySmallTitleModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">编辑任务</h4>
                    </div>
                        <div class="modal-body">
                            <div class="layui-form-item">
                                <label class="layui-form-label">议题</label>
                                <div class="layui-input-block">
                                      <asp:TextBox type="text" id="ModifySuperiseAssignID"  readonly="false" name="ModifyBigTitle" lay-verify="required"  autocomplete="off" class="layui-input" runat="server"/>
                                      <asp:TextBox type="text" id="ModifyRID3"  readonly="false" name="ModifyBigTitle" lay-verify="required"  autocomplete="off" class="layui-input" runat="server"/>
                                      <asp:TextBox type="text" id="ModifyBigTitle3"  readonly="false"  name="ModifyBigTitle" lay-verify="required"  autocomplete="off" class="layui-input" runat="server"/>
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">任务内容</label>
                                <div class="layui-input-block">
                                      <asp:TextBox type="text" id="ModifyMxID"  name="ModifyMxID" lay-verify="required" class="layui-input" runat="server" />
                                    <asp:TextBox type="text" id="ModifySmallTitle"   name="ModifySmallTitle" lay-verify="required" lay-reqtext="类别名称不能为空" autocomplete="off" placeholder="请输入类别名称" class="layui-input" runat="server"/>
                                </div>
                            </div>
                             <div class="layui-form-item">
                                <label class="layui-form-label">完成时限</label>
                                <div class="layui-input-block">
                                  <asp:TextBox type="text"  id="ModifyFinishDate2" lay-verify="ModifyFinishDate"  autocomplete="off" class="layui-input" runat="server"/>
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">分派员工</label>
                                <div class="layui-input-block">
                                      <asp:TextBox type="text" name="ModifySetStaff2" id="ModifySetStaff2"  autocomplete="off" class="layui-input" runat="server" readonly="false" />
                                </div>
                                <Button  type="button" class="btn btn-primary" OnClick="openSetStaffModal()">选择</Button>
                             </div>
                        </div>
                    <div class="modal-footer">
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="关闭" runat="server"></asp:Button>
                        <asp:Button type="button" class="btn btn-primary" Text="保存" runat="server" OnClick="SaveSmallTtile_Click"></asp:Button>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal -->
        </div>
        <%--员工列表的弹窗--%>
        <div class="modal fade" id="SetStaffModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">员工列表</h4>
                    </div>
                    <div class="modal-body">
                         <table class="layui-table">                
                <tr>
                    <td>
                         <asp:GridView ID="gridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="True" AllowSorting="True"  OnRowCommand="GridView_RowCommand"
                            CellPadding="5" BorderWidth="1px" PageSize="15"   
                            DataKeyNames="UID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                          <asp:CheckBox ID="CheckBox2" runat="server"/>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll2" runat="server"/>
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UID" HeaderText="编号" SortExpression="UID" />
                                 <asp:BoundField DataField="DeptName" HeaderText="部门"/>
                               <asp:BoundField DataField="UserID" HeaderText="员工名称"/>
                                <asp:BoundField DataField="bOFF" HeaderText="有/无效"/>
                                <asp:BoundField DataField="Memo" HeaderText="备注"/>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:GridView>
                        <asp:Label ID="Label3" runat="server" Visible="False" ForeColor="Red">没有数据！！</asp:Label>           
                    </td>
                        </tr>
            </table>
                        </div>
                    <div class="modal-footer">
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="关闭" runat="server"></asp:Button>
                        <Button type="button" class="btn btn-primary"  OnClick="SureSetStaff_Click()" >确认</Button>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal -->
        </div>
        <%--处理任务的弹窗--%>
        <div class="modal fade" id="DealSuperviseMxModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">编辑跟进情况</h4>
                    </div>
                        <div class="modal-body">
                           <div class="layui-form-item">
                                <label class="layui-form-label">所属议题</label>
                                <div class="layui-input-block">
                                      <asp:TextBox type="text" id="UpdateAssignID"   name="AssignID" lay-verify="required" autocomplete="off"  class="layui-input" runat="server"/>
                                    <asp:TextBox type="text" id="UpdateBigTitle"  disabled="disabled"    name="UpdateBigTitle" lay-verify="required" lay-reqtext="议题不能为空" autocomplete="off" placeholder="请输入任务内容" class="layui-input" runat="server"/>
                                </div>
                            </div> 
                             <div class="layui-form-item">
                                <label class="layui-form-label">任务内容</label>
                                <div class="layui-input-block">
                                    <asp:TextBox type="text" id="UpdateSmallTitle"  disabled="disabled"  name="UpdateSmallTitle" lay-verify="required" lay-reqtext="议题不能为空" autocomplete="off" placeholder="请输入任务内容" class="layui-input" runat="server"/>
                                </div>
                            </div> 
                            <div class="layui-form-item">
                                <label class="layui-form-label">完成情况</label>
                                <div class="layui-input-block">
                                   <textarea id="UpdateReplyMemo" name="UpdateReplyMemo" cols="53" style="height: 102px" runat="Server"/>
                                </div>
                            </div> 
                            <div class="layui-form-item">
                                <label class="layui-form-label">存在问题及推进思路</label>
                                <div class="layui-input-block">
                                  <textarea id="UpdateMemo" name="UpdateMemo" cols="53" style="height: 102px" runat="Server"/>
                                </div>
                            </div> 
                            <div class="layui-form-item">
                                <label class="layui-form-label">办理状态</label>
                                    <asp:DropDownList ID="UpdateBSate" runat="server">
                                     <asp:ListItem Value="0" Text="跟进中"></asp:ListItem>
                                     <asp:ListItem Value="1" Text="需协调"></asp:ListItem>
                                     <asp:ListItem Value="2" Text="已完成"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">已上传附件</label>
                                 <textarea id="UploadFile" name="UploadFile" cols="50" runat="Server" style="height: 118px; margin: 0px; width: 424px;" readonly="readonly"/>
                            </div>
                           <%-- 上传附件--%>
                            <div class="layui-upload">
                              <button type="button" class="layui-btn layui-btn-normal" id="testList">选择文件</button> 
                              <div class="layui-upload-list">
                                <table class="layui-table">
                                  <thead>
                                    <tr><th>文件名</th>
                                    <th>大小</th>
                                    <th>状态</th>
                                  </tr></thead>
                                  <tbody id="demoList"></tbody>
                                </table>
                              </div>
                              <button type="button" class="layui-btn" id="testListAction">开始上传</button>
                            </div> 
                        </div>
                    <div class="modal-footer">
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="关闭" runat="server"></asp:Button>
                        <asp:Button type="button" class="btn btn-primary" Text="确认" runat="server" OnClick="SaveDealSuperviseAssign_Click"></asp:Button>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal -->
        </div>
      <%--  附件列表弹窗--%>
        <div class="modal fade" id="FileModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content" style="width:1000px">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">附件列表</h4>
                    </div>
                    <div class="modal-body">
                         <table class="layui-table">   
                    <tr>
                        <td>
                        <asp:GridView ID="FileGridView"  CssClass="gridView" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" AllowSorting="True" OnRowCommand="FileGridView_RowCommand" OnRowDataBound="FileGridView_RowDataBound"
                    CellPadding="5" BorderWidth="1px" PageSize="5" >
                    <Columns>
                        <asp:BoundField HeaderText="序号"  ItemStyle-HorizontalAlign="Center"></asp:BoundField> 
                        <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Width="200"/>
                        <asp:BoundField DataField="Path" HeaderText="文件名" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"/>
                         <asp:BoundField DataField="UserID" HeaderText="上传人" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"/>
                         <asp:BoundField DataField="UploadTime" HeaderText="上传时间" ItemStyle-Width="200" ItemStyle-HorizontalAlign="Center"/>
                        <asp:TemplateField HeaderText="操作" ShowHeader="False" ItemStyle-Width="200"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton  ID="DownLoadFile" CommandName="DownLoadFile_Click"  CommandArgument='<%#Eval("Path")%>' Text="下载附件" runat="server"></asp:LinkButton>

                              <asp:LinkButton  ID="DeleteFile" CommandName="DeleteFile_Click"  CommandArgument='<%#Eval("ID")+","+Eval("UserID")%>' Text="删除附件" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                        </asp:GridView>
                        </td>
                     </tr>
                   </table>
                    </div>
                    <div class="modal-footer">
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="关闭" runat="server"></asp:Button>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal -->
        </div>
    </form>
</body>
</html>

<script>
    layui.use(['form', 'layedit', 'laydate'], function () {
        var form = layui.form
        , layer = layui.layer
        , layedit = layui.layedit
        , laydate = layui.laydate;

        //日期
        laydate.render({
            elem: '#AddFinishDate'
        });
        laydate.render({
            elem: '#ModifyFinishDate2'
        });
        laydate.render({
            elem: '#ModifyFinishDate'
        }); 
    })
layui.use('table', function(){
  var table = layui.table;
});
    //点击添加 弹窗显示
function OpenSuperviseModal() {
    $("#AddBigTitle").val("");
    $("#SetDepartment").val("");
    $("#AddSmallTitle").val("");
    
    $('#AddSuperviseModal').modal({
            show: true,
            backdrop: 'static'
        })
}
    //权限控制 如果不是办公室的主管 就不能添加议题
    if ($("#UserDept").val() != "办公室") {
        $("#AddSupersive").attr("style","display:none")
    }
//监听是否添加小任务
$(function () {
     $('#AddSmallTitle').bind('input propertychange', function() {  
         var input1 = $("#AddSmallTitle").val();
         if (input1 != null || input1 != "") {
             $("#IfAddFinishDate").attr("style", "display:block");
             $("#IfAddDepartment").attr("style", "display:block");
         } if (input1 == null || input1 == "") {
             $("#IfAddFinishDate").attr("style", "display:none");
             $("#IfAddDepartment").attr("style", "display:none");
         }
     })
     $('.Search').bind('input propertychange', function() {  
         __doPostBack('Search');
         return false;
     })
 })
    //打开部门弹窗
 function SetDepartment_Click() {
     $('#SetDepartmentModal').modal({
         show: true,
         backdrop: 'static'
     })
 }
 //选择部门
 function SureSetDepartment_Click() {
     var gridView = document.getElementById("gridView2");
     // 遍历GridView中的行
     var department;
     var str = "";
     var strDept = "";
     for (var i = 1; i < gridView.rows.length; i++) {
         var cb = gridView.rows[i].cells[0].children[0];
         if (cb.checked) {
             department = gridView.rows[i].cells[2].innerText;
             DeptCharge = gridView.rows[i].cells[3].innerText;
             str += department + ",";
             strDept += DeptCharge + ",";
         }
     }
     if ($('#AddSuperviseModal').css('display') == "block") {
         $("#SetDepartment").val(str);
         $("#SetDeptCharge").val(strDept);
         $('#SetDepartmentModal').modal('hide');//隐藏modal

         $('#AddSuperviseModal').modal({
             show: true,
             backdrop: 'static'
         })
     }
     else if ($('#ModifyBigTitleModal').css('display') == "block") {
         $("#SetDepartment2").val(str);
         $("#SetDeptCharge2").val(strDept);
         $('#SetDepartmentModal').modal('hide');//隐藏modal

         $('#ModifyBigTitleModal').modal({
             show: true,
             backdrop: 'static'
         })
     }
 }
//员工列表打开
 function openSetStaffModal() {
     $("#ModifySetStaff2").val("");
     $('#SetStaffModal').modal({
         show: true,
         backdrop: 'static'
     });
 }

 
 //选择员工
 function SureSetStaff_Click() {
     var gridView = document.getElementById("gridView1");
     // 遍历GridView中的行
     var department;
     var str = "";
     for (var i = 0; i < gridView.rows.length; i++) {
         var cb = gridView.rows[i].cells[0].children[0];
         if (cb.checked) {
             department = gridView.rows[i].cells[3].innerText;
             str += department + ",";
         }
     }
     if ($('#ModifySuperviseMxModal').css('display') == "block") {
     $("#ModifySetStaff").val(str);
     $('#SetStaffModal').modal('hide');//隐藏modal

     $('#ModifySuperviseMxModal').modal({
         show: true,
         backdrop: 'static'
     })
     } else if ($('#ModifySmallTitleModal').css('display') == "block") {
         $("#ModifySetStaff2").val(str);
         $('#SetStaffModal').modal('hide');//隐藏modal
         $('#ModifySmallTitleModal').modal({
             show: true,
             backdrop: 'static'
         })
     }
 }
 var $checkedAllBox = $('#checkedAllBox')
 $checkedAllBox.click(function () {
     var gridView = document.getElementById("gridView2");
    
     for (var i = 0; i < gridView.rows.length; i++) {
         if (checkedAllBox.checked == false) {
             $("#gridView2_CheckBox1_" + i + "").prop('checked', false);
         }
         else {
             $("#gridView2_CheckBox1_" + i + "").prop('checked', true);
         }
     }
 })
 var $checkedAllBox2 = $('#gridView1_CheckAll2')
 $checkedAllBox2.click(function () {
     var gridView = document.getElementById("gridView1");
     alret("");
     for (var i = 0; i < gridView.rows.length; i++) {
         if (checkedAllBox2.checked == false) {
             $("#gridView1_CheckBox2_" + i + "").prop('checked', false);
         }
         else {
             $("#gridView1_CheckBox2_" + i + "").prop('checked', true);
         }
     }
 })

 layui.use('upload', function(){
     var upload = layui.upload;
     var id = $("#UpdateAssignID").val();
     var userID='<%=Session["UserID"].ToString()%>';
    var demoListView = $('#demoList')
    , uploadListIns = upload.render({
        elem: '#testList'
     , url: '<%=ResolveUrl("~/Admin/Upload.ashx")%>'
     , accept: 'file'
     , multiple: true
     , auto: false
     , bindAction: '#testListAction'
     ,before: function(obj){
         this.data = { 'ID': id, 'UserID': userID };//关键代码
     } 
     , choose: function (obj) {
         var files = this.files = obj.pushFile(); //将每次选择的文件追加到文件队列
         //读取本地文件
         obj.preview(function (index, file, result) {
             var tr = $(['<tr id="upload-' + index + '">'
               , '<td>' + file.name + '</td>'
               , '<td>' + (file.size / 1014).toFixed(1) + 'kb</td>'
               , '<td>等待上传</td>'
               , '<td>'
                 , '<button class="layui-btn layui-btn-xs demo-reload layui-hide">重传</button>'
                 , '<button class="layui-btn layui-btn-xs layui-btn-danger demo-delete">删除</button>'
               , '</td>'
             , '</tr>'].join(''));

             //单个重传
             tr.find('.demo-reload').on('click', function () {
                 obj.upload(index, file);
             });

             //删除
             tr.find('.demo-delete').on('click', function () {
                 delete files[index]; //删除对应的文件
                 tr.remove();
                 uploadListIns.config.elem.next()[0].value = ''; //清空 input file 值，以免删除后出现同名文件不可选
             });

             demoListView.append(tr);
         });
     }
     , done: function (res, index, upload) {
         if (res.code == 0) { //上传成功
             var tr = demoListView.find('tr#upload-' + index)
             , tds = tr.children();
             tds.eq(2).html('<span style="color: #5FB878;">上传成功</span>');
             tds.eq(3).html(''); //清空操作
             return delete this.files[index]; //删除文件队列已经上传成功的文件
         }
         this.error(index, upload);
     }
     , error: function (index, upload) {
         var tr = demoListView.find('tr#upload-' + index)
           , tds = tr.children();
         tds.eq(2).html('<span style="color: #5FB878;">上传成功</span>');
         tds.eq(3).html(''); //清空操作
         return delete this.files[index]; //删除文件队列已经上传成功的文件
     }
    });
 })
</script>

