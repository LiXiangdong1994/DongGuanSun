using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public partial class SuperviseAssignDAL
    {
        public static int AddsuperviseAssign(R_SuperviseAssign superviseAssign)
        {
            string sql = "insert into R_SuperviseAssign (RID,MxID,AssignNo,ReplyMemo,bSate,Memo,Mender,MendDate) values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
            sql = string.Format(sql, superviseAssign.RID, superviseAssign.MxID,superviseAssign.AssignNo, superviseAssign.ReplyMemo, superviseAssign.bSate, superviseAssign.Memo, superviseAssign.Mender,superviseAssign.MendDate);
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

        public static int updateSuperviseAssign(R_SuperviseAssign superviseAssign)
        {
            string sql = "update  R_SuperviseAssign set ReplyMemo='{0}',bSate={1},Memo='{2}',Mender='{3}',MendDate='{4}' ,AssignNo='{5}' where AssignID={6}";
            sql = string.Format(sql,superviseAssign.ReplyMemo, superviseAssign.bSate, superviseAssign.Memo, superviseAssign.Mender, superviseAssign.MendDate, superviseAssign.AssignNo,superviseAssign.AssignID);
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

        public static DataSet SearchSuperviseAssignByUserID(string userID)
        {
            string sql = "select *  from vwR_SuperviseAssign where AssignNo='{0}'";
            sql = string.Format(sql, userID);
            try
            {
                DataSet set = DbHelperSQL.Query(sql);
                return set;
            }
            catch
            {
                return null;
            }
        }

        public static int DeleteSuperviseAssignByMxIDZero()
        {
            string sql = "delete from  R_SuperviseAssign where MxID=0";
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

        public static int DeleteSuperviseAssignByMxIDAndAssign(string mxID,string assignID)
        {
            string sql = "delete from  R_SuperviseAssign where AssignID={0}";
            sql = string.Format(sql, assignID);
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

        public static SqlDataReader FindSuperviseAssignByRID(int rID)
        {
            string sql = "select R_SuperviseAssign.AssignNo,(select DeptName from vwS_UserInFo where R_SuperviseAssign.AssignNo=UserID)  as DeptName from R_SuperviseAssign  where RID='{0}'";
            sql = string.Format(sql, rID);
            try
            {
                SqlDataReader read = DbHelperSQL.ExecuteReader(sql);
                return read;
            }
            catch
            {
                return null;
            }
        }

        public static int DeleteSuperviseAssignByRIDandAssignNozero(int rID)
        {
            string sql = "delete from  R_SuperviseAssign where RID={0} and AssignNo=''";
            sql = string.Format(sql, rID);
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

        public static int DeleteSuperviseAssignByRID(string rID)
        {
            string sql = "delete from  R_SuperviseAssign where RID={0}";
            sql = string.Format(sql, rID);
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

        public static DataSet FindSuperviseAssignByMore(List<string> listWhere)
        {
            string sql = "select * ,(select vwR_SuperviseAssign.DeptName+'(部长：'+vwS_UserInFo.UserID+')' from vwS_UserInFo where vwS_UserInFo.DeptName=vwR_SuperviseAssign.DeptName and vwS_UserInFo.Power=0) as bing from vwR_SuperviseAssign";
            DataSet set = null;
            if (listWhere.Count > 0)
            {
                string sqlWhere = string.Join(" and ", listWhere.ToArray());
                sql = sql + " where " + sqlWhere+ "order by RID asc";
            }
            else
            {
                sql = "select * ,(select vwR_SuperviseAssign.DeptName+'(部长：'+vwS_UserInFo.UserID+')' from vwS_UserInFo where vwS_UserInFo.DeptName=vwR_SuperviseAssign.DeptName and vwS_UserInFo.Power=0) as bing from vwR_SuperviseAssign order by RID asc";
            }
            try
            {
                 set = DbHelperSQL.Query(sql);
            }
            catch
            {
                return set;
            }
            return set;
        }

        public static SqlDataReader FindSuperviseAssignByMxID(int mxID)
        {
           string sql= "select * from R_SuperviseAssign  where mxID='{0}'";
            sql= string.Format(sql, mxID);
            try
            {
                SqlDataReader read = DbHelperSQL.ExecuteReader(sql);
                return read;
            }
            catch
            {
                return null;
            }
        }

        public static SqlDataReader FindSuperviseAssignByMxIDNotDep(int mxID)
        {
            string sql = "select * from R_SuperviseAssign  where mxID='{0}' and AssignNo not like '%部'";
            sql = string.Format(sql, mxID);
            try
            {
                SqlDataReader read = DbHelperSQL.ExecuteReader(sql);
                return read;
            }
            catch
            {
                return null;
            }
        }

        public static R_SuperviseAssign FindSuperviseAssign(int mxID, string assignNo)
        {
            string sql1 = "select * from R_SuperviseAssign  where mxID='{0}' and AssignNo='{1}'";
            sql1 = string.Format(sql1, mxID, assignNo);
            DataSet ds = DbHelperSQL.Query(sql1);
            R_SuperviseAssign superviseAssign = new R_SuperviseAssign();
            if (ds.Tables[0].Rows.Count > 0)
            {
                superviseAssign.AssignID = int.Parse(ds.Tables[0].Rows[0]["AssignID"].ToString());
                superviseAssign.RID = int.Parse(ds.Tables[0].Rows[0]["RID"].ToString());
                superviseAssign.MxID = int.Parse(ds.Tables[0].Rows[0]["MxID"].ToString());
                superviseAssign.AssignNo = ds.Tables[0].Rows[0]["AssignNo"].ToString();
                superviseAssign.ReplyMemo = ds.Tables[0].Rows[0]["ReplyMemo"].ToString();
                superviseAssign.bSate = int.Parse(ds.Tables[0].Rows[0]["bSate"].ToString());
                superviseAssign.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                superviseAssign.Mender = ds.Tables[0].Rows[0]["Mender"].ToString();
                superviseAssign.MendDate = DateTime.Parse(ds.Tables[0].Rows[0]["MendDate"].ToString());
                return superviseAssign;
            }
            else
            {
                return null;
            }
        }

        public static int DeleteSuperviseAssignByMxID(int mxID,string AssignNo)
        {
            string sql = "delete from  R_SuperviseAssign where MxID={0}  and AssignNo='{1}'";
            sql = string.Format(sql,mxID, AssignNo);
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

        public static DataSet FindAllSuperviseAssign()
        {
            string sql = "select *  from vwR_SuperviseAssign order by RID asc";
            sql = string.Format(sql);
            try
            {
                DataSet set = DbHelperSQL.Query(sql);
                return set;
            }
            catch
            {
                return null;
            }
        }

        public static R_SuperviseAssign FindSuperviseAssign(int assignID)
        {
            string sql1 = "select * from R_SuperviseAssign  where AssignID='{0}'";
            sql1 = string.Format(sql1, assignID);
            DataSet ds = DbHelperSQL.Query(sql1);
            R_SuperviseAssign superviseAssign = new R_SuperviseAssign();
            superviseAssign.AssignID = assignID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                superviseAssign.RID = int.Parse(ds.Tables[0].Rows[0]["RID"].ToString());
                superviseAssign.MxID = int.Parse(ds.Tables[0].Rows[0]["MxID"].ToString());
                superviseAssign.AssignNo = ds.Tables[0].Rows[0]["AssignNo"].ToString();
                superviseAssign.ReplyMemo = ds.Tables[0].Rows[0]["ReplyMemo"].ToString();
                superviseAssign.bSate = int.Parse(ds.Tables[0].Rows[0]["bSate"].ToString());
                superviseAssign.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                superviseAssign.Mender = ds.Tables[0].Rows[0]["Mender"].ToString();
                superviseAssign.MendDate = DateTime.Parse(ds.Tables[0].Rows[0]["MendDate"].ToString());
                return superviseAssign;
            }
            else
            {
                return null;
            }
        }

        public static DataSet SearchSuperviseAssignByDeparament(string deptparament)
        {
            string sql = "select *  from vwR_SuperviseAssign where DeptName='{0}'";
            sql = string.Format(sql, deptparament);
            try
            {
                DataSet set = DbHelperSQL.Query(sql);
                return set;
            }
            catch
            {
                return null;
            }


        }
    }
}
