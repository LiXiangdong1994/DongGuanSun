using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial  class F_Files
    {
        public F_Files(int iD, string superviseAssignID, string userID, string path, DateTime uploadTime)
        {
            ID = iD;
            SuperviseAssignID = superviseAssignID;
            UserID = userID;
            Path = path;
            UploadTime = uploadTime;
        }
        public F_Files() { }
        public int ID{ get; set; }
        public string SuperviseAssignID { get; set; }
        public  string UserID{ get; set; }

        public string Path { get; set; }
        public DateTime UploadTime { get; set; }
    }
}
