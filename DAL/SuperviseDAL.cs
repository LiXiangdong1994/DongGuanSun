using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DBUtility;
using System.Data.SqlClient;

namespace DAL
{
    public partial class SuperviseDAL
    {
        public static DataSet SearchAllSupervise()
        {
            string sql = "select * from R_Supervise";
            List<R_Supervise> list = new List<R_Supervise>();
            DataSet set = DbHelperSQL.Query(sql);
            return set;
        }
        public static SqlDataReader SearchAllSuperviseList()
        {
            string sql = "select * from R_Supervise";
            List<R_Supervise> list = new List<R_Supervise>();
            SqlDataReader read = DbHelperSQL.ExecuteReader(sql);
            return read;
        }

        public static int DeleteSupervise(string rID)
        {
            string sql = "delete from R_Supervise where RID={0}";
            sql = string.Format(sql,rID);
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

        public static int UpdateSupervise(R_Supervise supervise)
        {
            string sql = "update  R_Supervise set SuperviseType='{0}',Title='{1}',Urgency='{2}',Mender='{3}',MendDate='{4}' where RID={5}";
            sql = string.Format(sql, supervise.SuperviseType, supervise.Title, supervise.Urgency,supervise.Mender, supervise.MendDate, supervise.RID);
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

        public static R_Supervise FindSupervise(int rID)
        {
            string sql1 = "select * from R_Supervise where RID='{0}'";
            sql1 = string.Format(sql1, rID);
            DataSet ds = DbHelperSQL.Query(sql1);
            R_Supervise supervise = new R_Supervise();
            supervise. RID = rID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                supervise.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                supervise.Mender = ds.Tables[0].Rows[0]["Mender"].ToString();
                supervise.MendDate = DateTime.Parse( ds.Tables[0].Rows[0]["MendDate"].ToString());
                return supervise;
            }
            else
            {
                return null;
            }
        }
        public static R_Supervise FindSuperviseByTitle(string title)
        {
            string sql1 = "select * from R_Supervise where Title='{0}'";
            sql1 = string.Format(sql1, title);
            DataSet ds = DbHelperSQL.Query(sql1);
            R_Supervise supervise = new R_Supervise();

            if (ds.Tables[0].Rows.Count > 0)
            {
                supervise.RID = int.Parse(ds.Tables[0].Rows[0]["RID"].ToString());
                supervise.SuperviseType = int.Parse(ds.Tables[0].Rows[0]["SuperviseType"].ToString());
                supervise.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                supervise.Urgency = int.Parse(ds.Tables[0].Rows[0]["Urgency"].ToString());
                supervise.Mender = ds.Tables[0].Rows[0]["Mender"].ToString();
                supervise.MendDate = DateTime.Parse(ds.Tables[0].Rows[0]["MendDate"].ToString());
                return supervise;
            }
            else
            {
                return null;
            }
        }


        public static int AddSupervise(R_Supervise supervise)
        {
            string sql = "insert into R_Supervise(SuperviseType,Title,Urgency,Mender,MendDate) values('{0}','{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, supervise.SuperviseType, supervise.Title, supervise.Urgency, supervise.Mender,supervise.MendDate);
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
