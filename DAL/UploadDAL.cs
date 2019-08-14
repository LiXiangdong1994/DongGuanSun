using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class UploadDAL
    {
        public static int AddFile(F_Files file)
        {
            string sql = "insert into F_Files(SuperviseAssignID,UserID,Path,UploadTime) values({0},'{1}','{2}','{3}')";
            sql = string.Format(sql, file.SuperviseAssignID, file.UserID, file.Path, file.UploadTime);
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

        public static DataSet SearchFileByAssignID(int assignID)
        {
            string sql = "select * from F_Files  where SuperviseAssignID='{0}'";
            sql = string.Format(sql, assignID);
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

        public static int DeleteFileByID(int iD)
        {
            string sql = "delete from F_File where ID={0}";
            sql = string.Format(sql, iD);
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

        public static SqlDataReader SearchFileByAssignIDAndUserID(int assignID, string userID)
        {
            string sql = "select * from F_Files  where SuperviseAssignID='{0}' and UserID='{1}'";
            sql = string.Format(sql, assignID, userID);
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
    }
}
