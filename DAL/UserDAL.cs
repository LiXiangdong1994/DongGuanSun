using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DBUtility;
using System.Data;

namespace DAL
{
    public partial class UserDAL
    {
        public static SqlDataReader StaffLogin(S_UserInFo userInFo)
        {
            string sql1 = "select * from S_UserInFo where UserID= '{0}'and UserPWD='{1}'";
            sql1 = string.Format(sql1, userInFo.UserID, userInFo.UserPWD);
            try
            {
                SqlDataReader read = DbHelperSQL.ExecuteReader(sql1);
                return read;
            }
            catch
            {
                return null;
            }
        }

        public static DataSet selectAllStaffSet()
        {
            string sql1 = "select C_Code.CodeName as CodeName ,S_UserInFo.* from S_UserInFo ,C_Code where S_UserInFo.DeptID=C_Code.OID";
            sql1 = string.Format(sql1);
            try
            {
                DataSet set = DbHelperSQL.Query(sql1);
                return set;
            }
            catch
            {
                return null;
            }
        }

        public static int UpdateUserInFo(S_UserInFo userInFo)
        {
            string sql = "update S_UserInFo set UserID='{0}',UserPWD='{1}',DeptID='{2}',bOFF='{3}',Memo='{4}',Mender='{5}',MendDate='{6}' where UID={7}";
            sql = string.Format(sql, userInFo.UserID, userInFo.UserPWD, userInFo.DeptID, userInFo.bOFF, userInFo.Memo, userInFo.Mender, userInFo.MendDate, userInFo.UID);
            try
            {
                int num = DbHelperSQL.ExecuteSql(sql);
                return num;
            }
            catch
            {
                return 0;
            }
        }

        public static S_UserInFo FindUserInfo(int uID)
        {
            string sql1 = "select * from S_UserInFo  where UID='{0}'";
            sql1 = string.Format(sql1, uID);
            DataSet ds = DbHelperSQL.Query(sql1);
            S_UserInFo userInFo = new S_UserInFo();
            userInFo.UID = uID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                userInFo.UID = int.Parse(ds.Tables[0].Rows[0]["UID"].ToString());
                userInFo.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
                userInFo.UserPWD = ds.Tables[0].Rows[0]["UserPWD"].ToString();
                userInFo.Power = int.Parse(ds.Tables[0].Rows[0]["Power"].ToString());
                userInFo.DeptID = int.Parse(ds.Tables[0].Rows[0]["DeptID"].ToString());
                userInFo.bOFF = int.Parse(ds.Tables[0].Rows[0]["bOFF"].ToString());
                userInFo.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                userInFo.Mender = ds.Tables[0].Rows[0]["Mender"].ToString();
                userInFo.MendDate = DateTime.Parse(ds.Tables[0].Rows[0]["MendDate"].ToString());
                return userInFo;
            }
            else
            {
                return null;
            }
        }

        public static DataSet selectStaffSet(int DeptID)
        {
            string sql1 = "select * from vwS_UserInFo where DeptID= '{0}' and Power <>0";
            sql1 = string.Format(sql1, DeptID);
            try
            {
                DataSet set = DbHelperSQL.Query(sql1);
                return set;
            }
            catch
            {
                return null;
            }
        }

        public static int AddUserInFo(S_UserInFo userInFo)
        {
            string sql = "insert into S_UserInFo(UserID,UserPWD,Power,DeptID,bOFF,Memo,Mender,MendDate) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
            sql = string.Format(sql, userInFo.UserID,userInFo.UserPWD, userInFo.Power, userInFo.DeptID,userInFo.bOFF,userInFo.Memo,userInFo.Mender,userInFo.MendDate);
            try
            {
                int num = DbHelperSQL.ExecuteSql(sql);
                return num;
            }
            catch
            {
                return 0;
            }
        }
    }
}
