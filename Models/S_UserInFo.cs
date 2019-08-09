using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial  class S_UserInFo
    {
        public S_UserInFo(int uID, string userID, string userPWD, int power,int deptID, int bOFF, string memo, string mender, DateTime mendDate)
        {
            UID = uID;
            UserID = userID;
            UserPWD = userPWD;
            DeptID = deptID;
            this.bOFF = bOFF;
            Memo = memo;
            Mender = mender;
            MendDate = mendDate;
            Power = power;
        }
        public S_UserInFo() { }


        public int UID { get; set; }
        public string UserID { get; set; }
        public string UserPWD { get; set; }
        public int Power { get; set; }
        public int DeptID { get; set; }
        public int bOFF { get; set; }
        public string Memo { get; set; }
        public string Mender { get; set; }
        public DateTime MendDate { get; set; }
    }
}
