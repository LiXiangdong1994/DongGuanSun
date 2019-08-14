using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBUtility;
using Models;
namespace DAL
{
    public partial class ParameterDAL
    {
        //查询所有参数
        public static DataSet SearchAllCode()
        {
            string sql1 = "select * from C_Code";

            List<C_Code> list = new List<C_Code>();
            DataSet set = DbHelperSQL.Query(sql1);
            return set;
        }

        public static int AddParameter(C_Code code)
        {
            string sql = "insert into C_Code(CodeClass,CodeName,Memo,Mender,MendDate) values('{0}','{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, code.CodeClass, code.CodeName, code.Memo, code.Mender, code.MendDate);
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
        //返回SqlDataReader
        public static SqlDataReader selectDepartments()
        {
            string sql = "select * from C_Code where CodeClass='部门'";
            sql = string.Format(sql);
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
        //返回DataSet
        public static DataSet selectDepartmentsSet()
        {
            string sql = "select S_UserInFo.UID,S_UserInFo.UserID ,S_UserInFo.DeptID,vwC_Code.CodeName as DepartmentName from S_UserInFo ,vwC_Code where Power=0 and vwC_Code.OID=S_UserInFo.DeptID";
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

        public static int UpdateCode(C_Code code)
        {
            string sql = "update  C_Code set CodeClass='{0}',CodeName='{1}',Memo='{2}',Mender='{3}',MendDate='{4}' where OID={5}";
            sql = string.Format(sql, code.CodeClass, code.CodeName, code.Memo, code.Mender, code.MendDate,code.OID);
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

        public static C_Code findCode(int oID)
        {
            string sql1 = "select * from C_Code where OID='{0}'";
            sql1 = string.Format(sql1, oID);
            DataSet ds = DbHelperSQL.Query(sql1);
            C_Code code = new C_Code();
            code.OID = oID;
            if (ds.Tables[0].Rows.Count > 0)
            {
                code.CodeClass = ds.Tables[0].Rows[0]["CodeClass"].ToString();
                code.CodeName = ds.Tables[0].Rows[0]["CodeName"].ToString();
                code.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                return code;
            }
            else
            {
                return null;
            }
        }

        public static int DeleteParameter(string oID)
        {
            string sql1 = "delete from  C_Code where OID='{0}'";
            sql1 = string.Format(sql1, oID);
            try
            {
                int num = DbHelperSQL.ExecuteSql(sql1);
                return num;
            }
            catch
            {
                return 0;
            }
        }
    }
}
