using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
using System.Data.SqlClient;

namespace BLL
{
    public partial class SuperviseBLL
    {
        public DataSet SearchAllSupervise()
        {
            return SuperviseDAL.SearchAllSupervise();
        }
        public SqlDataReader SearchAllSuperviseList()
        {
            return SuperviseDAL.SearchAllSuperviseList();
        }

        public int DeleteSupervise(string rID)
        {
            return SuperviseDAL.DeleteSupervise(rID);
        }

        public int AddSupervise(R_Supervise supervise)
        {
            return SuperviseDAL.AddSupervise(supervise);
        }

        public R_Supervise FindSupervise(int rID)
        {
            return SuperviseDAL.FindSupervise(rID);
        }

        public int UpdateSupervise(R_Supervise supervise)
        {
            return SuperviseDAL.UpdateSupervise(supervise);
        }

        public R_Supervise FindSuperviseByTitle(string title)
        {
            return SuperviseDAL.FindSuperviseByTitle(title);
        }
    }
}
