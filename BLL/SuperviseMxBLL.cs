using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
namespace BLL
{
    public partial class SuperviseMxBLL
    {
        public DataSet SearchAllSuperviseMx()
        {
            return SuperviseMxDAL.SearchAllSuperviseMx();
        }

        public int DeleteSuperviseMx(string mxID)
        {
            return SuperviseMxDAL.DeleteSuperviseMx(mxID);
        }

        public int AddSuperviseMx(R_SuperviseMx superviseMx)
        {
            return SuperviseMxDAL.AddSuperviseMx(superviseMx);
        }

        public R_SuperviseMx FindSuperviseMx(int MxID)
        {
            return SuperviseMxDAL.FindSuperviseMx(MxID);
        }

        public int UpdateSuperviseMx(R_SuperviseMx superviseMx)
        {
            return SuperviseMxDAL.UpdateSuperviseMx(superviseMx);
        }

        public int FindSuperviseMxID(string Title)
        {
            return SuperviseMxDAL.FindSuperviseMxID(Title);
        }
    }
}
