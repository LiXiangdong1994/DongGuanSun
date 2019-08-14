using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BLL;
namespace NewSupersive.Admin
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                S_UserInFo user = (S_UserInFo)Session["User"];
                user.ToString();
            } catch (Exception ex)
            {
                Response.Redirect("Login.aspx");
            }
           

        }
        protected void GoLogout_Click(object sender,EventArgs e)
        {
            Session.Remove("User");
            Response.Redirect("Login.aspx");
        }
        protected void Login_Click(object sender, EventArgs e)
        {
            Session.Remove("User");
            Response.Redirect("Login.aspx");
        }

    }
}