using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BLL;
namespace NewSupersive.Admin
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : HttpPostedFileBase,IHttpHandler 
    {

        public void ProcessRequest(HttpContext context)
        {
            if (true)
            {
                context.Response.ContentType = "text/plain";
                result result = new result();
                string fileNewName = string.Empty;
                string filePath = string.Empty;
                HttpFileCollection files = context.Request.Files;
                string ID = context.Request["ID"].ToString();
                string UserID = context.Request["UserID"].ToString();
                if (files.Count > 0)
                {
                    ArrayList alist = new ArrayList();
                    int count = context.Request.Files.Count;
                    string ImgUrl = string.Empty;
                    string VendorPid = string.Empty;
                    for (int i = 0; i < count; i++)
                    {
                        //设置文件名
                        fileNewName = DateTime.Now.ToString("yyyyMMddHHmmssff") + "_" + System.IO.Path.GetFileName(files[0].FileName);
                        //保存文件  

                        files[0].SaveAs(context.Server.MapPath("~/File/" + fileNewName));
                        //result.code = "200";
                        //result.msg = "文件上传成功！";
                        result.code = "0";
                        result.msg = "文件上传成功！";
                        context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(result));
                        string virtualPath = String.Format("~/File/{0}", fileNewName);//上传到指定文件夹
                        string path = virtualPath;//相对获取文件路径
                        F_Files file = new F_Files();
                        file.SuperviseAssignID = ID;
                        file.UserID = UserID;
                        file.Path = path;
                        file.UploadTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));;
                        UploadBLL uploadBLL = new UploadBLL();
                        int num=uploadBLL.AddFile(file);
                    }
                    
                }
                else
                {
                }
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}