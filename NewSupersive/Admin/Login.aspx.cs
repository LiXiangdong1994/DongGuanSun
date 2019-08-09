using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;
using System.Data.SqlClient;

namespace NewSupersive.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.GoLogin.ServerClick += Login_Click;
        }
        protected void Login_Click(object sender, EventArgs e)
        {
            S_UserInFo userInFo = new S_UserInFo();
            userInFo.UserID = Request.Params["UserName"];
            userInFo.UserPWD = Request.Params["Password"];
            UserBLL staffBLL = new UserBLL();
            ParameterBLL parameterBLL = new ParameterBLL();
            SqlDataReader read = staffBLL.StaffLogin(userInFo);
            if (read.Read())
                {
                    int DeptID = int.Parse(read["DeptID"].ToString());
                    C_Code code = parameterBLL.findCode(DeptID);
                    string UserID = read["UserID"].ToString();
                    int Power = int.Parse(read["Power"].ToString());
                    string CodeName = code.CodeName;

                    S_UserInFo user = new S_UserInFo();
                    user.UserID = UserID;//用户名称
                    user.DeptID = DeptID;//用户部门ID
                    user.Power = Power;//用户权限
                    user.Memo = CodeName;//此次用户所属部门名称用备注先存着
                    if (userInFo.bOFF == 1)
                    {
                        Response.Write("<script language='javascript'>alert('当前用户已被禁用" + userInFo.Memo + "')</script>");
                        return;
                    }
                    else
                    {
                        Session.Remove("User");
                        Session["UserID"] = UserID;
                        Session["User"] = user;//用户名称，所属部门ID
                        Response.Redirect("Index.aspx");
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('用户名或密码错误!');window.location.href=document.URL;</script>");
                    return;
                }
            }
    }
}