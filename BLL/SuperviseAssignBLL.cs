using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public partial class SuperviseAssignBLL
    {
        public int AddsuperviseAssign(R_SuperviseAssign superviseAssign)
        {
            return SuperviseAssignDAL.AddsuperviseAssign(superviseAssign);
        }

        public DataSet SearchSuperviseAssignByDeparament(string deptparament)
        {
            return SuperviseAssignDAL.SearchSuperviseAssignByDeparament(deptparament);
        }

        public R_SuperviseAssign FindSuperviseAssign(int assignID)
        {
            return SuperviseAssignDAL.FindSuperviseAssign(assignID);
        }

        public int updateSuperviseAssign(R_SuperviseAssign superviseAssign)
        {
            return SuperviseAssignDAL.updateSuperviseAssign(superviseAssign);
        }
        public DataSet FindAllSuperviseAssign()
        {
            return SuperviseAssignDAL.FindAllSuperviseAssign();
        }

        public int DeleteSuperviseAssignByMxID(int mxID,string AssignNo)
        {
            return SuperviseAssignDAL.DeleteSuperviseAssignByMxID(mxID, AssignNo);
        }

        public DataSet SearchSuperviseAssignByUserID(string userID)
        {
            return SuperviseAssignDAL.SearchSuperviseAssignByUserID(userID);
        }

        public R_SuperviseAssign FindSuperviseAssign(int mxID, string AssignNo)
        {
            return SuperviseAssignDAL.FindSuperviseAssign(mxID, AssignNo);
        }

        public SqlDataReader FindSuperviseAssignByMxID(int mxID)
        {
            return SuperviseAssignDAL.FindSuperviseAssignByMxID(mxID);
        }

        public SqlDataReader FindSuperviseAssignByMxIDNotDep(int mxID)
        {
            return SuperviseAssignDAL.FindSuperviseAssignByMxIDNotDep(mxID);
        }

        public int DeleteSuperviseAssignByMxIDZero(int RID)
        {
            return  SuperviseAssignDAL.DeleteSuperviseAssignByMxIDZero();
        }

        public DataSet FindSuperviseAssignByMore(List<string> listWhere)
        {
            return SuperviseAssignDAL.FindSuperviseAssignByMore(listWhere);
        }

        public int DeleteSuperviseAssignByRID(string rID)
        {
            return SuperviseAssignDAL.DeleteSuperviseAssignByRID(rID);
        }

        public int DeleteSuperviseAssignByMxIDAndAssign(string mxID,string assignID)
        {
            return SuperviseAssignDAL.DeleteSuperviseAssignByMxIDAndAssign(mxID, assignID);
        }
    }
}
