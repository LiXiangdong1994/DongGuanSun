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
    public partial class UserBLL
    {
        public SqlDataReader StaffLogin(S_UserInFo userInFo)
        {
            return UserDAL.StaffLogin(userInFo);
        }

        public int AddUserInFo(S_UserInFo userInFo)
        {
            return UserDAL.AddUserInFo(userInFo);
        }

        public DataSet selectStaffSet(int DeptID)
        {
            return UserDAL.selectStaffSet(DeptID);
        }
        public DataSet selectAllStaffSet()
        {
            return UserDAL.selectAllStaffSet();
        }

        public S_UserInFo FindUserInfo(int uID)
        {
            return UserDAL.FindUserInfo(uID);
        }

        public int UpdateUserInFo(S_UserInFo userInFo)
        {
            return UserDAL.UpdateUserInFo(userInFo);
        }
    }
}
