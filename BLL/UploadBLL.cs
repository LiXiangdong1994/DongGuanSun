using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    public class UploadBLL
    {
        public int AddFile(F_Files file)
        {
            return UploadDAL.AddFile(file);
        }

        public SqlDataReader SearchFileByAssignIDAndUserID(int assignID, string userID)
        {
            return UploadDAL.SearchFileByAssignIDAndUserID(assignID, userID);
        }

        public DataSet SearchFileByAssignID(int assignID)
        {
           return UploadDAL.SearchFileByAssignID(assignID);
        }

        public int DeleteFileByID(int iD)
        {
            return UploadDAL.DeleteFileByID(iD);
        }
    }
}
