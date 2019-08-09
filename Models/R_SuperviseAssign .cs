using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial   class R_SuperviseAssign
    {
        public R_SuperviseAssign(int assignID, int rID,  int mxID, string assignNo, string replyMemo, int bSate, string memo, string mender, DateTime mendDate)
        {
            AssignID = assignID;
            RID = rID;
            MxID = mxID;
            AssignNo = assignNo;
            ReplyMemo = replyMemo;
            this.bSate = bSate;
            Memo = memo;
            Mender = mender;
            MendDate = mendDate;
        }
        public R_SuperviseAssign() { }
        public int AssignID { get; set; }
        public int RID { get; set; }
        public int MxID { get; set; }
        public string AssignNo { get; set; }
        public string ReplyMemo { get; set; }
        public int bSate { get; set; }
        public string Memo { get; set; }
        public string Mender { get; set; }
        public DateTime MendDate { get; set; }
    }

}
