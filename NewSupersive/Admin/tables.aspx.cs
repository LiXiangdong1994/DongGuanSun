using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace NewSupersive.Admin
{
    public partial class tables : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            Style();
            LoadDeparamentData();
            LoadUserInFoData();

            LoadCodeList();
        }
        protected void LoadCodeList()
        {
            if (DeptSearch.Items.Count == 1)
            {
                ParameterBLL parameterBLL = new ParameterBLL();
                SqlDataReader read = parameterBLL.selectDepartments();
                while (read.Read())
                {
                    string OID = read["OID"].ToString();
                    string CodeName = read["CodeName"].ToString();
                    DeptSearch.Items.Add(new ListItem(CodeName, OID));
                }
            }
        }
        //加载部门列表
        #region
        private void LoadDeparamentData()
        {
            S_UserInFo userInfo = (S_UserInFo)Session["User"];
            ParameterBLL parameterBLL = new ParameterBLL();
            DataSet ds = new DataSet();
            ds = parameterBLL.selectDepartmentsSet();
            DataView dv = ds.Tables[0].DefaultView;
            gridView2.DataSource = dv;
            gridView2.DataBind();
        }
        #endregion

        //加载部门员工
        #region
        private void LoadUserInFoData()
        {
            S_UserInFo userInfo = (S_UserInFo)Session["User"];
            UserBLL staffBLL = new UserBLL();
            DataSet ds = new DataSet();
            ds = staffBLL.selectStaffSet(userInfo.DeptID);
            DataView dv = ds.Tables[0].DefaultView;
            gridView1.DataSource = dv;
            gridView1.DataBind();
        }
        #endregion
        //加载所有任务
        #region
        private void LoadData()
        {
            S_UserInFo user = null;
            try
            {
                user = (S_UserInFo)Session["User"];
                user.ToString();
            }
            catch
            {
                Response.Redirect("Login.aspx");
            }
            List<string> listWhere = new List<string>();
            string superviseSearch = SuperviseSearch.Text;
            string superviseMxSearch = SuperviseMxSearch.Text;
            string deptSearch = DeptSearch.SelectedItem.ToString();
            int urgencySerach = int.Parse(UrgencySerach.SelectedValue);
            int superviseTypeSearch = int.Parse(SuperviseTypeSearch.SelectedValue.ToString());
            int bSateSearch = int.Parse(BSateSearch.SelectedValue.ToString());
            if (superviseSearch.Length != 0)
            {
                listWhere.Add("BigTitle like '" + '%' + superviseSearch + "'");
            }
            if (superviseMxSearch.Length != 0)
            {
                listWhere.Add("SmallTitle like '" + '%' + superviseMxSearch + "'");
            }
            Boolean a = deptSearch.Equals("全部");
            if (a == false)
            {
                listWhere.Add("DeptName= '" + deptSearch + "'");
            }
            if (urgencySerach != 999)
            {
                listWhere.Add("Urgency= '" + urgencySerach + "'");
            }
            if (superviseTypeSearch != 999)
            {
                listWhere.Add("SuperviseType= '" + superviseTypeSearch + "'");
            }
            if (bSateSearch != 999)
            {
                listWhere.Add("bSate= '" + bSateSearch + "'");
            }
            //办公室员工
            if (user.Memo == "办公室" && user.Power != 0)
            {
                listWhere.Add("DeptName= '" + user.Memo + "'");
            }
            //科部主管
            if (user.Memo != "办公室" && user.Power == 0)
            {
                listWhere.Add("DeptName= '" + user.Memo + "'");
            }
            //科部员工         
            if (user.Memo != "办公室" && user.Power != 0)
            {
                listWhere.Add("AssignNo= '" + user.UserID + "'");
            }



            SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
            DataSet ds = new DataSet();
            try
            {
                ds = superviseAssignBLL.FindSuperviseAssignByMore(listWhere);
                DataView dv = ds.Tables[0].DefaultView;
                gridView.DataSource = dv;
                gridView.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('当前无任务!')</script>");
            }

        }
        #endregion
        //合并单元格 合并某一列所有行
        #region 
        /// <summary>
        /// 合并GridView中某列相同信息的行（单元格）
        /// </summary>
        /// <param name="GridView1"></param>
        /// <param name="cellNum"></param>
        public static void GroupCol(GridView GridView1, int cols)
        {
            if (GridView1.Rows.Count < 1 || cols > GridView1.Rows[0].Cells.Count - 1)
            {
                return;
            }
            TableCell oldTc = GridView1.Rows[0].Cells[cols];//列表的一行
            for (int i = 1; i < GridView1.Rows.Count; i++)
            {
                TableCell tc = GridView1.Rows[i].Cells[cols];//列表的下一行
                if (oldTc.Text == tc.Text)
                {
                    tc.Visible = false;//循环遍历 如果行内容相同  就隐藏下一行
                    if (oldTc.RowSpan == 0)
                    {
                        oldTc.RowSpan = 1;
                    }
                    oldTc.RowSpan++;
                    oldTc.VerticalAlign = VerticalAlign.Middle;
                }
                else
                {
                    oldTc = tc;
                }
            }
        }

        #endregion

        //合并单元格 第一列相同，第6列合并
        #region 
        /// <summary>
        /// 合并GridView中第一列相同，第6列合并（单元格）
        /// </summary>
        /// <param name="GridView1"></param>
        /// <param name="cellNum"></param>
        public static void GroupCol2(GridView GridView1)
        {

            TableCell RIDCols = GridView1.Rows[0].Cells[3];//列表的第一行第二列（议题列）
            TableCell OrderCols = GridView1.Rows[0].Cells[0];//列表的第一行第二列（议题列）
            TableCell BigTitleDoCols = GridView1.Rows[0].Cells[2];
            //TableCell SmallTitleDoCols = GridView1.Rows[0].Cells[9];//列表的第一行第二列（议题列）

            for (int i = 1; i < GridView1.Rows.Count; i++)
            {
                TableCell RIDColsNext = GridView1.Rows[i].Cells[3];//列表的下一行
                TableCell BigTitleDoColsNext = GridView1.Rows[i].Cells[2];
                //TableCell SmallTitleDoColsNext = GridView1.Rows[i].Cells[9];//列表的添加小任务列
                TableCell OrderColsNext = GridView1.Rows[i].Cells[0];
                if (RIDCols.Text == RIDColsNext.Text)//如果议题相同
                {
                    //SmallTitleDoColsNext.Visible = false;//循环遍历 如果行内容相同  就隐藏下一行
                    OrderColsNext.Visible = false;
                    BigTitleDoColsNext.Visible = false;
                    if (BigTitleDoCols.RowSpan == 0)
                    {
                        BigTitleDoCols.RowSpan = 1;
                    }
                    BigTitleDoCols.RowSpan++;
                    BigTitleDoCols.VerticalAlign = VerticalAlign.Middle;
                    //if (SmallTitleDoCols.RowSpan == 0)
                    //{
                    //    SmallTitleDoCols.RowSpan = 1;
                    //}
                    //SmallTitleDoCols.RowSpan++;
                    //SmallTitleDoCols.VerticalAlign = VerticalAlign.Middle;
                    if (OrderCols.RowSpan == 0)
                    {
                        OrderCols.RowSpan = 1;
                    }
                    OrderCols.RowSpan++;
                    OrderCols.VerticalAlign = VerticalAlign.Middle;


                }
                else
                {
                    RIDCols = RIDColsNext;
                    BigTitleDoCols = BigTitleDoColsNext;
                    //SmallTitleDoCols = SmallTitleDoColsNext;
                    OrderCols = OrderColsNext;
                }
            }
        }

        #endregion
        //设置合并格式
        protected void Style()
        {
            S_UserInFo user = null;
            try
            {
                user = (S_UserInFo)Session["User"];
                user.ToString();
            }
            catch (Exception ex)
            {
                Response.Redirect("Login.aspx");
            }
            UserDept.Text = user.Memo;
            UserPower.Text = user.Power.ToString();
            LoadData();
            if (gridView.Rows.Count != 0)
            {
                GroupCol(gridView, 1);
                GroupCol2(gridView);
            }
        }
        //改变显示格式
        #region
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            S_UserInFo user = (S_UserInFo)Session["User"];
            //动态序号
            if (e.Row.RowIndex != -1)
            {
                int indexID = this.gridView.PageIndex * this.gridView.PageSize + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = indexID.ToString();
            }
            //隐藏列
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                //办公室 不操作任务
                if (user.Memo == "办公室")
                {
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[12].Visible = false;
                }
                //科部 不操作议题
                if (user.Memo != "办公室")
                {
                    e.Row.Cells[2].Visible = false;
                }
                //科部人员 不操作议题和任务 只处理任务
                if (user.Power != 0 && user.Memo != "办公室")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[9].Visible = false;
                }
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[17].Visible = false;
            }

            for (int i = 0; i < gridView.Rows.Count; i++)
            {

                ////Page.ClientScript.RegisterStartupScript(this.GetType(), "", "SetStyle()", true);
                //gridView.Rows[i].Cells[2].Attributes.Add("onmouseover", "ShowAndHide()");
                //隐藏信息
                for (int j = 0; j < gridView.Rows[i].Cells.Count; j++)
                {
                    string Str = gridView.Rows[i].Cells[j].Text; //第二列内容
                    gridView.Rows[i].Cells[j].ToolTip = Str;//鼠标放上去显示全部信息
                }
                //状态显示信息
                if (gridView.Rows[i].Cells[16].Text == "0")
                {
                    gridView.Rows[i].Cells[16].Text = "跟进中";
                }
                else if (gridView.Rows[i].Cells[16].Text == "1")
                {
                    gridView.Rows[i].Cells[16].Text = "需协调";
                }
                else if (gridView.Rows[i].Cells[16].Text == "2")
                {
                    gridView.Rows[i].Cells[16].Text = "已完成";
                }

                if (gridView.Rows[i].Cells[5].Text == "0")
                {
                    gridView.Rows[i].Cells[5].Text = "特急";
                }
                else if (gridView.Rows[i].Cells[5].Text == "1")
                {
                    gridView.Rows[i].Cells[5].Text = "急";
                }
                else if (gridView.Rows[i].Cells[5].Text == "2")
                {
                    gridView.Rows[i].Cells[5].Text = "平";
                }


                if (gridView.Rows[i].Cells[6].Text == "0")
                {
                    gridView.Rows[i].Cells[6].Text = "事项";
                }
                else if (gridView.Rows[i].Cells[6].Text == "1")
                {
                    gridView.Rows[i].Cells[6].Text = "项目";
                }
                //时间截取年月日
                string finishTime = gridView.Rows[i].Cells[10].Text;
                if (finishTime.Length >= 16 && finishTime.Length < 17)
                {
                    gridView.Rows[i].Cells[10].Text = finishTime.Substring(0, 9);
                }
                else if (finishTime.Length >= 17)
                {
                    gridView.Rows[i].Cells[10].Text = finishTime.Substring(0, 10);
                }
            }
        }


        #endregion
        protected void gridView_RowDataBound2()
        {
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                gridView.Columns[2].Visible = false;
                gridView.Columns[9].Visible = false;
                gridView.Columns[12].Visible = false;
            }
        }
        //翻页
        #region
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            Page_Load(sender, e);
        }
        #endregion
        //添加议题
        #region  
        protected void AddSupervise_Click(object sender, EventArgs e)
        {
            SuperviseMxBLL superviseMxBLL = new SuperviseMxBLL();
            SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
            SuperviseBLL superviseBLL = new SuperviseBLL();
            S_UserInFo user = null;
            int num = 0;
            try
            {
                user = (S_UserInFo)Session["User"];
                user.ToString();
            }
            catch (Exception s)
            {
                Response.Redirect("Login.aspx");
            }
            string str = Request.Params["SetDeptCharge"];//获取部门信息

            //如果不添加小任务
            if (Request.Params["AddSmallTitle"] == "" || Request.Params["AddSmallTitle"] == null)
            {
                R_Supervise supervise = new R_Supervise();
                supervise.SuperviseType = int.Parse(AddTypeList.SelectedValue);
                supervise.Title = Request.Params["AddBigTitle"];
                supervise.Urgency = int.Parse(AddUrgencyList.SelectedValue);
                if (supervise.Title == null || supervise.Title == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "SetStyle()", true);
                    return;
                }
                else
                {
                    try
                    {
                        supervise.Mender = user.UserID;
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    supervise.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                    num = superviseBLL.AddSupervise(supervise);//新建议题后
                    R_Supervise supervise2 = superviseBLL.FindSuperviseByTitle(supervise.Title);//新增议题后 按议题内容查找刚刚添加的议题，进行任务添加



                    R_SuperviseAssign superviseAssign = new R_SuperviseAssign();
                    superviseAssign.RID = supervise2.RID;
                    superviseAssign.MxID = 0;
                    superviseAssign.ReplyMemo = "";
                    superviseAssign.bSate = 0;
                    superviseAssign.Memo = "";
                    superviseAssign.Mender = user.UserID;
                    superviseAssign.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    if (str == "" || str == null)
                    {
                        superviseAssign.AssignNo = "";
                        num = superviseAssignBLL.AddsuperviseAssign(superviseAssign);
                    }
                    else
                    {
                        string srt2 = str.Trim(',');
                        string[] sArray = srt2.Split(',');
                        foreach (string i in sArray)
                        {
                            superviseAssign.AssignNo = i.ToString();
                            num = superviseAssignBLL.AddsuperviseAssign(superviseAssign);
                        }
                    }
                }
            }
            else if (Request.Params["AddSmallTitle"] != "" || Request.Params["AddSmallTitle"] != null)//如果要添加小任务
            {
                R_Supervise supervise = new R_Supervise();
                supervise.SuperviseType = int.Parse(AddTypeList.SelectedValue);
                supervise.Title = Request.Params["AddBigTitle"];
                supervise.Urgency = int.Parse(AddUrgencyList.SelectedValue);
                try
                {
                    supervise.Mender = user.UserID;
                }
                catch (Exception ex)
                {
                    Response.Redirect("Login.aspx");
                }
                supervise.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                superviseBLL.AddSupervise(supervise);//新建议题后
                string AddBigTitle = Request.Params["AddBigTitle"];
                R_Supervise supervise2 = superviseBLL.FindSuperviseByTitle(AddBigTitle);
                R_SuperviseMx superviseMx = new R_SuperviseMx();
                superviseMx.Title = Request.Params["AddSmallTitle"];
                string FinishDate = Request.Params["AddFinishDate"].ToString();
                superviseMx.Mender = user.UserID;
                superviseMx.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                if (FinishDate == null || FinishDate == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "SetStyle()", true);
                    return;
                }
                else
                {
                    superviseMx.FinishDate = DateTime.Parse(FinishDate);
                    num = superviseMxBLL.AddSuperviseMx(superviseMx);//添加小任务成功
                }
                int MxID = superviseMxBLL.FindSuperviseMxID(Request.Params["AddSmallTitle"]); //添加小任务成功后返回任务ID
                R_SuperviseAssign superviseAssign = new R_SuperviseAssign();
                superviseAssign.RID = supervise2.RID;
                superviseAssign.MxID = MxID;
                superviseAssign.ReplyMemo = "";
                superviseAssign.bSate = 0;
                superviseAssign.Memo = "";
                superviseAssign.Mender = user.UserID;
                superviseAssign.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                if (str == "" || str == null)
                {
                    superviseAssign.AssignNo = "";
                    num = superviseAssignBLL.AddsuperviseAssign(superviseAssign);
                }
                else
                {
                    string srt2 = str.Trim(',');
                    string[] sArray = srt2.Split(',');
                    foreach (string i in sArray)
                    {
                        superviseAssign.AssignNo = i.ToString();
                        num = superviseAssignBLL.AddsuperviseAssign(superviseAssign);
                    }
                }
            }
            if (num != 0)
            {
                Response.Write("<script language='javascript'>alert('新增议题成功!')</script>");
                LoadData();
                Page_Load(sender, e);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('新增议题失败!')</script>");
            }
        }
        #endregion


        //监听gridview按钮事件 打开添加弹窗
        #region
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {   //点击修改议题
            if (e.CommandName == "openModifyBigTitleModal")
            {
                SuperviseMxBLL superviseMxBLL = new SuperviseMxBLL();
                SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
                string[] commandArgument = e.CommandArgument.ToString().Split(',');
                int RID = int.Parse(commandArgument[0]);
                int MxID = int.Parse(commandArgument[2]);
                ModiyRID.Text = RID.ToString();
                ModifyMxID2.Text = MxID.ToString();
                SuperviseBLL superviseBLL = new SuperviseBLL();
                R_Supervise supervise = superviseBLL.FindSupervise(RID);
                SqlDataReader read = superviseAssignBLL.FindSuperviseAssignByRID(RID);
                string AssignNoStr = "";
                string DeptNameStr = "";
                while (read.Read())
                {
                    if (read["AssignNo"].ToString() != "")
                    {
                        AssignNoStr += read["AssignNo"].ToString() + ",";
                        Session["OldAssignNo"] = AssignNoStr;
                    }
                    Session["OldAssignNo"] = AssignNoStr;
                    DeptNameStr += read["DeptName"].ToString() + ",";
                }
                SetDepartment2.Text = DeptNameStr;
                SetDeptCharge2.Text = AssignNoStr;
                ModifyBigTitle2.Text = supervise.Title;
                RegisterJS(@"
                  $('#ModifyBigTitleModal').modal({
                    show: true,
                    backdrop: 'static'
                });", this);
            } //删除议题
            else if (e.CommandName == "DeleteBigTitle_Click")
            {
                string RID = e.CommandArgument.ToString();
                SuperviseBLL superviseBLL = new SuperviseBLL();
                SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
                int num = superviseAssignBLL.DeleteSuperviseAssignByRID(RID);
                if (num != 0)
                {
                    Response.Write("<script language='javascript'>alert('删除成功!')</script>");
                    Page_Load(sender, e);
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('删除失败!')</script>");
                }
            }
            //点击添加步骤
            if (e.CommandName == "openModifySuperviseMxModal")
            {
                SuperviseMxBLL superviseMxBLL = new SuperviseMxBLL();
                string[] commandArgument = e.CommandArgument.ToString().Split(',');

                string AssignID = commandArgument[0];
                string RID = commandArgument[1];
                string bigTitle = commandArgument[2];
                ModifyAssignID.Text = AssignID.ToString();
                ModifyRID.Text = RID.ToString();
                ModifyBigTitle.Text = bigTitle;
                RegisterJS(@"
                 $('#ModifyTitle').val('');
                 $('#ModifyFinishDate').val('');
                 $('#ModifySetStaff').val('');
                  $('#ModifySuperviseMxModal').modal({
                    show: true,
                    backdrop: 'static'
                });", this);
            }
            //点击修改步骤
            else if (e.CommandName == "openModifySmallTitleModal")
            {
                string[] commandArgument = e.CommandArgument.ToString().Split(',');
                int MxID = int.Parse(commandArgument[0]);
                string bigTitle = commandArgument[1];
                string smallTitle = commandArgument[2];
                string AssignID = commandArgument[4];
                string RID = commandArgument[3];
                ModifyBigTitle3.Text = bigTitle;
                ModifySmallTitle.Text = smallTitle;
                ModifyMxID.Text = MxID.ToString();
                ModifyRID3.Text = RID;
                ModifySuperiseAssignID.Text = AssignID;
                SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
                SqlDataReader read = superviseAssignBLL.FindSuperviseAssignByMxID(MxID);
                string AssignNoStr = "";
                while (read.Read())
                {
                    if (read["AssignNo"].ToString() != "")
                    {
                        AssignNoStr += read["AssignNo"].ToString() + ",";
                        Session["OldStaff"] = AssignNoStr;
                    }
                }
                ModifySetStaff2.Text = AssignNoStr;
                SuperviseMxBLL superviseMxBLL = new SuperviseMxBLL();
                RegisterJS(@"
                  $('#ModifySmallTitleModal').modal({
                    show: true,
                    backdrop: 'static'
                });", this);
            }//删除任务
            else if (e.CommandName == "DeleteSmallTitle_Click")
            {
                string[] commandArgument = e.CommandArgument.ToString().Split(',');
                string MxID = commandArgument[0];
                string AssignD = commandArgument[1];
                SuperviseMxBLL superviseMxBLL = new SuperviseMxBLL();
                SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
                int num = superviseAssignBLL.DeleteSuperviseAssignByMxIDAndAssign(MxID, AssignD);
                if (num != 0)
                {
                    Response.Write("<script language='javascript'>alert('删除成功!')</script>");
                    Page_Load(sender, e);
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('删除失败!')</script>");
                }
            }
            //点击处理任务
            else if (e.CommandName == "openDealSuperviseMxModal")
            {
                string[] commandArgument = e.CommandArgument.ToString().Split(',');
                int assignID = int.Parse(commandArgument[0]);
                string bigTitle = commandArgument[1];
                string smallTitle = commandArgument[2];
                string updateReplyMemo = commandArgument[3];
                string updateMemo = commandArgument[4];
                UploadBLL uploadBLL = new UploadBLL();
                string UserID = Session["UserID"].ToString();
                SqlDataReader read=uploadBLL.SearchFileByAssignIDAndUserID(assignID,UserID);
                string file = "";
                while (read.Read())
                {
                    if (read["Path"].ToString() != "")
                    {
                        file += read["Path"].ToString() + ",";
                       
                    }
                }
                UpdateAssignID.Text = assignID.ToString();
                UpdateBigTitle.Text = bigTitle;
                UpdateSmallTitle.Text = smallTitle;
                UpdateReplyMemo.InnerText = updateReplyMemo;
                UpdateMemo.InnerText = updateMemo;
                string path2 = file.Replace("~/File/", "");
                UploadFile.InnerText= path2;
                RegisterJS(@"
                  $('#DealSuperviseMxModal').modal({
                    show: true,
                    backdrop: 'static'
                });", this);
            }else if (e.CommandName=="openSearchFiles")
            {
                UploadBLL uploadBLL = new UploadBLL();
                int AssignID = int.Parse(e.CommandArgument.ToString());
                DataSet ds = new DataSet();
                ds = uploadBLL.SearchFileByAssignID(AssignID);
                DataView dv = ds.Tables[0].DefaultView;
                FileGridView.DataSource = dv;
                FileGridView.DataBind();
                RegisterJS(@"
                $('#FileModal').modal({
                    show: true,
                    backdrop: 'static'
                }); ", this); 
            }

        }
        #endregion
        //控制前台JS的代码
        #region
        public static void RegisterJS(string sJavascriptCode, Page page)
        {
            string sKeyName = "MyRegisterJS";
            string sJS = @"<script type='text/javascript'>" + sJavascriptCode + "</script>";
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), sKeyName))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), sKeyName, sJS);
            }
        }
        #endregion


        //修改 即添加小任务 分派到员工

        #region  
        protected void SaveSuperviseAssign_Click(object sender, EventArgs e)
        {
            S_UserInFo user = null;
            int num = 0;
            try
            {
                user = (S_UserInFo)Session["User"];
                user.ToString();
            }
            catch (Exception s)
            {
                Response.Redirect("Login.aspx");
            }
            SuperviseMxBLL superviseMxBLL = new SuperviseMxBLL();
            SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
            R_SuperviseMx superviseMx = new R_SuperviseMx();
            int RID = int.Parse(Request.Params["ModifyRID"]);
            superviseMx.RID = RID;
            superviseMx.Title = Request.Params["ModifyTitle"];
            string FinishDate = Request.Params["ModifyFinishDate"].ToString();
            superviseMx.Mender = user.UserID;
            superviseMx.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            //判断是否分派
            string str = Request.Params["ModifySetStaff"];
            int assignID = int.Parse(ModifyAssignID.Text);
            R_SuperviseAssign superviseAssign2 = superviseAssignBLL.FindSuperviseAssign(assignID);
            int mxID = 0;
            if (str == null || str == "")
            {//如果新建任务未分派 则 处理人依然是分派的部门 

                if (FinishDate == null || FinishDate == "" || superviseMx.Title == "" || superviseMx.Title == null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "SetStyle()", true);
                    return;
                }
                else
                {
                    superviseMx.FinishDate = DateTime.Parse(FinishDate);
                    num = superviseMxBLL.AddSuperviseMx(superviseMx);//新增小任务成功
                    mxID = superviseMxBLL.FindSuperviseMxID(Request.Params["ModifyTitle"]);//新增成功返回MxID   用来分派 
                }
                R_SuperviseAssign superviseAssign = new R_SuperviseAssign();
                superviseAssign.RID = int.Parse(Request.Params["ModifyRID"]);
                superviseAssign.MxID = mxID;
                superviseAssign.AssignNo = user.UserID;
                superviseAssign.ReplyMemo = "";
                superviseAssign.bSate = 0;
                superviseAssign.Memo = "";
                superviseAssign.Mender = user.UserID;
                superviseAssign.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                num = superviseAssignBLL.AddsuperviseAssign(superviseAssign);

                superviseAssignBLL.DeleteSuperviseAssignByMxID(0, superviseAssign2.AssignNo);
            }
            else if (str != "" || str != null)
            {
                //如果分派 跟进人是分派的员工
                string srt2 = str.Trim(',');
                string[] sArray = srt2.Split(',');
                if (FinishDate == null || FinishDate == "" || superviseMx.Title == "" || superviseMx.Title == null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "SetStyle()", true);
                    return;
                }
                else
                {
                    superviseMx.FinishDate = DateTime.Parse(FinishDate);
                    num = superviseMxBLL.AddSuperviseMx(superviseMx);//新增小任务成功
                    mxID = superviseMxBLL.FindSuperviseMxID(Request.Params["ModifyTitle"]);//新增成功返回MxID   用来分派 
                }
                R_SuperviseAssign superviseAssign = superviseAssignBLL.FindSuperviseAssign(assignID);
                superviseAssign.RID = int.Parse(Request.Params["ModifyRID"]);
                superviseAssign.MxID = mxID;
                superviseAssign.ReplyMemo = "";
                superviseAssign.bSate = 0;
                superviseAssign.Memo = "";
                superviseAssign.Mender = user.UserID;
                superviseAssign.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                foreach (string i in sArray)
                {
                    superviseAssign.AssignNo = i.ToString();
                    num = superviseAssignBLL.AddsuperviseAssign(superviseAssign);
                    superviseAssignBLL.DeleteSuperviseAssignByMxID(0, superviseAssign2.AssignNo);
                }
            }
            if (num != 0)
            {
                Response.Write("<script language='javascript'>alert('新增任务成功!')</script>");
                Page_Load(sender, e);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('新增任务失败!')</script>");
            }
        }
        #endregion

        //处理任务提交
        protected void SaveDealSuperviseAssign_Click(object sender, EventArgs e)
        {
            S_UserInFo userInFo = (S_UserInFo)Session["User"];
            SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
            R_SuperviseAssign superviseAssign = superviseAssignBLL.FindSuperviseAssign(int.Parse(Request.Params["UpdateAssignID"]));
            superviseAssign.AssignID = int.Parse(Request.Params["UpdateAssignID"]);
            string updateReplyMemo = Request.Params["UpdateReplyMemo"];
            superviseAssign.ReplyMemo = updateReplyMemo;
            superviseAssign.bSate = int.Parse(UpdateBSate.SelectedValue);
            string updateMemo = Request.Params["UpdateMemo"];
            superviseAssign.Memo = updateMemo;
            superviseAssign.Mender = userInFo.UserID;
            superviseAssign.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            int num = superviseAssignBLL.updateSuperviseAssign(superviseAssign);


            R_SuperviseAssign superviseAssignDeparament = superviseAssignBLL.FindSuperviseAssign(superviseAssign.AssignID);
            if (num != 0)
            {
                Response.Write("<script language='javascript'>alert('任务处理成功!')</script>");
                LoadData();
            }
            else
            {
                Response.Write("<script language='javascript'>alert('任务处理失败!')</script>");
            }
        }
        //搜索功能
        #region
        protected void SuperviseSearch_Click(object sender, EventArgs e)
        {

            LoadData();
            Style();
        }
        #endregion
        //保存议题修改
        #region
        protected void SaveBigTitle_Click(object sender, EventArgs e)
        {
            SuperviseBLL superviseBLL = new SuperviseBLL();
            SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
            R_Supervise supervise = superviseBLL.FindSupervise(int.Parse(ModiyRID.Text));
            supervise.SuperviseType = int.Parse(ModifyTypeList.SelectedValue);
            supervise.Title = Request.Params["ModifyBigTitle2"];
            supervise.Urgency = int.Parse(ModifyUrgencyList.SelectedValue);
            S_UserInFo user = null;
            int num = 0;
            try
            {
                user = (S_UserInFo)Session["User"];
                user.ToString();
                supervise.Mender = user.UserID;
            }
            catch (Exception s)
            {
                Response.Redirect("Login.aspx");
            }
            supervise.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            if (supervise.Title == null || supervise.Title == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "SetStyle()", true);
                return;
            }
            else
            {
                if (Session["OldAssignNo"].ToString().Equals(Request.Params["SetDeptCharge2"]))
                {
                    num = superviseBLL.UpdateSupervise(supervise);
                }
                else
                {
                    num = superviseBLL.UpdateSupervise(supervise);
                    string OldAssignSrt2 = Session["OldAssignNo"].ToString().Trim(',');//分割旧的部门主管
                    string[] OldsArray = OldAssignSrt2.Split(',');
                    string NewAssignSrt2 = Request.Params["SetDeptCharge2"].Trim(',');//分割新的部门主管
                    string[] NewsArray = NewAssignSrt2.Split(',');
                    foreach (string item in NewsArray) //遍历intA中的元素
                    {
                        if (!OldsArray.Contains(item))//假如intA中的元素tem不包含在intB中
                        {
                            R_SuperviseAssign superviseAssign = new R_SuperviseAssign();
                            superviseAssign.RID = int.Parse(ModiyRID.Text);
                            superviseAssign.MxID = int.Parse(ModifyMxID2.Text);
                            superviseAssign.ReplyMemo = "";
                            superviseAssign.bSate = 0;
                            superviseAssign.Memo = "";
                            superviseAssign.Mender = user.UserID;
                            superviseAssign.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                            superviseAssign.AssignNo = item;
                            superviseAssignBLL.DeleteSuperviseAssignByRIDandAssignNozero(int.Parse(ModiyRID.Text));
                            num = superviseAssignBLL.AddsuperviseAssign(superviseAssign);
                        }
                    }
                }
            }
            if (num != 0)
            {
                Response.Write("<script language='javascript'>alert('更新成功!')</script>");
                Page_Load(sender, e);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('更新失败!')</script>");
            }
        }
        #endregion
        //保存步骤修改
        #region
        protected void SaveSmallTtile_Click(object sender, EventArgs e)
        {
            SuperviseMxBLL superviseMxBLL = new SuperviseMxBLL();
            R_SuperviseMx superviseMx = superviseMxBLL.FindSuperviseMx(int.Parse(ModifyMxID.Text));
            S_UserInFo user = null;
            int num = 0;
            try
            {
                user = (S_UserInFo)Session["User"];
                user.ToString();
            }
            catch (Exception s)
            {
                Response.Redirect("Login.aspx");
            }
            superviseMx.Title = Request.Params["ModifySmallTitle"];
            string FinishDate = Request.Params["ModifyFinishDate2"].ToString();
            superviseMx.Mender = user.UserID;
            superviseMx.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            if (superviseMx.Title == null || superviseMx.Title == "" || FinishDate == null || FinishDate == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "SetStyle()", true);
                return;
            }
            else
            {
                superviseMx.FinishDate = DateTime.Parse(FinishDate);
                superviseMxBLL.UpdateSuperviseMx(superviseMx);
                string OldAssignStr2 = Session["OldStaff"].ToString().Trim(',');//分割旧的部门主管
                string[] OldsArray = OldAssignStr2.Split(',');
                string NewAssignStr2 = Request.Params["ModifySetStaff2"].Trim(',');//分割新的部门主管
                string[] NewsArray = NewAssignStr2.Split(',');
                foreach (string item in NewsArray) //遍历intA中的元素
                {
                    if (!OldsArray.Contains(item))//假如intA中的元素tem不包含在intB中
                    {
                        R_SuperviseAssign superviseAssign = new R_SuperviseAssign();
                        superviseAssign.RID = int.Parse(ModifyRID3.Text);
                        superviseAssign.MxID = int.Parse(ModifyMxID.Text);
                        superviseAssign.ReplyMemo = "";
                        superviseAssign.bSate = 0;
                        superviseAssign.Memo = "";
                        superviseAssign.Mender = user.UserID;
                        superviseAssign.MendDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        superviseAssign.AssignNo = item;
                        SuperviseAssignBLL superviseAssignBLL = new SuperviseAssignBLL();
                        superviseAssignBLL.DeleteSuperviseAssignByRIDandAssignNozero(int.Parse(ModifyRID3.Text));
                        num = superviseAssignBLL.AddsuperviseAssign(superviseAssign);
                    }
                }
            }
            if (num != 0)
            {
                Response.Write("<script language='javascript'>alert('步骤更新成功!')</script>");
                Page_Load(sender, e);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('步骤更新失败!')</script>");
            }
        }
        #endregion

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void OutAsExcel(object sender, EventArgs e)
        {
            this.gridView.AllowPaging = false;//导出前关闭分页
            this.gridView.AllowSorting = false;//导出前关闭排序
            LoadData();//加载数据
            gridView_RowDataBound2();
            for (int i = 0; i < gridView.Columns.Count; i++)//设置边框 ，因为layer的边框好像不显示
            {
                this.gridView.Columns[i].HeaderStyle.BorderWidth = 1;
                this.gridView.Columns[i].ItemStyle.BorderWidth = 1;
            }
            toExcel(this.gridView);//执行导出方法

            this.gridView.AllowPaging = true;//导出后显示分页
            this.gridView.AllowSorting = true;//导出后显示排序
            Page_Load(sender, e);//获取数据并绑定到GridView
        }
        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="gv"></param>
        void toExcel(GridView gv)
        {
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            string fileName = "export.xls";
            string style = @"<style> .text { mso-number-format:\@; } </script> ";
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.gridView.RenderControl(htw);
            Response.Write(style);
            Response.Write(sw.ToString());
            Response.End();
        }
        /// <summary>
        /// 这个重写貌似是必须的
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void FileGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DownLoadFile_Click")
            {
                string filePath = e.CommandArgument.ToString();
                filePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["AttachmentPath"] + filePath);
                string fileName = Path.GetFileName(filePath);

                FileStream fs = new FileStream(filePath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];   //以字符流的形式下载文件
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                Response.ContentType = "application/octet-stream";  //通知浏览器下载文件而不是打开

                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            else if (e.CommandName == "DeleteFile_Click")
            {
                string userID = Session["UserID"].ToString();
                string[] commandArgument = e.CommandArgument.ToString().Split(',');
                int ID = int.Parse( commandArgument[0].ToString());
                string UserID = commandArgument[1];
                if (userID.Equals(UserID)){
                    UploadBLL uploadBLL = new UploadBLL();
                    int num=uploadBLL.DeleteFileByID(ID);
                    if (num == 0)
                    {
                        Response.Write("<script language='javascript'>alert('删除附件成功!')</script>");
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('无权限!')</script>");
                }
            }
        }

        protected void FileGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                
                //动态序号
                if (e.Row.RowIndex != -1)
                {
                    int indexID = this.gridView.PageIndex * this.gridView.PageSize + e.Row.RowIndex + 1;
                    e.Row.Cells[0].Text = indexID.ToString();
                }
                e.Row.Cells[1].Visible = false;
                }
            }
    }
}