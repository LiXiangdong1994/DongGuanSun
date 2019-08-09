using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class R_SuperviseMx
    {
        public R_SuperviseMx(int mxID, int rID, string title, DateTime finishDate, string mender, DateTime mendDate)
        {
            MxID = mxID;
            RID = rID;
            Title = title;
            FinishDate = finishDate;
            Mender = mender;
            MendDate = mendDate;
        }
        public R_SuperviseMx() { }
        public int MxID { get; set; }
        public int RID { get; set; }
        public string Title { get; set; }
        public DateTime FinishDate { get; set; }
        public string Mender { get; set; }
        public DateTime MendDate { get; set; }
    }
}
