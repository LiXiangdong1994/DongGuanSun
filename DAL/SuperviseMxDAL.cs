using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DBUtility;
namespace DAL
{
    public partial class SuperviseMxDAL
    {
        public static DataSet SearchAllSuperviseMx()
        {
            string sql = "select R_Supervise.Title as BigTitle,R_SuperviseMx.* from R_SuperviseMx INNER JOIN R_Supervise ON R_Supervise.RID =R_SuperviseMx.RID";
            List<R_SuperviseMx> list = new List<R_SuperviseMx>();
            DataSet set = DbHelperSQL.Query(sql);
            return set;
        }

        public static int DeleteSuperviseMx(string mxID)
        {
            string sql = "delete from R_SuperviseMx where MxID={0}";
            sql = string.Format(sql, mxID);
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

        public static int UpdateSuperviseMx(R_SuperviseMx superviseMx)
        {
            string sql = "update  R_SuperviseMx set Title='{0}',Mender='{1}',MendDate='{2}' where MxID={3}";
            sql = string.Format(sql, superviseMx.Title, superviseMx.Mender, superviseMx.MendDate, superviseMx.MxID);
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

        public static int FindSuperviseMxID(string title)
        {
            string sql = "select * from R_SuperviseMx where Title='{0}'";
            sql = string.Format(sql, title);
            try
            {
                int mxID = int.Parse(DbHelperSQL.GetSingle(sql).ToString());
                return mxID;
            }
            catch
            {
                return 0;
            }
        }

        public static R_SuperviseMx FindSuperviseMx(int MxID)
        {
            string sql1 = "select * from R_SuperviseMx where MxID='{0}'";
            sql1 = string.Format(sql1, MxID);
            DataSet ds = DbHelperSQL.Query(sql1);
            R_SuperviseMx superviseMx = new R_SuperviseMx();
            superviseMx.MxID = MxID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                superviseMx.RID = int.Parse(ds.Tables[0].Rows[0]["RID"].ToString());
                superviseMx.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                superviseMx.FinishDate = DateTime.Parse( ds.Tables[0].Rows[0]["FinishDate"].ToString());
                superviseMx.Mender = ds.Tables[0].Rows[0]["Mender"].ToString();
                superviseMx.MendDate = DateTime.Parse(ds.Tables[0].Rows[0]["MendDate"].ToString());
                return superviseMx;
            }
            else
            {
                return null;
            }
        }

        public static int AddSuperviseMx(R_SuperviseMx superviseMx)
        {
            string sql = "insert into R_SuperviseMx(RID,Title,FinishDate,Mender,MendDate) values({0},'{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, superviseMx.RID, superviseMx.Title, superviseMx.FinishDate,superviseMx.Mender, superviseMx.MendDate);
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
