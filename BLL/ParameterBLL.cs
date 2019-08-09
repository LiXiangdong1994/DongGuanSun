using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
namespace BLL
{
    public partial class ParameterBLL
    {
        public DataSet SearchAllCode()
        {
            return ParameterDAL.SearchAllCode();
        }

        public int DeleteParameter(string oID)
        {
            return ParameterDAL.DeleteParameter(oID);
        }

        public int AddParameter(C_Code code)
        {
            return ParameterDAL.AddParameter(code);
        }
        public SqlDataReader selectDepartments()
        {
            return ParameterDAL.selectDepartments();
        }
        public DataSet selectDepartmentsSet()
        {
            return ParameterDAL.selectDepartmentsSet();
        }

        public C_Code findCode(int oID)
        {
            return ParameterDAL.findCode(oID);
        }

        public int UpdateCode(C_Code code)
        {
            return ParameterDAL.UpdateCode(code);
        }
    }
}
